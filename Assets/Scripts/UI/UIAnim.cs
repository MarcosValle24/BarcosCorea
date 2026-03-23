using UnityEngine;
using DG.Tweening;

public class UIAnim : MonoBehaviour
{
    enum animType
    {
        translate,
        rotate,
        scale,
    }
    [SerializeField] private animType currentAnim = animType.translate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {    Sequence animSequence = DOTween.Sequence();
        switch (currentAnim)
        {
            case animType.translate:
                animSequence.Append(
                        GetComponent<RectTransform>().DOMoveY(GetComponent<RectTransform>().position.y + 10, 0.5f,true))
                .AppendInterval(.25f)
                .Append(GetComponent<RectTransform>().DOMoveY(GetComponent<RectTransform>().position.y - 10, 0.5f,true))
                .AppendInterval(.25f);
                break;
            case animType.rotate:
                animSequence.Append(
                    GetComponent<RectTransform>().DORotate(GetComponent<RectTransform>().eulerAngles, .25f));
                break;
            case animType.scale:
                break;

        }
           animSequence.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
