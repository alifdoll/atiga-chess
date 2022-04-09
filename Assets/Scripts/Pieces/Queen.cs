using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override void SetupTeamColor(Team team_color)
    {
        base.SetupTeamColor(team_color);
        if (team_color == Team.WHITE)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_queen");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_queen");
        }
    }

    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var horizontal_right = base.GenerateCoordinate(1, 0, tileX, tileY);
        var horizontal_left = base.GenerateCoordinate(-1, 0, tileX, tileY);

        var vertical_up = base.GenerateCoordinate(0, 1, tileX, tileY);
        var vertical_down = base.GenerateCoordinate(0, -1, tileX, tileY);

        var diagonal_up_right = base.GenerateCoordinate(1, 1, tileX, tileY);
        var diagonal_up_left = base.GenerateCoordinate(-1, 1, tileX, tileY);

        var diagonal_down_left = base.GenerateCoordinate(-1, -1, tileX, tileY);
        var diagonal_down_right = base.GenerateCoordinate(1, -1, tileX, tileY);

        horizontal_left.AddRange(horizontal_right);

        horizontal_left.AddRange(vertical_up);
        horizontal_left.AddRange(vertical_down);

        horizontal_left.AddRange(diagonal_up_right);
        horizontal_left.AddRange(diagonal_up_left);

        horizontal_left.AddRange(diagonal_down_left);
        horizontal_left.AddRange(diagonal_down_right);

        return horizontal_left;
    }
}
