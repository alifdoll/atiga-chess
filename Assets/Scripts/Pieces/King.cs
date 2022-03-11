using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : Piece
{
    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_king");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_king");
        }
    }

    public override List<Vector2Int> Move(int tileX, int tileY)
    {
        var horizontalRight = base.GenerateCoordinate(team, 1, 0, tileX, tileY, movement: 1);
        var horizontalLeft = base.GenerateCoordinate(team, -1, 0, tileX, tileY, movement: 1);

        var verticalUp = base.GenerateCoordinate(team, 0, 1, tileX, tileY, movement: 1);
        var verticalDown = base.GenerateCoordinate(team, 0, -1, tileX, tileY, movement: 1);

        var diagonalUpRight = base.GenerateCoordinate(team, 1, 1, tileX, tileY, movement: 1);
        var diagonalUpLeft = base.GenerateCoordinate(team, -1, 1, tileX, tileY, movement: 1);

        var diagonalDownLeft = base.GenerateCoordinate(team, -1, -1, tileX, tileY, movement: 1);
        var diagonalDownRight = base.GenerateCoordinate(team, 1, -1, tileX, tileY, movement: 1);

        horizontalLeft.AddRange(horizontalRight);

        horizontalLeft.AddRange(verticalUp);
        horizontalLeft.AddRange(verticalDown);

        horizontalLeft.AddRange(diagonalUpRight);
        horizontalLeft.AddRange(diagonalUpLeft);

        horizontalLeft.AddRange(diagonalDownLeft);
        horizontalLeft.AddRange(diagonalDownRight);

        return horizontalLeft;
    }

    public override void Eat()
    {
        FindObjectOfType<GridManager>().GetComponent<GridManager>().isKingDie = true;
        Destroy(gameObject);
    }
}
