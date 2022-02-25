using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileState
{
    Available,
    Unavailable,
    MoveTile,
    OutOfBounds,
    Enemy
}
public class Tile : MonoBehaviour
{
    #region Properties and Stuff
    private Color highlightColor = new Color32(213, 237, 251, 255);

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject plate;

    private SpriteRenderer tileRenderer;

    private int x = 0;
    private int y = 0;

    private TileState state = TileState.Unavailable;

    private GameObject selectedPiece = null;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    internal TileState State { get => state; set => state = value; }
    public GameObject SelectedPiece { get => selectedPiece; set => selectedPiece = value; }


    #endregion
    public void Init(bool isOffset, int xVal, int yVal)
    {
        this.x = xVal;
        this.y = yVal;
        tileRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (isOffset) tileRenderer.color = Color.white;
        else tileRenderer.color = Color.red;
    }

    public void ActivateHighlight(GameObject piece)
    {
        this.state = TileState.MoveTile;
        plate.SetActive(true);
        this.SelectedPiece = piece;
    }

    public void DeactivateHighlight()
    {
        Debug.Log("deactivate");
        // this.state = TileState.Unavailable;
        plate.SetActive(false);
    }

    public GridManager GetGridManager()
    {
        return FindObjectOfType<GridManager>().GetComponent<GridManager>();
    }

    private void OnMouseEnter()
    {
        if (gameObject.transform.childCount >= 3)
        {
            highlight.SetActive(true);
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
        Debug.Log(this.state);
        var gridManager = GetGridManager();
        if (this.State == TileState.Unavailable)
        {
            GameObject chessObj = gameObject.transform.GetChild(2).gameObject;
            gridManager.movePaths = CreatePath();
            var test = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTilePosition(this);
            FindObjectOfType<GridManager>().GetComponent<GridManager>().ActivatePath(gridManager.movePaths, chessObj);
            Debug.Log(gridManager.movePaths.Count);
        }
        else if (this.State == TileState.MoveTile)
        {
            GameObject selectedPiece = this.SelectedPiece;
            selectedPiece.transform.position = gameObject.transform.position;
            selectedPiece.transform.SetParent(gameObject.transform);
            this.State = TileState.Unavailable;
            GetGridManager().DeactivatePath(gridManager.movePaths);
        }
    }

    // Masi ada error tapi sudah bisa!!!!!!!!!!!
    private List<Vector2Int> CreatePath()
    {
        List<Vector2Int> paths = new List<Vector2Int>();
        if (gameObject.transform.childCount >= 2)
        {
            GameObject chessObj = gameObject.transform.GetChild(2).gameObject;
            Piece chessPiece = chessObj.GetComponent<Piece>();

            paths = chessPiece.Move(this.x, this.y);
        }
        return paths;
    }


}
