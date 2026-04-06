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
    //    private void Update()
    //    {
    //        if (UsingTouch()) TouchInput();
    //        else MouseInput();
    //        float value = controller.ReadValue<float>();
    //        rotationMove = value * rotationSpeed;
    //    }
    //    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //    void TouchInput()
    //    {
    //        if (Touchscreen.current == null) return;

    //        var touch = Touchscreen.current.primaryTouch;

    //        if (!touch.press.isPressed)
    //        {
    //            isPressed = false;
    //            return;
    //        }

    //        Vector2 pos = touch.position.ReadValue();

    //        if (!isPressed)
    //        {
    //            centerTouch = pos;
    //            firstTouch = pos - centerTouch;
    //            isPressed = true;
    //            return;
    //        }

    //        Vector2 currentVector = pos - centerTouch;

    //        angle = Vector2.SignedAngle(firstTouch, currentVector);

    //        transform.Rotate(Vector3.up * angle * rotationSpeed);

    //        firstTouch = currentVector;
    //    }
    //    void MouseInput()
    //    {
    //        if (Mouse.current == null) return;

    //        if (!click.IsPressed())
    //        {
    //            isPressed = false;
    //            return;
    //        }

    //        Vector2 pos = Mouse.current.position.ReadValue();

    //        if (!isPressed)
    //        {
    //            centerTouch = pos;
    //            firstTouch = pos - centerTouch;
    //            isPressed = true;
    //            return;
    //        }

    //        Vector2 currentVector = pos - centerTouch;

    //        angle = Vector2.SignedAngle(firstTouch, currentVector);

    //        transform.Rotate(Vector3.up * angle * rotationSpeed);

    //        firstTouch = currentVector;
    //    }
    //    private bool UsingTouch()
    //    {
    //        return Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed;
    //    }
    //}
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

        Vector2 currentVector = pos - centerTouch;
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

        Vector2 currentVector = pos - centerTouch;
        angle = Vector2.SignedAngle(firstTouch, currentVector);
        firstTouch = currentVector;
    }

    private bool UsingTouch()
    {
        return touchScreen.IsPressed();
    }
}
