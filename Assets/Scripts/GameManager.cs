using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameResult
{
    Win,
    Lose,
    Pause
}
public enum Mode
{
    FreeTime,
    TimeMode
}
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;
    //[SerializeField] private FreeGameMode freeGameMode;
    //[SerializeField] private TimeGameMode timeGameMode;
    [SerializeField] private float maxTime;

    public Mode gameMode;
    public bool hasFish;
    public bool isPlaying = false;

    public UnityEvent StartFreeTimeGame;
    public UnityEvent StartTimeGame;
    public UnityEvent FinishGame;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void BeginPlay()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        isPlaying = false;
    }
    public void Pause(bool isPaused) 
    {
        if (isPaused)
        {
            isPlaying = false;
            //UIHandler.instance.ShowPanel(GameResult.Pause);
        }
        else
        { 
            isPlaying = true;
            //UIHandler.instance.RemoveAllPanels();
        }
    }
    public void FreeGameMode()
    {
        gameMode = Mode.FreeTime;
        StartFreeTimeGame?.Invoke();
        BeginPlay();
        SceneManager.LoadScene("TimeMode");
    }
    public void TimeGameMode()
    {
        gameMode = Mode.TimeMode;
        StartTimeGame?.Invoke();
        BeginPlay();
        SceneManager.LoadScene("FreeMode");

    }
}
