using System.Linq;
using UnityEngine;

public class DockScript : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         PlayerMovement playerRef = other.GetComponent<PlayerMovement>();
         if (GameManager.instance.gameMode == Mode.FreeTime) ArrivedFreeTime(playerRef);   
         else if (GameManager.instance.gameMode == Mode.TimeMode) ArriveTime(playerRef);   
      }
   }
    void ArrivedFreeTime(PlayerMovement player)
    {
        if (player.hasFish == true)
        {
            player.arrived = true;
            player.RestartPosition();
        }

    }
    void ArriveTime(PlayerMovement player)
    {
        player.arrived = true;
    }
}
