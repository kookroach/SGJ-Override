using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] [Range(1, 10)] private float speed;
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
    /*
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
    */
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public static Dictionary<Vector2Int, GameObject> pieces = new Dictionary<Vector2Int, GameObject>();

    public List<GameObject> playerWhite = new List<GameObject>();

    public LayoutData layoutData;
    public LayoutData.CardSelect cardSelect;
    private List<LayoutData.Moves> PlayerMoves = new List<LayoutData.Moves>();
 

    public void Start()
    {
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
        PlayerMoves = layoutData.playerMoves.Where(x => x.card == cardSelect).ToList();


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
    

    public bool MoveToGrid(GameObject @object, Vector2Int target)
    {
        Vector2Int key = pieces.Where(x => x.Value == @object).FirstOrDefault().Key;
        if(pieces.ContainsKey(target))
        {
            GameObject objectOnTarget = GameManager.pieces[target];
            if (!@object.GetComponent<IRule>().OnAttack(objectOnTarget))
            {
                return false;
            }
            objectOnTarget.GetComponent<Animator>().SetTrigger("OnAttack");
        }

        CheckMove(key,target);

        pieces.Remove(key);
        StartCoroutine(muve(@object,new Vector3(target.x, @object.transform.position.y, target.y)));
        @object.GetComponent<Animator>().SetTrigger("OnAction");
        pieces[target] = @object;
        
        return true;
    }

    private void CheckMove(Vector2Int key,Vector2 target)
    {
        var possibilities = PlayerMoves.Where(x => x.from == key && x.to == target).FirstOrDefault();

        if (possibilities is null)
        {
            Application.LoadLevel(Application.loadedLevel);
            playerWhite.Clear();
            pieces.Clear();
        }

        PlayerMoves.Remove(possibilities);
        if (PlayerMoves.Count == 0) return; //WON

        if (playerWhite.Contains(PieceAtGrid(key)))
        {
            PieceAtGrid(PlayerMoves.First().from).GetComponent<IRule>().OnAction(PlayerMoves.First().to);
            PlayerMoves.Remove(PlayerMoves.First());

            if (PlayerMoves.Count == 0) return; //You WON
        }
    }

    public enum Color
    {
        white,
        black
    }

    IEnumerator muve(GameObject obj, Vector3 target)
    {
        var orig = obj.transform.rotation;
        while (Vector3.Distance(obj.transform.position, target) > 0.2f)
        {
           obj.transform.position =  Vector3.MoveTowards(obj.transform.position,  target, speed * Time.deltaTime);
            obj.transform.LookAt(new Vector3(target.x, obj.transform.position.y, target.z));
            yield return new WaitForEndOfFrame();
            
        }
        obj.transform.transform.rotation = orig; 
        obj.transform.position = target;
        obj.GetComponent<Animator>().SetTrigger("Idle");
        yield return null;

    }
}
