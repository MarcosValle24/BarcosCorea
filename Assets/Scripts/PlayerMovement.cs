using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField] private float rotationSpeed;
    
    [SerializeField]private InputAction controller;
    
   public bool arrived {get;  set;}

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
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
