using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private Transform initialPos;
    [SerializeField] private float rotationSpeed;
    
    [SerializeField]private InputAction controller;
    [SerializeField]private InputAction touchScreen;
    [SerializeField] private InputAction click;

    private Vector2 firstTouch;
    private Vector2 currentTouch;
    private Vector2 centerTouch;
    private bool isPressed;


    private float angle;

    public float GetAngle { get { return angle; } }
    public bool GetisPressed { get { return isPressed; } }
    public float GetRotationSpeed {  get { return rotationSpeed; } }
   public bool arrived {get;  set;}

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrived = false;
        initialSpeed = speed;
        initialPos = gameObject.transform;
    }
    private void OnEnable()
    {
        controller.Enable();
        touchScreen.Enable();
        click.Enable();
        GameManager.instance.startGame.AddListener(OnGameStart);
    }

    private void OnDisable()
    {
        controller.Disable();
        touchScreen.Disable();
        click.Disable();
        GameManager.instance.startGame.RemoveAllListeners();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnGameStart()
    {
        this.gameObject.transform.position = initialPos.position;
        this.gameObject.transform.rotation = initialPos.rotation;
        speed = initialSpeed;
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
        if (UsingTouch()) TouchInput();
        else MouseInput();
    }
    private bool UsingTouch()
    {
        return Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed;
    }
    void TouchInput()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;

        if (!touch.press.isPressed)
        {
            isPressed = false;
            return;
        }

        Vector2 pos = touch.position.ReadValue();

        if (!isPressed)
        {
            centerTouch = pos;
            firstTouch = pos - centerTouch;
            isPressed = true;
            return;
        }

        Vector2 currentVector = pos - centerTouch;

        angle = Vector2.SignedAngle(firstTouch, currentVector);

        transform.Rotate(Vector3.up * angle * rotationSpeed * Time.deltaTime);

        firstTouch = currentVector;
    }
    void MouseInput()
    {
        if (Mouse.current == null) return;

        if (!click.IsPressed())
        {
            isPressed = false;
            return;
        }

        Vector2 pos = Mouse.current.position.ReadValue();

        if (!isPressed)
        {
            centerTouch = pos;
            firstTouch = pos - centerTouch; 
            isPressed = true;
            return;
        }

        Vector2 currentVector = pos - centerTouch;

        angle = Vector2.SignedAngle(firstTouch, currentVector);

        transform.Rotate(Vector3.up * angle * rotationSpeed * Time.deltaTime);

        firstTouch = currentVector;
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
