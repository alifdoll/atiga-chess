using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_queen");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_queen");
        }
    }

    public override List<Vector2Int> Move(int tileX, int tileY)
    {
        var horizontalRight = base.GenerateCoordinate(team, 1, 0, tileX, tileY);
        var horizontalLeft = base.GenerateCoordinate(team, -1, 0, tileX, tileY);

        var verticalUp = base.GenerateCoordinate(team, 0, 1, tileX, tileY);
        var verticalDown = base.GenerateCoordinate(team, 0, -1, tileX, tileY);

        var diagonalUpRight = base.GenerateCoordinate(team, 1, 1, tileX, tileY);
        var diagonalUpLeft = base.GenerateCoordinate(team, -1, 1, tileX, tileY);

        var diagonalDownLeft = base.GenerateCoordinate(team, -1, -1, tileX, tileY);
        var diagonalDownRight = base.GenerateCoordinate(team, 1, -1, tileX, tileY);

        horizontalLeft.AddRange(horizontalRight);

        horizontalLeft.AddRange(verticalUp);
        horizontalLeft.AddRange(verticalDown);

        horizontalLeft.AddRange(diagonalUpRight);
        horizontalLeft.AddRange(diagonalUpLeft);

        horizontalLeft.AddRange(diagonalDownLeft);
        horizontalLeft.AddRange(diagonalDownRight);

        return horizontalLeft;
    }
}
