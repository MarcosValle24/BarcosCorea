using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelPause;
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
        RemoveAllPanels();
    }

    public void UpdateTimer(string text)
    {
        if (timerText != null)
        {
            timerText.text = text;
        }
    }
    public void RemoveAllPanels()
    {
        panelLose.SetActive(false);
        panelPause.SetActive(false);
        panelWin.SetActive(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelLose.SetActive(false);
        panelPause.SetActive(false);
        panelWin.SetActive(false);

        switch (result)
        {
            case GameResult.Win:
                panelWin.SetActive(true);
                break;
            case GameResult.Pause:
                panelPause.SetActive(true);
                break;
            case GameResult.Lose:
                panelLose.SetActive(true);
                break;
        }
    }
}
