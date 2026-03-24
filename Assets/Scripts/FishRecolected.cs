using UnityEngine;

public class FishRecolected : MonoBehaviour
{
    [Header("Particlee")]
    [SerializeField] private ParticleSystem confettiFX;
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
        if(confettiFX != null)
        {
            ParticleSystem fx = Instantiate(confettiFX,transform.position,Quaternion.identity);
            Destroy(fx.gameObject, fx.main.duration);
        }
        Destroy(gameObject);
    }

}
