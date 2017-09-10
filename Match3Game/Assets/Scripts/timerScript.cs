using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public Text timeText;
    public int timeSeconds = 200;
    Time time;
    float seconds = 0;
    int minutes = 0;
    void Start()
    {
        minutes = (timeSeconds / 60);
        seconds = timeSeconds % 60;
        tileMovement.inputEnabled = true;
    }

    void Update()
    {
        timeText.text = (minutes.ToString() + ":" + seconds.ToString("00"));
        seconds -= Time.deltaTime;
        if (seconds < 1)
        {
            if (minutes == 0 && seconds < 1)
            {
                tileMovement.inputEnabled = false;
                Score.RecordHighScore();
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                minutes--;
                seconds = 59;
                Debug.Log(minutes.ToString());
            }
        }
    }
}
