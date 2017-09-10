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
            switch(Random.Range(0, sprites.Count))
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
}
