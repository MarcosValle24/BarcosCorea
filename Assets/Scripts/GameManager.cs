using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameResult
{
    Win,
    Lose,
    Pause,
    Playing,
    Menus
}
public enum Mode
{
    FreeTime,
    TimeMode
}
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;

    public Mode gameMode;
    public GameResult gameResult;
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
        gameResult = GameResult.Lose;
    }
    public void Pause(bool isPaused) 
    {
        if (isPaused)
        {
            isPlaying = false;
            gameResult=GameResult.Pause;
        }
        else
        { 
            isPlaying = true;
            gameResult = GameResult.Playing;
        }
    }
    public void OpenFreeGameMode()
    {
        gameMode = Mode.FreeTime;
        gameResult = GameResult.Playing; 
        StartFreeTimeGame?.Invoke();
        BeginPlay();
        SceneManager.LoadScene("FreeMode");
    }
    public void OpenTimeGameMode()
    {
        gameMode = Mode.TimeMode;
        gameResult = GameResult.Playing;
        StartTimeGame?.Invoke();
        BeginPlay();
        SceneManager.LoadScene("TimeMode");
    }
    public void OpenMainMenu()
    {
        gameResult = GameResult.Menus;
        SceneManager.LoadScene("Menu");
    }
}
