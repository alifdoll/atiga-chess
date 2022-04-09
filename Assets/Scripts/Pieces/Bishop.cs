using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void SetupTeamColor(Team team_color)
    {
        base.SetupTeamColor(team_color);
        if (team_color == Team.WHITE)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_bishop");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_bishop");
        }
    }


    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var diagonal_up_right = base.GenerateCoordinate(1, 1, tileX, tileY);
        var diagonal_up_left = base.GenerateCoordinate(-1, 1, tileX, tileY);

        var diagonal_down_right = base.GenerateCoordinate(1, -1, tileX, tileY);
        var diagonal_down_left = base.GenerateCoordinate(-1, -1, tileX, tileY);

        diagonal_up_right.AddRange(diagonal_up_left);
        diagonal_up_right.AddRange(diagonal_down_right);
        diagonal_up_right.AddRange(diagonal_down_left);

        return diagonal_up_right;

    }
}
