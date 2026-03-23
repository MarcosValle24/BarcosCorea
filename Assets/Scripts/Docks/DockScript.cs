using System.Linq;
using UnityEngine;

public class DockScript : MonoBehaviour
{
    [SerializeField] ParticleSystem arrivedParticles;
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
          arrivedParticles.Play();
         PlayerMovement playerRef = other.GetComponent<PlayerMovement>();
         if (GameManager.instance.gameMode == Mode.FreeTime) ArrivedFreeTime(playerRef);   
         else if (GameManager.instance.gameMode == Mode.TimeMode) ArriveTime(playerRef);   
      }
   }
    void ArrivedFreeTime(PlayerMovement player)
    {
        if (player.hasFish == true)
        {
            player.BeginStop(this.transform);
        }
    }
    void ArriveTime(PlayerMovement player)
    {
        player.arrived = true;
    }
}
