using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tile : MonoBehaviour
{
    #region Properties and Stuff
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject plate;

    // Debug Purposes
    [SerializeField] private TileState.State stateTest;

    private int x = 0;
    private int y = 0;
    private TileState.State state = TileState.State.Unavailable;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public TileState.State State { get => state; set => state = value; }

    #endregion

    // FOR DEBUG PURPOSES
    private void Start()
    {
        stateTest = this.state;
    }

    private void Update()
    {
        stateTest = this.state;
    }
    // FOR DEBUG PURPOSES

    public void Init(bool isOffset, int xVal, int yVal)
    {
        this.x = xVal;
        this.y = yVal;
        SpriteRenderer tileRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (isOffset) tileRenderer.color = Color.white;
        else tileRenderer.color = Color.blue;
    }

    public void ActivateHighlight(GameObject piece)
    {
        if (this.state == TileState.State.Enemy)
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/plate_red");
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/plate");
            this.state = TileState.State.Move;
        }
        GetGridManager().CurrentlySelectedPiece = piece;
        plate.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        GetGridManager().CurrentlySelectedPiece = null;
        if (transform.childCount < 3)
        {
            this.state = TileState.State.Unavailable;
        }
        plate.SetActive(false);
    }

    public GridManager GetGridManager()
    {
        return FindObjectOfType<GridManager>().GetComponent<GridManager>();
    }

    private Piece GetPiece()
    {
        return gameObject.transform.GetChild(2).gameObject.GetComponent<Piece>();
    }

    private GameObject GetPieceGameObject()
    {
        return gameObject.transform.GetChild(2).gameObject;
    }

    private void OnMouseEnter()
    {
        // Debug.Log(this.SelectedPiece.GetComponent<Piece>().team);
        if (gameObject.transform.childCount >= 3)
        {
            if (GetGridManager().CurrentPlayer == GetPiece().team)
            {
                highlight.SetActive(true);
            }

        }
    }

    private void OnMouseExit()
    {
        if (gameObject.transform.childCount >= 3)
        {
            highlight.SetActive(false);
        }
    }


    private void OnMouseDown()
    {
        var gridManager = GetGridManager();


        if (this.State == TileState.State.Available /*&& GetGridManager().currentPlayer == GetPiece().team*/)
        {
            Debug.Log(this.State.ToString());
            if (GetGridManager().CurrentlySelectedPiece == null)
            {
                this.state = TileState.State.Selected;
                GameObject chessObj = gameObject.transform.GetChild(2).gameObject;
                gridManager.MovePaths = CreatePath();
                gridManager.GetComponent<GridManager>().ActivatePath(gridManager.MovePaths, chessObj);
                gridManager.CurrentlySelectedPiece = gameObject.transform.GetChild(2).gameObject;
            }
            else
            {
                gridManager.DeactivatePath(gridManager.MovePaths);
                gridManager.CurrentlySelectedPiece = gameObject.transform.GetChild(2).gameObject;
                gridManager.MovePaths = CreatePath();
                gridManager.ActivatePath(gridManager.MovePaths, gridManager.CurrentlySelectedPiece);
            }

        }
        else if (this.State == TileState.State.Move)
        {
            gridManager.GetSelectedPiece().Move(gameObject);
            this.State = TileState.State.Available;
            gridManager.DeactivatePath(gridManager.MovePaths);
            gridManager.CurrentPlayer = (gridManager.CurrentPlayer == Team.WHITE) ? Team.BLACK : Team.WHITE;
        }
        else if (this.State == TileState.State.Enemy)
        {
            Destroy(GetPieceGameObject());
            gridManager.GetSelectedPiece().Move(gameObject);
            this.State = TileState.State.Available;
            gridManager.DeactivatePath(gridManager.MovePaths);
        }
        else if (this.State == TileState.State.Selected)
        {
            gridManager.DeactivatePath(gridManager.MovePaths);
            gridManager.CurrentlySelectedPiece = null;
            this.state = TileState.State.Available;
        }

    }

    private List<Vector2Int> CreatePath()
    {
        List<Vector2Int> paths = new List<Vector2Int>();
        if (gameObject.transform.childCount >= 2)
        {
            Piece chessPiece = GetPiece().GetComponent<Piece>();
            paths = chessPiece.CreateMovePath(this.x, this.y);
        }
        return paths;
    }


}
