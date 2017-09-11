using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    void Update()
    {
        scoreText.text = tileMovement.score.ToString();
    }

}
