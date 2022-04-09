using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override void SetupTeamColor(Team team_color)
    {
        base.SetupTeamColor(team_color);
        if (team_color == Team.WHITE)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_knight");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_knight");
        }
    }


    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var first_L = base.GenerateCoordinate(1, 2, tileX, tileY, true, 0);
        var second_L = base.GenerateCoordinate(-1, 2, tileX, tileY, true, 0);

        var third_L = base.GenerateCoordinate(2, 1, tileX, tileY, true, 0);
        var fourth_L = base.GenerateCoordinate(-2, 1, tileX, tileY, true, 0);

        var fifth_L = base.GenerateCoordinate(2, -1, tileX, tileY, true, 0);
        var sixth_L = base.GenerateCoordinate(-2, -1, tileX, tileY, true, 0);

        var seventh_L = base.GenerateCoordinate(1, -2, tileX, tileY, true, 0);
        var eighth_L = base.GenerateCoordinate(-1, -2, tileX, tileY, true, 0);

        first_L.AddRange(second_L);
        first_L.AddRange(third_L);
        first_L.AddRange(fourth_L);
        first_L.AddRange(fifth_L);
        first_L.AddRange(sixth_L);
        first_L.AddRange(seventh_L);
        first_L.AddRange(eighth_L);

        return first_L;
    }
}
