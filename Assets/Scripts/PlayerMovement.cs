using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField] private float rotationSpeed;
    
    [SerializeField]private InputAction controller;
    [SerializeField]private InputAction touchScreen;
    [SerializeField] private InputAction click;

    private Vector2 firstTouch;
    private Vector2 currentTouch;
    private Vector2 centerTouch;
    private bool isPressed;

    private float angle;
   public bool arrived {get;  set;}

    private void OnEnable()
    {
        controller.Enable();
        touchScreen.Enable();
        click.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
        touchScreen.Disable();
        click.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrived = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(arrived)
            StopBoat();
        else
        {
            float value = controller.ReadValue<float>(); 
            float rotationMove = value * rotationSpeed;
        
            transform.Rotate(Vector3.up * rotationMove*Time.deltaTime);
        }

        if (Mouse.current == null) return;

        if (click.WasPressedThisFrame() && isPressed == false)
        {
            centerTouch = Mouse.current.position.ReadValue();
            firstTouch = Vector2.zero;
            isPressed = true;
        }

        if (click.IsPressed() && isPressed == true)
        {
            currentTouch = Mouse.current.position.ReadValue() - centerTouch;

            angle = Vector2.SignedAngle(firstTouch, currentTouch);

            transform.Rotate(Vector3.up * angle * rotationSpeed *  Time.deltaTime);

            firstTouch = currentTouch; 
        }

        if (click.WasReleasedThisFrame())
        {
            isPressed = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void StopBoat()
    {
        if (speed > 0)
        {
            speed-=Time.deltaTime*0.2f;
        } 
        else 
        {
            speed = 0;
        }
    }
    
}
