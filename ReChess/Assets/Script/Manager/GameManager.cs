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

    public static Dictionary<Vector2Int, GameObject> pieces = new Dictionary<Vector2Int, GameObject>();

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

        playerWhite.Clear();
        pieces.Clear();
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


        /*
       //pieces.Add(new Vector2(69,69), null);
        AddPiece(rookRed, 0, 0);
        AddPiece(knightRed,  1, 0);
        AddPiece(bishopRed,  2, 0);
        AddPiece(queenRed,  3, 0);
        AddPiece(kingRed,  4, 0);
        AddPiece(bishopRed, 5, 0);
        AddPiece(knightRed,  6, 0);
        AddPiece(rookRed,  7, 0);

        AddPiece(rookBlue, 0, 7);
        AddPiece(knightBlue, 1, 7);
        AddPiece(bishopBlue, 2, 7);
        AddPiece(queenBlue,3, 7);
        AddPiece(kingBlue,  4, 7);
        AddPiece(bishopBlue, 5, 7);
        AddPiece(knightBlue,  6, 7);
        AddPiece(rookBlue,  7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(pawnRed,  i, 1);
            AddPiece(pawnBlue, i, 6);
        }
        */

        FxManager.Instance.CreateSFX(this.gameObject, FxManager.SFX_TYPE.CheezySlow, true, false);
    }

    public void AddPiece(GameObject @object, int col, int row)
    {
        pieces.Add(new Vector2Int(col, row), board.AddPiece(@object, col, row));
    }

    public GameObject PieceAtGrid(Vector2Int @vector)
    {
        if (vector.x > 7 || vector.y > 7 || vector.x < 0 || vector.y < 0)
            return null;

        return pieces.GetValueOrDefault(vector, null);
    }

    public Vector2Int GridAtPiece(GameObject @object)
    {

        return pieces.Where(x => x.Value == @object).Select(x => x.Key).FirstOrDefault();
    }


    public void MoveToGrid(GameObject @object, Vector2Int target)
    {
        pieces.Remove(GridAtPiece(@object));
        pieces[target] = @object;      
    }

    public void CheckMove(Vector2Int key, Vector2 target)
    {
        var possibilities = PlayerMoves.Where(x => x.from == key && x.to == target).FirstOrDefault();

        if (possibilities is null && (PlayerMoves.Count != 0))
        {
            playerWhite.Clear();
            pieces.Clear();
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
        PlayerMoves.Remove(possibilities);
        if (PlayerMoves.Count == 0)
        {
            playerWhite.Clear();
            pieces.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //You WON
        }

        if (playerWhite.Contains(PieceAtGrid(key)))
        {
            PieceAtGrid(PlayerMoves.First().from).GetComponent<IRule>().OnAction(PlayerMoves.First().to);
            PlayerMoves.Remove(PlayerMoves.First());

        }
        }
    
    public void GetPossibleMoves(GameObject @object)
    {
        board.PossibleMoves(@object, GridAtPiece(@object));
    }

    public enum Color
    {
        white,
        black
    }

    
    

}
