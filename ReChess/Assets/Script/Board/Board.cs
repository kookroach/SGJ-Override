using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Material defaultMaterial;
    public Material defaultWhite;
    public Material defaultBlack;
    public Material selectedMaterial;
    




    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        GameObject newPiece = Instantiate(piece,new Vector3(col, piece.transform.position.y, row), piece.transform.rotation, gameObject.transform);
            
        if (newPiece.CompareTag("White"))
        {
            GameManager.Instance.playerWhite.Add(newPiece);
        }
            
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void PossibleMoves(GameObject @object, Vector2Int gridPoint)
    {
        GetComponent<MoveSelector>().SetPossibleMoves(@object.GetComponent<ChessPiece>().PieceMovement.movement, @object);
    }
}
