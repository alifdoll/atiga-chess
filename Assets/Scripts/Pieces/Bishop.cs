using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
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
        var diagonalUpRight = base.GenerateCoordinate(team, 1, 1, tileX, tileY);
        var diagonalUpLeft = base.GenerateCoordinate(team, -1, 1, tileX, tileY);

        var diagonalDownRight = base.GenerateCoordinate(team, 1, -1, tileX, tileY);
        var diagonalDownLeft = base.GenerateCoordinate(team, -1, -1, tileX, tileY);

        diagonalUpRight.AddRange(diagonalUpLeft);
        diagonalUpRight.AddRange(diagonalDownRight);
        diagonalUpRight.AddRange(diagonalDownLeft);

        return diagonalUpRight;

    }
}
