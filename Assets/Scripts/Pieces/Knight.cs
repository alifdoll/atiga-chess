using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
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
        var firstL = base.GenerateCoordinate(team, 1, 2, tileX, tileY, true, 0);
        var secondL = base.GenerateCoordinate(team, -1, 2, tileX, tileY, true, 0);

        var thirdL = base.GenerateCoordinate(team, 2, 1, tileX, tileY, true, 0);
        var fourthL = base.GenerateCoordinate(team, -2, 1, tileX, tileY, true, 0);

        var fifthL = base.GenerateCoordinate(team, 2, -1, tileX, tileY, true, 0);
        var sixthL = base.GenerateCoordinate(team, -2, -1, tileX, tileY, true, 0);

        var seventhL = base.GenerateCoordinate(team, 1, -2, tileX, tileY, true, 0);
        var eighthL = base.GenerateCoordinate(team, -1, -2, tileX, tileY, true, 0);

        firstL.AddRange(secondL);
        firstL.AddRange(thirdL);
        firstL.AddRange(fourthL);
        firstL.AddRange(fifthL);
        firstL.AddRange(sixthL);
        firstL.AddRange(seventhL);
        firstL.AddRange(eighthL);

        return firstL;
    }
}
