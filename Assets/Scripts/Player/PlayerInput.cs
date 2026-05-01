using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputAction controller;
    [SerializeField] private InputAction touchScreen;
    [SerializeField] private InputAction click;
    [SerializeField] private float rotationSpeed;

    private Vector2 firstTouch;
    private Vector2 centerTouch;
    private bool isPressed;
    private float angle;
    public float GetAngle => angle; 
    public float GetRotationSpeed => rotationSpeed;
    public bool GetIsPressed => isPressed;

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
   
    private void Update()
    {
        if (UsingTouch())
            TouchInput();
        else
            MouseInput();
    }

    void TouchInput()
    {
        if (!touchScreen.IsPressed())
        {
            isPressed = false;
            return;
        }

        Vector2 pos = controller.ReadValue<Vector2>();

        if (!isPressed)
        {
            centerTouch = pos;
            firstTouch = pos - centerTouch;
            isPressed = true;
            return;
        }

        Vector2 currentVector = new Vector2(pos.x - centerTouch.x, centerTouch.y);
        angle = Vector2.SignedAngle(firstTouch, currentVector);
        firstTouch = currentVector;
    }

    void MouseInput()
    {
        if (!click.IsPressed())
        {
            isPressed = false;
            return;
        }

        Vector2 pos = controller.ReadValue<Vector2>();

        if (!isPressed)
        {
            centerTouch = pos;
            firstTouch = pos - centerTouch;
            isPressed = true;
            return;
        }

        Vector2 currentVector = new Vector2(pos.x - centerTouch.x, centerTouch.y);
        angle = Vector2.SignedAngle(firstTouch, currentVector);
        firstTouch = currentVector;
    }

    private bool UsingTouch()
    {
        return touchScreen.IsPressed();
    }
}
