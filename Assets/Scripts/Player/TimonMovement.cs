using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TimonMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerAngleMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTimon();
    }
    public void MoveTimon()
    {
        if (playerAngleMovement == null || !playerAngleMovement.isActiveAndEnabled) return;

        if (playerAngleMovement.GetisPressed == true)
        {
            transform.Rotate(Vector3.forward * playerAngleMovement.GetAngle);
        }

    }
}
