using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour
{
    // Team Bidak
    public Color32 team;


    [SerializeField] protected GameObject currentTile = null;


    public virtual void SetupPiece(Color32 teamColor)
    {
        // Set Team Bidak
        team = teamColor;
    }

    public virtual void Place(GameObject tile)
    {
        // Posisikan bidak pada posisi cell papan catur
        gameObject.transform.position = tile.transform.position;

        currentTile = tile;
        tile.GetComponent<Tile>().State = TileState.State.Available;

    }

    public virtual void Move(GameObject selectedTile)
    {
        currentTile.GetComponent<Tile>().State = TileState.State.Unavailable;
        this.transform.position = selectedTile.transform.position;
        this.transform.SetParent(selectedTile.transform);
        currentTile = selectedTile;
    }
    public virtual List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        return GenerateCoordinate(team, 0, 1, tileX, tileY, false, 2);
    }

    public virtual List<Vector2Int> GenerateCoordinate(Color team, int xmove, int ymove, int xpos, int ypos, bool canSkip = false, int movement = 16)
    {
        ymove = (team == Color.white) ? ymove : -ymove;
        List<Vector2Int> paths = new List<Vector2Int>();
        int enemy = 0;
        for (int i = 1; i <= movement + 1; i++)
        {
            if (enemy > 0) break;
            xpos += xmove;
            ypos += ymove;
            GameObject tile = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(xpos, ypos);
            if (tile == null) continue;
            if (!canSkip)
            {
                if (tile.transform.childCount >= 3) enemy++;
            }
            tile.GetComponent<Tile>().State = ValidatePath(tile);
            paths.Add(new Vector2Int(xpos, ypos));

            Debug.Log(tile.GetComponent<Tile>().State.ToString());
        }
        return paths;
    }

    public virtual void Eat()
    {
        Destroy(gameObject);
    }

    public TileState.State ValidatePath(GameObject tile)
    {
        if (tile.transform.childCount < 3)
        {
            return TileState.State.Available;
        }
        else if (tile.transform.childCount == 3)
        {
            var check = tile.transform.GetChild(2).GetComponent<Piece>();

            if ((Color)check.team == this.team)
            {
                return TileState.State.Available;
            }
            else
            {
                return TileState.State.Enemy;
            }

        }
        else
        {
            return TileState.State.Unavailable;
        }

    }


    public GameObject GetCurrentTile()
    {
        return currentTile;
    }




}
