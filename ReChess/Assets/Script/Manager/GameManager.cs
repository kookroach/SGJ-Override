using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    
    public GameObject pawnRed;
    public GameObject pawnBlue;

    public GameObject knightRed;
    public GameObject knightBlue;

    public GameObject rookRed;
    public GameObject rookBlue;

    public GameObject bishopRed;
    public GameObject bishopBlue;

    public GameObject queenRed;
    public GameObject queenBlue;

    public GameObject kingRed;
    public GameObject kingBlue;
    
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;


    public List<GameObject> playerWhite = new List<GameObject>();

    public LayoutData layoutData;
    public LayoutData.CardSelect cardSelect;
    public List<LayoutData.Moves> PlayerMoves = new List<LayoutData.Moves>();
 
    public bool DEBUG;
    public string currentFen;

    public void Awake()
    {
        if(DEBUG){
            FenReader.LoadPositionFromFen(FenReader.startFEN);
            currentFen = FenReader.startFEN;
            return;
        }


        //Set Layout
       foreach(var piece in layoutData.layouts)
       {
            AddPiece(piece.ChessPiece, (int)piece.vector.x, (int)piece.vector.y);
            
       }

       
       
        //Set Cards
        //button1.GetComponent<Button>().onClick.AddListener(SelectCard);
        button1.GetComponentInChildren<TextMeshProUGUI>().text = layoutData.card1.Description;
        
        button2.GetComponentInChildren<TextMeshProUGUI>().text = layoutData.card2.Description;

        button3.GetComponentInChildren<TextMeshProUGUI>().text = layoutData.card3.Description;

        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);

        FxManager.Instance.CreateSFX(this.gameObject, FxManager.SFX_TYPE.CheezySlow, true, false);
    }

    public void AddPiece(GameObject @object, int col, int row)
    {
        board.AddPiece(@object, col, row);
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
        board.MovePiece(@object, target);
    }

    public void GetPossibleMoves(GameObject @object)
    {
        board.PossibleMoves(@object);
    }

    public bool GetTurn() => board.isWhiteMove;
    public bool SetTurn(bool value) => board.isWhiteMove = value;

    public Board GetBoard() => board;
}
