using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{

    #region properties and stuff
    [SerializeField] private int width, height;
    [SerializeField] private GameObject prefab_tile;
    [SerializeField] private Camera main_camera;
    [SerializeField] GameObject board;
    [SerializeField] GameObject piece_prefab;

    private GameObject[,] tile_positions = new GameObject[8, 8];

    private GameObject[,] destroyed_chess_piece = new GameObject[8, 8];

    private GameObject[] player_black = new GameObject[16];
    private GameObject[] player_white = new GameObject[16];

    private Dictionary<Vector2, GameObject> tiles_map = new Dictionary<Vector2, GameObject>();

    private string[] piece_order = new string[16] {
    "P","P","P","P","P","P","P","P",
    "R","K","B","KG","Q","B","K","R"
    };

    private Dictionary<string, Type> piece_map = new Dictionary<string, Type>() {
        {"P", typeof(Pawn)},
        {"R", typeof(Rook)},
        {"K", typeof(Knight)},
        {"B", typeof(Bishop)},
        {"Q", typeof(Queen)},
        {"KG", typeof(King)},
    };



    private GameObject currently_selected_piece = null;
    private List<Vector2Int> move_paths = new List<Vector2Int>();
    private Color current_player = Color.white;


    private bool is_king_die = false;
    public List<Vector2Int> MovePaths { get => move_paths; set => move_paths = value; }
    public Color CurrentPlayer { get => current_player; set => current_player = value; }
    public bool IsKingDie { get => is_king_die; set => is_king_die = value; }
    public GameObject CurrentlySelectedPiece { get => currently_selected_piece; set => currently_selected_piece = value; }
    #endregion
    #region grid generator



    private void Start()
    {
        // Generate papan terlebih dahulu
        GenerateBoard();

        // Lalu generate tiap bidak
        GeneratePieces(tile_positions);
    }


    // Method untuk generate papan catur
    private void GenerateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // Size papan
                float size = 1.2f;

                // Instantiate tiap sel papan menjadi gameobject
                GameObject newTile = Instantiate(prefab_tile, new Vector3(x * size, y * size), Quaternion.identity);

                // Buat tiap sel menjadi child dari gameobject board
                newTile.transform.parent = board.transform;

                // Offset untuk warna board selang seling
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                // newTile.Init(isOffset);
                newTile.GetComponent<Tile>().Init(isOffset, x, y);

                // Simpan posisi tiap sel papan
                tile_positions[x, y] = newTile;
                tiles_map[new Vector2(newTile.transform.position.x, newTile.transform.position.y)] = newTile;
            }
        }

        // Posisikan kamera selalu berada pada posisi papan
        main_camera.transform.position = new Vector3((float)width / 4 - 0.82f, (float)height / 2 - 0.31f, -10);
    }


    // Method untuk generate semua bidak catur
    private void GeneratePieces(GameObject[,] chessBoard)
    {
        // Generate bidak hitam dan putih
        player_white = CreatePiece(Color.white);
        player_black = CreatePiece(Color.black);

        // Set posisi bidak hitam dan putih
        PlacePiece(1, 0, player_white, chessBoard);
        PlacePiece(6, 7, player_black, chessBoard);
    }



    // Method untuk generate tiap bidak
    private GameObject[] CreatePiece(Color32 team_color)
    {
        GameObject[] new_piece = new GameObject[16];
        // TILE AVAILABLE WHEN PLACED WITH PIECE
        for (int i = 0; i < new_piece.Length; i++)
        {
            // Buat gameobject nya
            GameObject piece_obj = Instantiate(piece_prefab, new Vector3(1, 1), Quaternion.identity);

            // Rotate -180 untuk tim hitam
            if (team_color == Color.black) piece_obj.transform.localRotation = Quaternion.Euler(0f, 0f, -180f);

            // Ambil tipe bidak
            Type piece_types = piece_map[piece_order[i]];

            // Destroy(GetComponent<Piece>());

            // Beri tipe pada gameobject bidak
            Piece create_piece = piece_obj.AddComponent(piece_types) as Piece;

            // Setup sprite dan tim bidak
            create_piece.SetupTeamColor(team_color);

            // Beri nama pada gameobject bidak
            piece_obj.name = piece_types.Name;

            new_piece[i] = piece_obj;
        }
        return new_piece;
    }



    // Method untuk mengatur posisi tiap bidak catur
    private void PlacePiece(int pawn, int royalty, GameObject[] player_chess, GameObject[,] chess_board)
    {
        for (int i = 0; i < 8; i++)
        {
            // Set Posisi bidak pawn
            player_chess[i].GetComponent<Piece>().Place(chess_board[i, pawn]);
            // Set tile gameobject sebagai parent dari gameobject bidak
            player_chess[i].transform.SetParent(chess_board[i, pawn].transform);

            // Set Posisi bidak royalti
            player_chess[i + 8].GetComponent<Piece>().Place(chess_board[i, royalty]);
            // Set tile gameobject sebagai parent dari gameobject bidak
            player_chess[i + 8].transform.SetParent(chess_board[i, royalty].transform);
        }
    }
    #endregion



    #region methods
    public Vector2Int GetTilePosition(Tile tile)
    {
        if (tile_positions[tile.X, tile.Y] != null)
        {
            return new Vector2Int(tile.X, tile.Y);
        }
        return new Vector2Int(0, 0);
    }

    public GameObject GetTileAtPosition(int x, int y)
    {
        try
        {
            return tile_positions[x, y];
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }

    }


    public Piece GetSelectedPiece()
    {
        return CurrentlySelectedPiece.GetComponent<Piece>();
    }
    public void ActivatePath(List<Vector2Int> list_path, GameObject piece)
    {
        MovePaths = list_path;
        for (int i = 0; i < list_path.Count; i++)
        {
            Vector2Int path = list_path[i];
            Tile tile = tile_positions[path.x, path.y].GetComponent<Tile>();
            if (tile.State == TileState.State.Move)
            {
                tile.GetComponent<Tile>().ActivateHighlight(piece);
            }

        }
    }

    public void DeactivatePath(List<Vector2Int> list_path)
    {
        for (int i = 0; i < list_path.Count; i++)
        {
            Vector2Int path = list_path[i];
            Tile tile = tile_positions[path.x, path.y].GetComponent<Tile>();
            tile.GetComponent<Tile>().DeactivateHighlight();
        }
        MovePaths.Clear();
    }
    #endregion
}
