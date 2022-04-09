using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private int movement = 2;
    private Vector3 starting_position = new Vector3();
    public override void SetupTeamColor(Color32 teamColor)
    {
        base.SetupTeamColor(teamColor);
        if (teamColor == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/w_pawn");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/b_pawn");
        }
    }

    public override void Place(GameObject tile)
    {
        base.Place(tile);
        starting_position = gameObject.transform.position;
    }

    public override List<Vector2Int> CreateMovePath(int tileX, int tileY)
    {
        if (starting_position != gameObject.transform.position)
        {
            movement--;
        }

        if (movement < 1) movement = 1;


        var paths = GenerateCoordinate(0, 1, tileX, tileY, movement: movement);
        var right = checkEnemyPawn(tileX + 1, tileY);
        var left = checkEnemyPawn(tileX - 1, tileY);

        if (right || left)
        {
            if (right && left)
            {
                var rightEnemy = GenerateCoordinate(1, 1, tileX, tileY, movement: 1, can_skip: true);
                var leftEnemy = GenerateCoordinate(-1, 1, tileX, tileY, movement: 1, can_skip: true);

                rightEnemy.AddRange(leftEnemy);
                rightEnemy.AddRange(paths);
                return rightEnemy;
            }
            else if (left)
            {
                var enemy = GenerateCoordinate(-1, 1, tileX, tileY, movement: 1, can_skip: true);
                enemy.AddRange(paths);
                return enemy;
            }
            else
            {
                var enemy = GenerateCoordinate(1, 1, tileX, tileY, movement: 1, can_skip: true);
                enemy.AddRange(paths);
                return enemy;
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

    public override List<Vector2Int> GenerateCoordinate(int x_move, int y_move, int x_pos, int y_pos, bool can_skip = false, int movement = 16)
    {
        y_move = (team == Color.white) ? y_move : -y_move;
        List<Vector2Int> paths = new List<Vector2Int>();
        int enemy = 0;
        for (int i = 1; i <= movement; i++)
        {
            if (enemy > 0) break;
            x_pos += x_move;
            y_pos += y_move;
            GameObject tile = FindObjectOfType<GridManager>().GetComponent<GridManager>().GetTileAtPosition(x_pos, y_pos);
            if (tile == null) continue;
            if (!can_skip)
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
            paths.Add(new Vector2Int(x_pos, y_pos));
        }
        return paths;
    }
}
