using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TimonMovementVersus : MonoBehaviour
{
    [SerializeField] private PlayerInputVersus playerAngleMovement;
    void Update()
    {
        MoveTimon();
    }
    float playerRotationSpeed = .3f;
    public void MoveTimon()
    {
        if (playerAngleMovement == null || !playerAngleMovement.isActiveAndEnabled) return;

        if (playerAngleMovement.GetIsPressed == true)
        {
            float angle = playerAngleMovement.GetAngle;
            //transform.localRotation = Quaternion.Euler(0, 0, -targetAngle);
            transform.Rotate(Vector3.forward * -playerAngleMovement.GetAngle * playerRotationSpeed);
        }
    }
}
