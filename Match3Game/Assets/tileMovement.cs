using UnityEngine;

public class tileSelection : MonoBehaviour
{
    static Tile selectedTile = new Tile();
    static Tile secondTile = new Tile();
    public float highlightScale = 1.1f;

    public class Tile
    {
        public Vector3 tilePosition = new Vector3();
        public string name = null;
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
        if (selectedTile.name == null)
        {
            Select(selectedTile);
        }
        else
        if (selectedTile.tilePosition == transform.position)
            Deselect(selectedTile);
        else
        {
            Select(secondTile);
        }
        if (selectedTile.name != null && secondTile.name != null)
            if (
                (Mathf.Abs(selectedTile.tilePosition.x - secondTile.tilePosition.x) < 2f && (selectedTile.tilePosition.y == secondTile.tilePosition.y)) ||
                (Mathf.Abs(selectedTile.tilePosition.y - secondTile.tilePosition.y) < 2f && (selectedTile.tilePosition.x == secondTile.tilePosition.x))
                )
                Swap();
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
