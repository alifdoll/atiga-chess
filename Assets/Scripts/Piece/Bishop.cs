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


    // public override List<Vector2Int> Move(int x, int y)
    // {
    //     return base.Move(x, y);
    // }
}
