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

public class tileMovement : MonoBehaviour
{
    static Tile selectedTile = new Tile();
    static Tile secondTile = new Tile();
    public float highlightScale = 1.1f;
    bool swap = false;
    int counter = 1;

    public class Tile
    {
        public Vector3 tilePosition = new Vector3();
        public string name;

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
            swap = true;
        }
        else
        {
            Deselect(selectedTile);
        }
    }

    void Update()
    {
        if (swap)
        {
            GameObject.Find(selectedTile.name).transform.position = Vector3.MoveTowards(selectedTile.tilePosition, secondTile.tilePosition, 100 * Time.deltaTime);
            GameObject.Find(secondTile.name).transform.position = Vector3.MoveTowards(secondTile.tilePosition, selectedTile.tilePosition, 100 * Time.deltaTime);
            selectedTile.tilePosition = GameObject.Find(selectedTile.name).transform.position;
            secondTile.tilePosition = GameObject.Find(secondTile.name).transform.position;
            if (MatchSearch(selectedTile,true) || MatchSearch(secondTile,true))
            {
                Debug.Log("Vertical tiles match!");
            }
            else if(MatchSearch(selectedTile, false) || MatchSearch(secondTile, false))
            {
                Debug.Log("Horizontal tiles match!");
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
            swap = false;
        }
    }

    bool MatchSearch(Tile tile, bool vertical)
    {
        Vector2 currenTile = tile.tilePosition;
        Vector2 originalTile = tile.tilePosition;
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
        Physics.Raycast(currenTile, backwardDirection, out hitBackward, 1.5f);
        Physics.Raycast(originalTile, forwardDirection, out hitForward, 1.5f);
        var tag = GameObject.Find(tile.name).tag;
        bool firstLoop = true;
        while (hitForward.collider != null || firstLoop)
        {            
            firstLoop = false;
            while (hitBackward.collider != null )
            {
                if (hitBackward.collider.tag == tag)
                {
                    counter++;
                    currenTile = hitBackward.collider.transform.position;
                    Physics.Raycast(currenTile, backwardDirection, out hitBackward, 1.5f);
                }
                else break;              
            }         

            if(hitForward.collider != null && hitForward.collider.tag == tag)
            {
                originalTile = hitForward.collider.transform.position;
                counter++;
                Physics.Raycast(originalTile, forwardDirection, out hitForward, 1.5f);
            }     
            else break;
        }

        if (counter >= 3)
        {
            counter = 1;
            return true;
        }
        else
        {
            counter = 1;
            return false;

        }
    }