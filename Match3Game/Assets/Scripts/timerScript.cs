using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public Text timeText;
    public int timeSeconds = 200;
    Time time;
    float seconds = 0;
    int minutes = 0;
    bool gameEnded = false;
    void Start()
    {
        minutes = (timeSeconds / 60);
        seconds = timeSeconds % 60;
        tileMovement.inputEnabled = true;
    }

    void Update()
    {
        if(!gameEnded)
        timeText.text = (minutes.ToString() + ":" + seconds.ToString("00"));
        seconds -= Time.deltaTime;
        if (seconds < 1 && !gameEnded)
        {
            if (minutes == 0 && seconds < 1)
            {
                gameEnded = true;
                timeText.text = "0:00";
                FindObjectOfType<GameManager>().EndGame();              
            }
            else
            {
                minutes--;
                seconds = 59;
            }
        }
    }
}
