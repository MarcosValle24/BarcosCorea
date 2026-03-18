using UnityEngine;

public class TimeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float maxTime;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
        GameManager.instance.StartTimeGame.AddListener(setValues);   
    }
    void setValues()
    {
        UIHandler.instance.timerText.gameObject.SetActive(true);
        timer = maxTime;
    }
    void HandlePlayerStopped()
    {
        UIHandler.instance.ShowPanel(GameResult.Win);
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPlaying) return;

        if (GameManager.instance.gameMode == Mode.TimeMode && !player.arrived)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            UIHandler.instance.UpdateTimer(minutes.ToString("00") + ":" + seconds.ToString("00"));

            if (timer <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
        
    }
}
