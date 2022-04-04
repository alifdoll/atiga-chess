using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tile : MonoBehaviour
{
    #region Properties and Stuff
    private Color highlightColor = new Color32(213, 237, 251, 255);

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject plate;

    private SpriteRenderer tileRenderer;

    private int x = 0;
    private int y = 0;

    private TileState.State state = TileState.State.Unavailable;

    private GameObject selectedPiece = null;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    internal TileState.State State { get => state; set => state = value; }
    public GameObject SelectedPiece { get => selectedPiece; set => selectedPiece = value; }


    #endregion


    public void Init(bool isOffset, int xVal, int yVal)
    {
        this.x = xVal;
        this.y = yVal;
        tileRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            this.state = TileState.State.MoveTile;
        }
        GetGridManager().CurrentlySelectedPiece = piece;
        plate.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        GetGridManager().CurrentlySelectedPiece = null;
        this.state = TileState.State.Unavailable;
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
            if (GetGridManager().currentPlayer == GetPiece().team)
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

        if (this.State == TileState.State.Unavailable /*&& GetGridManager().currentPlayer == GetPiece().team*/)
        {
            if (GetGridManager().CurrentlySelectedPiece == null)
            {
                GameObject chessObj = gameObject.transform.GetChild(2).gameObject;
                gridManager.movePaths = CreatePath();
                gridManager.GetComponent<GridManager>().ActivatePath(gridManager.movePaths, chessObj);
                gridManager.CurrentlySelectedPiece = gameObject.transform.GetChild(2).gameObject;
            }
            else
            {
                gridManager.DeactivatePath(gridManager.movePaths);
                gridManager.CurrentlySelectedPiece = gameObject.transform.GetChild(2).gameObject;
                gridManager.movePaths = CreatePath();
                gridManager.ActivatePath(gridManager.movePaths, gridManager.CurrentlySelectedPiece);
            }

        }
        else if (this.State == TileState.State.MoveTile)
        {
            gridManager.CurrentlySelectedPiece.transform.position = gameObject.transform.position;
            gridManager.CurrentlySelectedPiece.transform.SetParent(gameObject.transform);
            this.State = TileState.State.Unavailable;
            gridManager.DeactivatePath(gridManager.movePaths);
            gridManager.currentPlayer = (gridManager.currentPlayer == Color.white) ? Color.black : Color.white;
        }
        else if (this.State == TileState.State.Enemy)
        {
            Destroy(GetPieceGameObject());
            gridManager.CurrentlySelectedPiece.transform.position = gameObject.transform.position;
            gridManager.CurrentlySelectedPiece.transform.SetParent(gameObject.transform);
            this.State = TileState.State.Unavailable;
            gridManager.DeactivatePath(gridManager.movePaths);
        }
        else if (this.State == TileState.State.Selected)
        {

        }

    }

    // Masi ada error tapi sudah bisa!!!!!!!!!!!
    private List<Vector2Int> CreatePath()
    {
        List<Vector2Int> paths = new List<Vector2Int>();
        if (gameObject.transform.childCount >= 2)
        {
            Piece chessPiece = GetPiece().GetComponent<Piece>();
            paths = chessPiece.Move(this.x, this.y);
        }
        return paths;
    }


}
