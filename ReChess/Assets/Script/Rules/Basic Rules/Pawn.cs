using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : IRule
{
    private bool hasMoved = false;
    public bool OnAction(Vector2Int target) 
    {
        if (!hasMoved)
        {

        }

        return true;
    }

    public void OnAttack(IPiece piece, IPiece other)
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroy(IPiece piece)
    {
        throw new System.NotImplementedException();
    }

}
