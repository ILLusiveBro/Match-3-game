using UnityEngine;
using System.Collections.Generic;

public class tileMovement : MonoBehaviour
{
    static Tile selectedTile = new Tile();
    static Tile secondTile = new Tile();
    public float highlightScale = 1.1f;
    public static int score = 0;
    public static bool inputEnabled = true;
    float tilesDistance = 1.6f;

    public class Tile
    {
        public Vector3 tilePosition = new Vector3();
        public string name;
    }

    void Select(Tile tile)
    {
        tile.tilePosition = transform.position;
        tile.name = transform.name;
        GameObject.Find(tile.name).transform.localScale += new Vector3(highlightScale, highlightScale, 0);
    }

    void Deselect(Tile tile)
    {
        GameObject.Find(tile.name).transform.localScale += new Vector3(-highlightScale, -highlightScale, 0);
        tile.tilePosition = new Vector3();
        tile.name = null;
    }

    void CreateNewTiles(List<GameObject> destroyedTiles)
    {
        foreach (var tile in destroyedTiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            FindObjectOfType<GameManager>().TileColorGeneration(destroyedTiles);
            RaycastHit hit;
            if (Physics.Raycast(tile.transform.position, Vector2.up, out hit, 1.5f)) // Prevent top tile from disappearing 
            while (Physics.Raycast(tile.transform.position, Vector2.up, out hit, 1.5f))
            {
                var temp = hit.collider.transform.position;
                hit.collider.transform.position = tile.transform.position;
                tile.transform.position = temp;               
            } 
            tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            tile.GetComponent<Animation>().Play("blockCreation");
            tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void OnMouseDown()
    {
        if (inputEnabled)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            if (selectedTile.name == null)
            {
                Select(selectedTile);
            }
            else if (selectedTile.tilePosition != transform.position
                && ((Mathf.Abs(selectedTile.tilePosition.x - x) < tilesDistance && Mathf.Abs(y - selectedTile.tilePosition.y) < 0.1f)
                || (Mathf.Abs(selectedTile.tilePosition.y - y) < tilesDistance && Mathf.Abs(x - selectedTile.tilePosition.x) < 0.1f)))
            {
                Select(secondTile);
                Swap();
            }
            else
            {
                Deselect(selectedTile);
            }
        }
    }

    public void Swap()
    {
        List<GameObject> destroyedTiles = new List<GameObject>();
        GameObject.Find(selectedTile.name).transform.position = Vector3.MoveTowards(selectedTile.tilePosition, secondTile.tilePosition, 100 * Time.deltaTime);
        GameObject.Find(secondTile.name).transform.position = Vector3.MoveTowards(secondTile.tilePosition, selectedTile.tilePosition, 100 * Time.deltaTime);
        selectedTile.tilePosition = GameObject.Find(selectedTile.name).transform.position;
        secondTile.tilePosition = GameObject.Find(secondTile.name).transform.position;
        if (MatchSearch(selectedTile, destroyedTiles) | MatchSearch(secondTile, destroyedTiles))
        {
            CreateNewTiles(destroyedTiles);
        }
        else
        {
            Animation selectedAnimation = GameObject.Find(selectedTile.name).GetComponent<Animation>();
            Animation secondAnimation = GameObject.Find(secondTile.name).GetComponent<Animation>();
            selectedAnimation.Play();
            secondAnimation.Play();
            GameObject.Find(secondTile.name).transform.position = Vector3.MoveTowards(secondTile.tilePosition, selectedTile.tilePosition, 100 * Time.deltaTime);
            GameObject.Find(selectedTile.name).transform.position = Vector3.MoveTowards(selectedTile.tilePosition, secondTile.tilePosition, 100 * Time.deltaTime);
        }
        Deselect(selectedTile);
        Deselect(secondTile);
        destroyedTiles.Clear();
    }

    public bool MatchSearch(Tile tile, List<GameObject> destroyedTiles)
    {
        int counter = 1;
        int counterX = 0;
        int counterY = 0;

        destroyedTiles.AddRange(CollisionDetection(tile, true, ref counterY));
        counter += counterY;
        destroyedTiles.AddRange(CollisionDetection(tile, false, ref counterX));
        counter += counterX;
        if (counter < 3)
        {
            return false;
        }
        else
        {
            destroyedTiles.Add(GameObject.Find(tile.name));
            if(!GameManager.firstGeneration)
            ScoreCalculation(counter);
            return true;
        }
    }

    void ScoreCalculation(int counter)
    {
        if (counter == 3)
            score += 100;
        else if (counter == 4)
            score += 300;
        else
        {
            score += 1000 + (500 * (counter - 5));
        }
        counter = 1;
    }

    List<GameObject> CollisionDetection(Tile tile, bool vertical, ref int counter)
    {
        List<GameObject> matchedTiles = new List<GameObject>();
        Vector2 originalTile = tile.tilePosition;
        Vector2 currentTile = tile.tilePosition;
        RaycastHit hitForward;
        RaycastHit hitBackward;
        Vector3 forwardDirection;
        Vector3 backwardDirection;
        if (vertical)
        {
            forwardDirection = Vector3.up;
            backwardDirection = Vector3.down;
        }
        else
        {
            forwardDirection = Vector3.right;
            backwardDirection = Vector3.left;
        }
        Physics.Raycast(currentTile, backwardDirection, out hitBackward, 1.5f);
        Physics.Raycast(currentTile, forwardDirection, out hitForward, 1.5f);
        var tag = GameObject.Find(tile.name).tag;

        do
        {
            while (hitBackward.collider != null)
            {
                if (hitBackward.collider.tag == tag)
                {
                    counter++;
                    currentTile = hitBackward.collider.transform.position;
                    matchedTiles.Add(hitBackward.collider.gameObject);
                    Physics.Raycast(currentTile, backwardDirection, out hitBackward, 1.5f);
                }
                else break;
            }

            if (hitForward.collider != null && hitForward.collider.tag == tag)
            {
                counter++;
                originalTile = hitForward.collider.transform.position;
                matchedTiles.Add(hitForward.collider.gameObject);
                Physics.Raycast(originalTile, forwardDirection, out hitForward, 1.5f);
            }
            else break;
        } while (hitForward.collider != null);

        if (counter < 2)
        {
            matchedTiles.Clear();
            counter = 0;
        }
        return matchedTiles;
    }
}
