using DG.Tweening;
using UnityEngine;

public class FishMovingLeft : MonoBehaviour
{
    [SerializeField] private float speed = 40;

    [SerializeField] RectTransform rectFish;
    [SerializeField] RectTransform rectCanvas;

    [SerializeField] private float yOffset = 30f;
    [SerializeField] private float duration = 1f;

    void Start()
    {
        rectFish = GetComponent<RectTransform>();

        rectCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        float startY = rectFish.anchoredPosition.y;

        rectFish.DOAnchorPosY(startY + yOffset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        rectFish.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        float leftLimit = -rectCanvas.rect.width / 2f - rectFish.rect.width / 4f;
        float rightSpawn = rectCanvas.rect.width / 2f;//+ rectFish.rect.width / 8f;

        if (rectFish.anchoredPosition.x < leftLimit)
        {
            rectFish.anchoredPosition = new Vector2(rightSpawn,rectFish.anchoredPosition.y);
        }
    }
}
