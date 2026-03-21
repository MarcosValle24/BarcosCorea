using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelPause;

    private void Start()
    {
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
        GameManager.instance.Pause(false);
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

    public void ShowPausePanel()
    {
        ShowPanel(GameResult.Pause);
        GameManager.instance.Pause(true);
    }
    public void ShowLosePanel()
    {
        ShowPanel(GameResult.Lose);
    }
    public void ShowWinPanel()
    {
        ShowPanel(GameResult.Win);
    }
    public void ResumeGame()
    {
        RemoveAllPanels();
        GameManager.instance.Pause(false);
        //GameManager.instance.gameResult = GameResult.Playing;
    }
}
