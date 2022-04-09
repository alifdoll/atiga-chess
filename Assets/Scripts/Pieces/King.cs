using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : Piece
{
    public override void SetupTeamColor(Color32 teamColor)
    {
        base.SetupTeamColor(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_king");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_king");
        }
    }

    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var horizontal_right = base.GenerateCoordinate(1, 0, tileX, tileY, movement: 1);
        var horizontal_left = base.GenerateCoordinate(-1, 0, tileX, tileY, movement: 1);

        var vertical_up = base.GenerateCoordinate(0, 1, tileX, tileY, movement: 1);
        var vertical_down = base.GenerateCoordinate(0, -1, tileX, tileY, movement: 1);

        var diagonal_up_right = base.GenerateCoordinate(1, 1, tileX, tileY, movement: 1);
        var diagonal_up_left = base.GenerateCoordinate(-1, 1, tileX, tileY, movement: 1);

        var diagonal_down_left = base.GenerateCoordinate(-1, -1, tileX, tileY, movement: 1);
        var diagonal_down_right = base.GenerateCoordinate(1, -1, tileX, tileY, movement: 1);

        horizontal_left.AddRange(horizontal_right);

        horizontal_left.AddRange(vertical_up);
        horizontal_left.AddRange(vertical_down);

        horizontal_left.AddRange(diagonal_up_right);
        horizontal_left.AddRange(diagonal_up_left);

        horizontal_left.AddRange(diagonal_down_left);
        horizontal_left.AddRange(diagonal_down_right);

        return horizontal_left;
    }

    public override void Eat()
    {
        FindObjectOfType<GridManager>().GetComponent<GridManager>().IsKingDie = true;
        Destroy(gameObject);
    }
}
