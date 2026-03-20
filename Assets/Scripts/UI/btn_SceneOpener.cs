using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_SceneOpener : MonoBehaviour
{
    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
