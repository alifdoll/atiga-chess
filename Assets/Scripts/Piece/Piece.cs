using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour
{
    // Team Bidak
    protected Color32 team;

    protected GameObject currentTile = null;

    public virtual void SetupPiece(Color32 teamColor)
    {
        // Set Team Bidak
        team = teamColor;
    }

    public virtual string TestType()
    {
        return "Piece";
    }


    public void Place(GameObject tile)
    {
        // Posisikan bidak pada posisi cell papan catur
        gameObject.transform.position = tile.transform.position;

        currentTile = tile;
    }
    public virtual List<Vector2Int> Move(int tileX, int tileY)
    {
        return GenerateCoordinate(team, 0, 1, tileX, tileY, false, 2);
    }

    protected virtual List<Vector2Int> GenerateCoordinate(Color team, int xmove, int ymove, int xpos, int ypos, bool canSkip = false, int movement = 16)
    {
        ymove = (team == Color.white) ? ymove : -ymove;
        List<Vector2Int> paths = new List<Vector2Int>();
        for (int i = 1; i <= movement; i++)
        {
            xpos += xmove;
            ypos += ymove;
            GameObject tile = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(xpos, ypos);
            if (tile == null) continue;
            if (!canSkip)
            {
                if (tile.transform.childCount >= 3)
                {
                    break;
                }
            }
            tile.GetComponent<Tile>().State = ValidatePath(tile);
            paths.Add(new Vector2Int(xpos, ypos));
        }
        return paths;
    }

    public TileState.State ValidatePath(GameObject tile)
    {
        if (tile.transform.childCount < 3)
        {
            return TileState.State.Available;
        }
        return TileState.State.Unavailable;
    }


    public GameObject GetCurrentTile()
    {
        return currentTile;
    }




}
