using UnityEngine;

public class tileMovement : MonoBehaviour
{
    static Tile selectedTile = new Tile();
    static Tile secondTile = new Tile();
    public float highlightScale = 1.1f;

    public class Tile
    {
        public Vector3 tilePosition = new Vector3();
        public string name;
    }

    void Deselect(Tile tile)
    {
        GameObject.Find(tile.name).transform.localScale += new Vector3(-highlightScale, -highlightScale, 0);
        tile.tilePosition = new Vector3();
        tile.name = null;
    }

    void Select(Tile tile)
    {
        tile.tilePosition = transform.position;
        tile.name = transform.name;
        GameObject.Find(tile.name).transform.localScale += new Vector3(highlightScale, highlightScale, 0);
    }

    void OnMouseDown()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (selectedTile.name == null)
        {
            Select(selectedTile);
        }
        else if (selectedTile.tilePosition != transform.position
            && ((Mathf.Abs(selectedTile.tilePosition.x - x) < 1.6f && Mathf.Abs(y - selectedTile.tilePosition.y) < 0.1f)
            || (Mathf.Abs(selectedTile.tilePosition.y - y) < 1.6f && Mathf.Abs(x - selectedTile.tilePosition.x) < 0.1f)))
        {
            Select(secondTile);
            Swap();
        }
        else
        {
            Deselect(selectedTile);
        }
    }

    private void Swap()
    {
        Tile temp = selectedTile;
        GameObject.Find(selectedTile.name).transform.position = Vector3.MoveTowards(selectedTile.tilePosition, secondTile.tilePosition, 1.5f);
        GameObject.Find(secondTile.name).transform.position = Vector3.MoveTowards(temp.tilePosition, temp.tilePosition, 1.5f);
        Deselect(selectedTile);
        Deselect(secondTile);
    }
}
