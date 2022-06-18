using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Knight : ChessPiece, IRule
{

    public virtual bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (pieces[key] == null)
        {
            return false;
        }

        if ((Math.Abs(target.y - key.y) == 2 && Math.Abs(target.x - key.x) == 1) ||
            (Math.Abs(target.y - key.y) == 1 && Math.Abs(target.x - key.x) == 2))
            return true;
        return false;

    }
    public bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;
        
        return GameManager.Instance.MoveToGrid(this.gameObject, target);

    }

    public bool OnAttack(GameObject other)
    {
        if (other.CompareTag(this.gameObject.tag))
            return false;

        if (other.GetComponent<IRule>().OnDestroy())
        {
            Destroy(other);
            return true;
        }

        return false;
    }

    public bool OnDestroy()
    {
        return true;
    }
}
