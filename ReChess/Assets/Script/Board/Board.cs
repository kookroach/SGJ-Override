using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> pieces = new Dictionary<Vector2Int, GameObject>();
    [HideInInspector]
    public bool isWhiteTurn;

    [HideInInspector]
    public bool canWhiteQueenSideCastling = true;
    [HideInInspector]
    public bool canWhiteKingSideCastling = true;
    [HideInInspector]
    public bool canBlackQueenSideCastling = true;
    [HideInInspector]
    public bool canBlackKingSideCastling = true;
    [HideInInspector]
    public string enPassant = "-";

    [HideInInspector]
    public int fullmove;
    [HideInInspector]
    public int halfmove;
    private string currentFEN;

    public void AddPiece(GameObject @object, int col, int row, bool isWhite)
    {
        GameObject newPiece = Instantiate(@object, new Vector3(col, @object.transform.position.y, row), @object.transform.rotation, gameObject.transform);
            
        if (isWhite)
        {
            newPiece.tag = "White";
            newPiece.GetComponent<PieceBehaviour>().white.SetActive(true);
            GameManager.Instance.playerWhite.Add(newPiece);
        }
        else
        {
            newPiece.tag = "Black";
            newPiece.transform.eulerAngles = new Vector3(0, 180, 0);
            newPiece.GetComponent<PieceBehaviour>().black.SetActive(true);
            GameManager.Instance.playerBlack.Add(newPiece);
        }

        pieces.Add(new Vector2Int(col, row), newPiece);
    }

    public void LoadPos(string FEN)
    {
        currentFEN = FEN;
        FenReader.LoadPositionFromFen(FEN);
    }

    public void RemovePiece(GameObject @object)
    {
        Destroy(@object);
    }

    public void MovePiece(GameObject @object, Vector2Int target)
    {
        if(isWhiteTurn)
            fullmove++;
        
        //TODO: Set Halfmove

        @object.GetComponent<PieceBehaviour>().OnAction(target);
        pieces.Remove(GridAtPiece(@object));
        pieces[target] = @object;
    }

    public void PossibleMoves(GameObject @object)
    {
        GetComponent<MoveSelector>().SetPossibleMoves(@object.GetComponent<PieceBehaviour>().PieceMovement.movement, @object);
    }

    public Vector2Int GridAtPiece(GameObject @object)
    {
        return pieces.Where(x => x.Value == @object).Select(x => x.Key).FirstOrDefault();
    }

    public GameObject PieceAtGrid(Vector2Int vector)
    {
        if (vector.x > 7 || vector.y > 7 || vector.x < 0 || vector.y < 0)
            return null;

        return pieces.GetValueOrDefault(vector, null);
    }

    public List<GameObject> GetPiecesOfType(Type type)
    {
        return pieces.Where(x => x.Value.GetComponent(type) != null).Select(x => x.Value).ToList();
    }
}
