using DG.Tweening;
using UnityEngine;

public class FishAnimationMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    private Transform startPos;
    private BoxCollider boxCollider;
    private bool isJumping;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        image = GetComponentInChildren<SpriteRenderer>();
        Animation();
    }

    private void Update()
    {
        if (!isJumping)
        {

        }
    }

    private void Animation()
    {
        DOTween.Kill(image);

        image.transform
            .DOBlendableRotateBy(new Vector3(0f, 15.0f, 0), .5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
