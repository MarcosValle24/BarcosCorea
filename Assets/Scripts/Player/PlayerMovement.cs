using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    [Header("Inputs")]
    [SerializeField]private InputAction controller;
    [SerializeField]private InputAction touchScreen;
    [SerializeField] private InputAction click;
    private Vector2 firstTouch;
    private Vector2 currentTouch;
    private Vector2 centerTouch;
    private bool isPressed;
    public bool hasStopped;
    private float angle;

    public UnityEvent OnStopped;
    public float GetAngle { get { return angle; } }
    public bool GetisPressed { get { return isPressed; } }
    public float GetRotationSpeed {  get { return rotationSpeed; } }
   public bool arrived {get;  set;}

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        arrived = false;
        hasStopped = false;
        initialSpeed = speed;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    private void OnEnable()
    {
        controller.Enable();
        touchScreen.Enable();
        click.Enable();
        GameManager.instance.StartFreeTimeGame.AddListener(OnGameStart);
        GameManager.instance.StartTimeGame.AddListener(OnGameStart);
    }

    private void OnDisable()
    {
        controller.Disable();
        touchScreen.Disable();
        click.Disable();
        GameManager.instance.StartFreeTimeGame.RemoveListener(OnGameStart);
        GameManager.instance.StartTimeGame.RemoveListener(OnGameStart);
    }

    private void OnGameStart()
    {
        arrived = false;
        hasStopped = false;
        speed = initialSpeed;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isPressed = false;
    }
    void Update()
    {
        if (!GameManager.instance.isPlaying)
            return;

        if (arrived)
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
        if (!GameManager.instance.isPlaying)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity = transform.right * speed;
    }
    public void Crash()
    {
        Camera.main.transform.DOShakePosition(
            duration: 0.3f,
            strength: 0.5f,
            vibrato: 10,
            randomness: 90,
            snapping: false,
            fadeOut: true
        );
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    void StopBoat()
    {
        if (speed > 0)
        {
            speed-=Time.deltaTime*0.2f;
        } 
        else 
        {
            if (!hasStopped)
            {
                hasStopped = true;
                OnStopped?.Invoke();
                RestartPosition();
            }
            speed = 0;
        }
    }
    public void RestartPosition()
    {
        OnGameStart();
    }
    
}
