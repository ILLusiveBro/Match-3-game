using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite spriteGreen;
    public Sprite spriteBlue;
    public Sprite spritePurple;
    public Sprite spriteRed;
    public Sprite spriteOrange;
    List<Sprite> sprites = new List<Sprite>();

    void Start()
    {
        Screen.SetResolution(600, 800, false);
        sprites.Add(spriteGreen);
        sprites.Add(spriteBlue);
        sprites.Add(spritePurple);
        sprites.Add(spriteRed);
        sprites.Add(spriteOrange);
        var tilesList = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tilesList)
        {         
            tile.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Count)];
        }
    }
}
