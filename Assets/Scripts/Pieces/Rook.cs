using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override void SetupTeamColor(Team team_color)
    {
        base.SetupTeamColor(team_color);
        if (team_color == Team.WHITE)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_rook");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_rook");
        }
    }


    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var y_move_pos = base.GenerateCoordinate(0, 1, tileX, tileY);
        var x_move_pos = base.GenerateCoordinate(1, 0, tileX, tileY);

        var y_move_neg = base.GenerateCoordinate(0, -1, tileX, tileY);
        var x_move_neg = base.GenerateCoordinate(-1, 0, tileX, tileY);

        x_move_pos.AddRange(y_move_pos);
        x_move_pos.AddRange(x_move_neg);
        x_move_pos.AddRange(y_move_neg);
        return x_move_pos;
    }
}
