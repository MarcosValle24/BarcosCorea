using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenFreeGameMode()
    {
        GameManager.instance.OpenFreeGameMode();
    }
    public void OpenTimeGameMode()
    {
        GameManager.instance.OpenTimeGameMode();
    }
}
