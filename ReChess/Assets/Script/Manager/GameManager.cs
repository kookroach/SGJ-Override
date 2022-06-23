using System;
using System.Collections.Generic;
using TMPro;
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
                GameObject manager = GameObject.Find("GameManager");
                _instance = manager.GetComponent<GameManager>();
                
            }
            return _instance;
        }
    }

   
    public Board board;
    
    public GameObject pawn;
    public GameObject knight;
    public GameObject rook;
    public GameObject bishop;
    public GameObject queen;
    public GameObject king;


    public List<GameObject> playerWhite = new List<GameObject>();
    public List<GameObject> playerBlack = new List<GameObject>();

    public List<string> moveHistory = new List<string>();
 
    public bool DEBUG;

    public void Awake()
    {
        if (DEBUG)
        {
            board.LoadPos(FenReader.startFEN);
            return;
        }

        board.LoadPos(FenReader.startFEN);
        //TODO: Start Game Logic (if needed)

        FxManager.Instance.CreateSFX(this.gameObject, FxManager.SFX_TYPE.CheezySlow, true, false);
    }

    public void AddPiece(GameObject @object, int col, int row, bool isWhite)
    {
        board.AddPiece(@object, col, row, isWhite);
    }

    public GameObject PieceAtGrid(Vector2Int @vector)
    {
        return board.PieceAtGrid(vector);
    }

    public Vector2Int GridAtPiece(GameObject @object)
    {
        return board.GridAtPiece(@object);
    }

    public List<GameObject> GetPiecesOfType(Type type)
    {
        return board.GetPiecesOfType(type);
    }

    public void MoveToGrid(GameObject @object, Vector2Int target)
    {
        SetTurn(!GetTurn());
        board.MovePiece(@object, target);
    }

    public void GetPossibleMoves(GameObject @object)
    {
        board.PossibleMoves(@object);
    }

    public bool GetTurn() => board.isWhiteTurn;
    public bool SetTurn(bool value) => board.isWhiteTurn = value;

    public Board GetBoard() => board;
}
