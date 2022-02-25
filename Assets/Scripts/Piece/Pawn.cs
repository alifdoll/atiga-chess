using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
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

    public override string TestType()
    {
        return "Pawn";
    }
}
