using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public float stopSpeed;
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
    public bool hasFish;
    private float angle;
    private Transform currentDock;
    private bool rotateAfterStop;
    public UnityEvent OnStopped;
    public UnityEvent OnCrashed;
    public float GetAngle { get { return angle; } }
    public bool GetisPressed { get { return isPressed; } }
    public float GetRotationSpeed {  get { return rotationSpeed; } }
   public bool arrived {get;  set;}

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        arrived = false;
        hasStopped = false;
        hasFish = false;
        initialSpeed = speed;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    private void Start()
    {
        OnGameStart();
    }
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

    private void OnGameStart()
    {
        arrived = false;
        hasStopped = false;
        hasFish= false;
        rotateAfterStop = false;
        speed = initialSpeed;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isPressed = false;
        GameManager.instance.isPlaying = true;
    }
    void Update()
    {
        if (arrived)
            StopBoat();

        if (!GameManager.instance.isPlaying)
            return;
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

        transform.Rotate(Vector3.up * angle * rotationSpeed);//* Time.deltaTime);

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

        transform.Rotate(Vector3.up * angle * rotationSpeed);

        firstTouch = currentVector;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isPlaying && !hasStopped)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }
        rb.linearVelocity = transform.right * speed;
    }
    public void Crash()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Camera.main.transform.DOShakePosition(
            duration: 0.3f,
            strength: 0.5f,
            vibrato: 10,
            randomness: 90,
            snapping: false,
            fadeOut: true
        );
    }
    void StopBoat()
    {
        if (speed > 0)
        {
            speed-=Time.deltaTime* stopSpeed;
            //if (!hasStopped)
            //{
            //    hasStopped = true;
            //    if(GameManager.instance.gameMode == Mode.TimeMode)
            //    {
            //        OnStopped?.Invoke();
            //        return;
            //    }
            //    if (rotateAfterStop)
            //    {
            //        rotateAfterStop = false;
            //        StartCoroutine(LookAtDockRight(currentDock));
            //    }
            //}
        } 
        else 
        {
            if (!hasStopped)
            {
                GameManager.instance.isPlaying = false;
                hasStopped = true;
                if (GameManager.instance.gameMode == Mode.TimeMode)
                {
                    OnStopped?.Invoke();
                    return;
                }
                if (rotateAfterStop)
                {
                    rotateAfterStop = false;
                    StartCoroutine(LookAtDockRight(currentDock));
                }
                speed = 0;
            }
        }
    }
    public void RestartPosition()
    {
        OnGameStart();
    }
    public void ArrivedWithFish(Transform dockRight)
    {
        GameManager.instance.isPlaying = false;
        arrived = true;
        hasFish = false;
        StopAllCoroutines();
        StartCoroutine(LookAtDockRight(dockRight));
    }
    IEnumerator LookAtDockRight(Transform dockRight)
    {
        Vector3 dockDirection = dockRight.TransformDirection(-Vector3.forward);
        dockDirection.y = 0;
        dockDirection.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dockDirection);

        while (true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.deltaTime);          
            float dot = Vector3.Dot(transform.forward, dockDirection);

            if (dot >= 0.99f) break;

            yield return null;
        }
        OnStopped?.Invoke();
        ResetAfterArrival();
    }
    public void ResetAfterArrival()
    {
        arrived = false;
        hasStopped = false;
        GameManager.instance.isPlaying = true;
        speed = initialSpeed;
    }
    public void BeginStop(Transform dock)
    {
        speed = 0;
        arrived = true;
        currentDock = dock;
        rotateAfterStop = true;
        hasFish = false;
        GameManager.instance.isPlaying = false;
    }
    public void CrashFreeMode()
    {
        if (GameManager.instance.gameMode == Mode.FreeTime) OnCrashed?.Invoke();
    }
}
