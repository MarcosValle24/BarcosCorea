using DG.Tweening;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    private Transform startPos;
    private BoxCollider boxCollider;
    private ParticleSystem particles;
    private AudioSource audiocrash;
    private Rigidbody rb;
    [SerializeField]private float speed = 5f;
    
    private Vector3 direction = Vector3.right;
    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed * Time.deltaTime;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BoatWall")) return;

        BoatWall wall = other.GetComponent<BoatWall>();
        if(wall == null) return;

        switch (wall.type)
        {
            case WallType.RIGHT: 
            case WallType.LEFT:
                direction.x *= -1;
                break;
            case WallType.UP:
            case WallType.DOWN:
                direction.z *= -1;
                break;
        }
        direction.y = 0;
        direction.Normalize();
    }
}
