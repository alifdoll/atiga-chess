using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private int movement = 2;

    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_pawn");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_pawn");
        }
    }

    public override List<Vector2Int> Move(int tileX, int tileY)
    {
        int tempMove = movement;
        if (movement == 2) movement--;
        return base.GenerateCoordinate(team, 0, 1, tileX, tileY, movement: tempMove);
    }
}
