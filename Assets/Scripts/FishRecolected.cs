using UnityEngine;

public class FishRecolected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().hasFish = true;
            Recolected();
        }
    }
    private void Recolected()
    {
        Destroy(gameObject);
    }

}
