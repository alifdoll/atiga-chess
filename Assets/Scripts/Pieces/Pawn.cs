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
        var paths = GenerateCoordinate(team, 0, 1, tileX, tileY, movement: tempMove);
        var right = checkEnemyPawn(tileX + 1, tileY);
        var left = checkEnemyPawn(tileX - 1, tileY);

        if (right || left)
        {
            if (right && left)
            {
                var rightEnemy = GenerateCoordinate(team, 1, 1, tileX, tileY, movement: 1, canSkip: true);
                var leftEnemy = GenerateCoordinate(team, -1, 1, tileX, tileY, movement: 1, canSkip: true);

                rightEnemy.AddRange(leftEnemy);
                return rightEnemy;
            }
            else if (left)
            {
                return GenerateCoordinate(team, -1, 1, tileX, tileY, movement: 1, canSkip: true);
            }
            else
            {
                return GenerateCoordinate(team, 1, 1, tileX, tileY, movement: 1, canSkip: true);
            }
        }
        return paths;
    }

    private bool checkEnemyPawn(int tileX, int tileY)
    {
        tileY = (team == Color.white) ? tileY + 1 : tileY - 1;
        GameObject check = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(tileX, tileY);

        if (check == null) return false;
        if (check.transform.childCount >= 3) return true;
        else return false;
    }

    public override List<Vector2Int> GenerateCoordinate(Color team, int xmove, int ymove, int xpos, int ypos, bool canSkip = false, int movement = 16)
    {
        ymove = (team == Color.white) ? ymove : -ymove;
        List<Vector2Int> paths = new List<Vector2Int>();
        int enemy = 0;
        for (int i = 1; i <= movement; i++)
        {
            if (enemy > 0) break;
            xpos += xmove;
            ypos += ymove;
            GameObject tile = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(xpos, ypos);
            if (tile == null) continue;
            if (!canSkip)
            {
                if (tile.transform.childCount >= 3)
                {
                    enemy++;
                    var checkpiece = tile.transform.GetChild(2).GetComponent<Piece>();
                    if (checkpiece.team != (Color)team)
                    {
                        break;
                    }
                }
            }
            tile.GetComponent<Tile>().State = ValidatePath(tile);
            paths.Add(new Vector2Int(xpos, ypos));
        }
        return paths;
    }
}
