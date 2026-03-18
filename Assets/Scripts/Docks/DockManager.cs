using System.Collections.Generic;
using UnityEngine;

public class DockManager : MonoBehaviour
{
    public static DockManager instance;   
    [SerializeField] private List<GameObject> docks;
    private GameObject currentDock;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DeactivateDocks();
        if (GameManager.instance != null)
        {
            GameManager.instance.StartTimeGame.AddListener(ActivateRandomDock);
            GameManager.instance.StartFreeTimeGame.AddListener(ActivateRandomDock);
            GameManager.instance.FinishGame.AddListener(DeactivateDocks);
        }
    }
    private void DeactivateDocks()
    {
        foreach (GameObject dock in docks)
        {
            dock.SetActive(false);
        }
        currentDock = null;
    }
    public void ActivateRandomDock()
    {
        DeactivateDocks();
        if (currentDock != null)
            currentDock.SetActive(false);

        currentDock = docks[Random.Range(0, docks.Count)];
        currentDock.SetActive(true);
    }
}
