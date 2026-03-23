using UnityEngine;

public class BoatNLose : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerRef = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerRef != null)
            {
                playerRef.CrashFreeMode();
            }
        }
    }
}
