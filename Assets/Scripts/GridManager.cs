using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{

    #region properties and stuff
    [SerializeField] private int width, height;
    [SerializeField] private GameObject prefabTile;
    [SerializeField] private Camera mainCamera;
    [SerializeField] GameObject board;
    [SerializeField] GameObject piecePrefab;

    private GameObject[,] tilePositions = new GameObject[8, 8];

    private GameObject[,] destroyedChessPiece = new GameObject[8, 8];

    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private Dictionary<Vector2, GameObject> tilesMap = new Dictionary<Vector2, GameObject>();

    private string[] pieceOrder = new string[16] {
    "P","P","P","P","P","P","P","P",
    "R","K","B","KG","Q","B","K","R"
    };

    private Dictionary<string, Type> pieceMap = new Dictionary<string, Type>() {
        {"P", typeof(Pawn)},
        {"R", typeof(Rook)},
        {"K", typeof(Knight)},
        {"B", typeof(Bishop)},
        {"Q", typeof(Queen)},
        {"KG", typeof(King)},
    };



    private GameObject currentlySelectedPiece = null;
    private List<Vector2Int> movePaths = new List<Vector2Int>();
    private Color currentPlayer = Color.white;


    private bool isKingDie = false;
    public List<Vector2Int> MovePaths { get => movePaths; set => movePaths = value; }
    public Color CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
    public bool IsKingDie { get => isKingDie; set => isKingDie = value; }
    public GameObject CurrentlySelectedPiece { get => currentlySelectedPiece; set => currentlySelectedPiece = value; }
    #endregion
    #region grid generator



    private void Start()
    {
        // Generate papan terlebih dahulu
        GenerateBoard();

        // Lalu generate tiap bidak
        GeneratePieces(tilePositions);
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
                GameObject newTile = Instantiate(prefabTile, new Vector3(x * size, y * size), Quaternion.identity);

                // Buat tiap sel menjadi child dari gameobject board
                newTile.transform.parent = board.transform;

                // Offset untuk warna board selang seling
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                // newTile.Init(isOffset);
                newTile.GetComponent<Tile>().Init(isOffset, x, y);

                // Simpan posisi tiap sel papan
                tilePositions[x, y] = newTile;
                tilesMap[new Vector2(newTile.transform.position.x, newTile.transform.position.y)] = newTile;
            }
        }

        // Posisikan kamera selalu berada pada posisi papan
        mainCamera.transform.position = new Vector3((float)width / 4 - 0.82f, (float)height / 2 - 0.31f, -10);
    }


    // Method untuk generate semua bidak catur
    private void GeneratePieces(GameObject[,] chessBoard)
    {
        // Generate bidak hitam dan putih
        playerWhite = CreatePiece(Color.white);
        playerBlack = CreatePiece(Color.black);

        // Set posisi bidak hitam dan putih
        PlacePiece(1, 0, playerWhite, chessBoard);
        PlacePiece(6, 7, playerBlack, chessBoard);
    }



    // Method untuk generate tiap bidak
    private GameObject[] CreatePiece(Color32 teamColor)
    {
        GameObject[] newPiece = new GameObject[16];
        // TILE AVAILABLE WHEN PLACED WITH PIECE
        for (int i = 0; i < newPiece.Length; i++)
        {
            // Buat gameobject nya
            GameObject pieceObj = Instantiate(piecePrefab, new Vector3(1, 1), Quaternion.identity);

            // Rotate -180 untuk tim hitam
            if (teamColor == Color.black) pieceObj.transform.localRotation = Quaternion.Euler(0f, 0f, -180f);

            // Ambil tipe bidak
            Type pieceTypes = pieceMap[pieceOrder[i]];

            // Destroy(GetComponent<Piece>());

            // Beri tipe pada gameobject bidak
            Piece createPiece = pieceObj.AddComponent(pieceTypes) as Piece;

            // Setup sprite dan tim bidak
            createPiece.SetupPiece(teamColor);

            // Beri nama pada gameobject bidak
            pieceObj.name = pieceTypes.Name;

            newPiece[i] = pieceObj;
        }
        return newPiece;
    }



    // Method untuk mengatur posisi tiap bidak catur
    private void PlacePiece(int pawn, int royalty, GameObject[] playerChess, GameObject[,] chessBoard)
    {
        for (int i = 0; i < 8; i++)
        {
            // Set Posisi bidak pawn
            playerChess[i].GetComponent<Piece>().Place(chessBoard[i, pawn]);
            // Set tile gameobject sebagai parent dari gameobject bidak
            playerChess[i].transform.SetParent(chessBoard[i, pawn].transform);

            // Set Posisi bidak royalti
            playerChess[i + 8].GetComponent<Piece>().Place(chessBoard[i, royalty]);
            // Set tile gameobject sebagai parent dari gameobject bidak
            playerChess[i + 8].transform.SetParent(chessBoard[i, royalty].transform);
        }
    }
    #endregion



    #region methods
    public Vector2Int GetTilePosition(Tile tile)
    {
        if (tilePositions[tile.X, tile.Y] != null)
        {
            return new Vector2Int(tile.X, tile.Y);
        }
        return new Vector2Int(0, 0);
    }

    public GameObject GetTileAtPosition(int x, int y)
    {
        try
        {
            return tilePositions[x, y];
        }
        catch (Exception e)
        {
            return null;
        }
    }


    public Piece GetSelectedPiece()
    {
        return CurrentlySelectedPiece.GetComponent<Piece>();
    }
    public void ActivatePath(List<Vector2Int> listPath, GameObject piece)
    {
        MovePaths = listPath;
        for (int i = 0; i < listPath.Count; i++)
        {
            Vector2Int path = listPath[i];
            Tile tile = tilePositions[path.x, path.y].GetComponent<Tile>();
            if (tile.State != TileState.State.Unavailable)
            {
                tile.GetComponent<Tile>().ActivateHighlight(piece);
            }

        }
    }

    public void DeactivatePath(List<Vector2Int> listPath)
    {
        for (int i = 0; i < listPath.Count; i++)
        {
            Vector2Int path = listPath[i];
            Tile tile = tilePositions[path.x, path.y].GetComponent<Tile>();
            tile.GetComponent<Tile>().DeactivateHighlight();
        }
        MovePaths.Clear();
    }
    #endregion
}
