using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override void SetupPiece(Color32 teamColor)
    {
        base.SetupPiece(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_rook");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_rook");
        }
    }


    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        var yMovementPost = base.GenerateCoordinate(team, 0, 1, tileX, tileY);
        var xMovementPost = base.GenerateCoordinate(team, 1, 0, tileX, tileY);

        var yMovementNeg = base.GenerateCoordinate(team, 0, -1, tileX, tileY);
        var xMovementNeg = base.GenerateCoordinate(team, -1, 0, tileX, tileY);

        xMovementPost.AddRange(yMovementPost);
        xMovementPost.AddRange(xMovementNeg);
        xMovementPost.AddRange(yMovementNeg);
        return xMovementPost;
    }
}
