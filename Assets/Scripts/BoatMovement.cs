using DG.Tweening;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    private Transform startPos;
    private BoxCollider boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        image = GetComponentInChildren<SpriteRenderer>();
        Animation();
    }

    private void Update()
    {

    }

    private void Animation()
    {
        DOTween.Kill(image);

        image.transform
            .DOBlendableRotateBy(new Vector3(10f, 3.0f, 0), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
