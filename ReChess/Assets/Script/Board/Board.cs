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
    




    public GameObject AddPiece(GameObject piece,Material material, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
        newPiece.GetComponent<Renderer>().material = material;
        if (material == defaultBlack)
            newPiece.tag = "Black";
            
        else if (material == defaultWhite)
        {
            GameManager.Instance.playerWhite.Add(newPiece);
            newPiece.tag = "White";
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

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = defaultMaterial;
        if (piece.CompareTag("Black"))
            renderers.material = defaultBlack;
        else if (piece.CompareTag("White"))
            renderers.material = defaultWhite;
    }
}
