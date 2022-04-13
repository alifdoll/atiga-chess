using System.Collections.Generic;
using UnityEngine;
public enum Team
{
    WHITE,
    BLACK
}

public class Piece : MonoBehaviour
{

    // Team Bidak
    public Team team;


    [SerializeField] protected GameObject current_tile = null;


    public virtual void SetupTeamColor(Team team_color)
    {
        // Set Team Bidak
        team = team_color;
    }

    public virtual void Place(GameObject tile)
    {
        // Posisikan bidak pada posisi cell papan catur
        gameObject.transform.position = tile.transform.position;

        current_tile = tile;
        tile.GetComponent<Tile>().State = TileState.State.Available;

    }

    public virtual void Eat()
    {
        Destroy(gameObject);
    }

    public virtual void Move(GameObject selected_tile)
    {
        current_tile.GetComponent<Tile>().State = TileState.State.Unavailable;
        this.transform.position = selected_tile.transform.position;
        this.transform.SetParent(selected_tile.transform);
        current_tile = selected_tile;
    }

    public virtual List<Vector2Int> CreateMovePath(int tile_x, int tile_y)
    {
        return GenerateCoordinate(0, 1, tile_x, tile_y, false, 2);
    }

    public virtual List<Vector2Int> GenerateCoordinate(int x_move, int y_move, int x_pos, int y_pos, bool can_skip = false, int movement = 16)
    {
        Debug.Log(movement.ToString());
        y_move = (team == Team.WHITE) ? y_move : -y_move;
        List<Vector2Int> paths = new List<Vector2Int>();
        int enemy = 0;
        for (int i = 1; i <= movement + 1; i++)
        {
            if (enemy > 0) break;
            x_pos += x_move;
            y_pos += y_move;
            GameObject tile = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(x_pos, y_pos);
            if (tile == null) continue;
            if (!can_skip)
            {
                if (tile.transform.childCount >= 3) enemy++;
            }
            tile.GetComponent<Tile>().State = ValidatePath(tile);
            paths.Add(new Vector2Int(x_pos, y_pos));
        }
        return paths;
    }


    public TileState.State ValidatePath(GameObject tile)
    {
        if (tile.transform.childCount < 3)
        {
            return TileState.State.Move;
        }
        else if (tile.transform.childCount == 3)
        {
            var check = tile.transform.GetChild(2).GetComponent<Piece>();

            if (check.team != this.team)
            {
                return TileState.State.Enemy;
            }
            else
            {
                return TileState.State.Available;
            }

        }
        else
        {
            return TileState.State.Unavailable;
        }

    }


    public GameObject GetCurrentTile()
    {
        return current_tile;
    }




}
