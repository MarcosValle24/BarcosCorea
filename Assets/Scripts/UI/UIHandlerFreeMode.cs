using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerFreeMode : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelEnterScore;
    [SerializeField] private GameObject panelShowScore;

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
}
