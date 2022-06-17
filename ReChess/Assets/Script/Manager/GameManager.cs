using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private Board board;

    public GameObject pawn;
    public GameObject knight;
    public GameObject rook;
    public GameObject bishop;
    public GameObject queen;
    public GameObject king;

    public Material white;
    public Material black;


    public Dictionary<Vector2, GameObject> pieces = new Dictionary<Vector2, GameObject>();

    public void Start()
    {
        AddPiece(rook, Color.white, 0, 0);
        AddPiece(knight, Color.white, 1, 0);
        AddPiece(bishop, Color.white, 2, 0);
        AddPiece(queen, Color.white, 3, 0);
        AddPiece(king, Color.white, 4, 0);
        AddPiece(bishop, Color.white, 5, 0);
        AddPiece(knight, Color.white, 6, 0);
        AddPiece(rook, Color.white, 7, 0);

        AddPiece(rook, Color.black, 0, 0);
        AddPiece(knight, Color.black, 1, 0);
        AddPiece(bishop, Color.black, 2, 0);
        AddPiece(queen, Color.black, 3, 0);
        AddPiece(king, Color.black, 4, 0);
        AddPiece(bishop, Color.black, 5, 0);
        AddPiece(knight, Color.black, 6, 0);
        AddPiece(rook, Color.black, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(pawn, Color.white, i, 1);
            AddPiece(pawn, Color.black, i, 6);
        }
    }

    public void AddPiece(GameObject @object, Color color, int col, int row)
    {
        Material mat = color == Color.white ? white : black;
        pieces.Add(new Vector2Int(col, row), board.AddPiece(@object, mat, col, row));
    }

    public GameObject PieceAtGrid(Vector2Int @vector)
    {
        if (vector.x > 7 || vector.y > 7 || vector.x < 0 || vector.y < 0)
            return null;

        return pieces.GetValueOrDefault(vector, null);
    }
    public void SelectPiece(GameObject @gameObject)
    {
       board.SelectPiece(gameObject);
    }

    public enum Color
    {
        white,
        black
    }
}
