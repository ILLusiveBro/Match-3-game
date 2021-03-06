﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite spriteGreen;
    public Sprite spriteBlue;
    public Sprite spritePurple;
    public Sprite spriteRed;
    public Sprite spriteOrange;
    List<Sprite> sprites = new List<Sprite>();
    public static bool firstGeneration = true;
    public GameObject levelCompleteUI;
    public Text finalScoreText;
    public Text highScoreText;
    static int highScore;
    bool gamePaused = false;

    public void PauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }         
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }        
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    public void EndGame()
    {
        tileMovement.inputEnabled = false;
        levelCompleteUI.GetComponent<Animation>().Play();
        RecordHighScore();
        finalScoreText.text = "Your score: " + tileMovement.score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Highest score: " + highScore.ToString();
        levelCompleteUI.SetActive(true);
        
    }

    public static void RecordHighScore()
    {
        if (tileMovement.score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", tileMovement.score);
    }

    public void ColorChange(List<GameObject> tilesList)
    {
        foreach (var tile in tilesList)
        {           
            switch (Random.Range(0, sprites.Count))
            {
                case 0:
                    tile.GetComponent<SpriteRenderer>().sprite = sprites[0];
                    tile.tag = "Green";
                    break;
                case 1:
                    tile.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    tile.tag = "Blue";
                    break;
                case 2:
                    tile.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    tile.tag = "Purple";
                    break;
                case 3:
                    tile.GetComponent<SpriteRenderer>().sprite = sprites[3];
                    tile.tag = "Red";
                    break;
                case 4:
                    tile.GetComponent<SpriteRenderer>().sprite = sprites[4];
                    tile.tag = "Orange";
                    break;
            }
        }
    }

    List<GameObject> GetAllTiles()
    {
        List<GameObject> tilesList = new List<GameObject>();
        var tilesFound = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tilesFound)
        {
            tilesList.Add(tile);
        }
        return tilesList;
    }

    void Start()
    {      
        sprites.Add(spriteGreen);
        sprites.Add(spriteBlue);
        sprites.Add(spritePurple);
        sprites.Add(spriteRed);
        sprites.Add(spriteOrange);
        List<GameObject> tilesList = new List<GameObject>();
        tilesList = GetAllTiles();
        ColorChange(tilesList);
        FindObjectOfType<tileMovement>().FindAllMatches();
        tileMovement.score = 0;
        firstGeneration = false;
    }
}
