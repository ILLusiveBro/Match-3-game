using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public GameObject tile;
    public Text scoreText;
    static int highScore;
    float time = 60;

    public static void RecordHighScore()
    {
        if (tileMovement.score > highScore)
            PlayerPrefs.SetInt("HighScore", tileMovement.score);
        Debug.Log(highScore.ToString());
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {
        scoreText.text = tileMovement.score.ToString();
        time -= Time.deltaTime;
    }

}
