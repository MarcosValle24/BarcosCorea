using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerFreeMode : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelEnterScore;
    [SerializeField] private GameObject panelShowScore;
    [SerializeField] private TextMeshProUGUI fishText;

    private void Start()
    {
        RemoveAllPanels();
    }

    public void RemoveAllPanels()
    {
        panelPause.SetActive(false);
        panelEnterScore.SetActive(false);
        panelShowScore.SetActive(false);
        GameManager.instance.Pause(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelEnterScore.SetActive(false);
        panelPause.SetActive(false);

        switch (result)
        {
            case GameResult.Pause:
                panelPause.SetActive(true);
                break;
            case GameResult.Lose:
                panelEnterScore.SetActive(true);
                break;
        }
    }
    public void ShowPausePanel()
    {
        RemoveAllPanels();
        panelPause.SetActive(true);
        GameManager.instance.Pause(true);
    }
    public void ResumeGame()
    {
        RemoveAllPanels();
        GameManager.instance.Pause(false);
    }
    public void ShowScorePanel()
    {
        RemoveAllPanels();
        panelShowScore.SetActive(true);
    }
    public void EnterScorePanel()
    {
        ShowPanel(GameResult.Lose);
        panelEnterScore.SetActive(true);
    }
    public void ShowFishRecolected(string fishInt)
    {
        fishText.text = $"Recolectaste: {fishInt} peces!";
    }
}
