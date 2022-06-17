using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : IRule
{
    private bool hasMoved = false;

    public bool CanMoveToTarget(Vector2Int target)
    {

        return true;
    }

    public bool OnAction(Vector2Int target) 
    {
        if (!hasMoved)
        {

        }

        return true;
    }

    public void OnAttack(IPiece other)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroy(IPiece other)
    {
        throw new System.NotImplementedException();
    }
}
