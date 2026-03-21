using UnityEngine;

public class FishRecolected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.hasFish = true;
            other.GetComponent<PlayerMovement>().hasFish = true;
            Recolected();
        }
    }
    private void Recolected()
    {
        Destroy(gameObject);
    }

}
