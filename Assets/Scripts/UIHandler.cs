using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    [SerializeField] private TMP_Text timerText;

    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateTimer(string text)
    {
        if (timerText != null)
        {
            timerText.text = text;
        }
    }
}
