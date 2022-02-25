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

    public override string TestType()
    {
        return "King";
    }
}
