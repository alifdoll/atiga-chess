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

    public override string TestType()
    {
        return "Knight";
    }
}
