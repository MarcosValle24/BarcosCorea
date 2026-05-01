using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerFreeMode : MonoBehaviour
{
    [SerializeField] private GameObject panelEnterScore;
    [SerializeField] private GameObject panelShowScore;
    [SerializeField] private TextMeshProUGUI fishTextEnterScore;
    [SerializeField] private TextMeshProUGUI recolectedFishGameUI;

    private void Start()
    {
        RemoveAllPanels();
    }

    public void RemoveAllPanels()
    {
        panelEnterScore.SetActive(false);
        panelShowScore.SetActive(false);
        GameManager.instance.Pause(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelEnterScore.SetActive(false);

        switch (result)
        {
            case GameResult.Lose:
                panelEnterScore.SetActive(true);
                break;
            default:
                break;
        }
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
        fishTextEnterScore.text = $"Recolectaste: {fishInt} peces!";
    }
    public void ShowFishGameUI(string fishInt)
    {
        recolectedFishGameUI.text = $"Peces: {fishInt}!";
    }
}
