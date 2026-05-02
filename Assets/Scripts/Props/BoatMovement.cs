using DG.Tweening;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    private Transform startPos;
    private BoxCollider boxCollider;
    private ParticleSystem particles;
    private AudioSource audiocrash;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        image = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        audiocrash = GetComponentInChildren<AudioSource>();
        Animation();
    }

    private void Animation()
    {
        DOTween.Kill(image);

        image.transform
            .DOBlendableRotateBy(new Vector3(10f, 3.0f, 0), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particles.Play();
            audiocrash.Play();
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.Crash();
            }
        }
    }
}
