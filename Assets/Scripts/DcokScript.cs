using UnityEngine;

public class DcokScript : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         other.GetComponent<PlayerMovement>().arrived = true;
         GameManager.instance.GameOver();
      }
   }
}
