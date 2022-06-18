using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : ChessPiece, IRule
{
    public int forwardMovement = 10;
    public int lateralMovement = 10;

    public virtual bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (pieces[key] == null)
        {
            return false;
        }
        
        //forward(/backward) movement
        if ((Math.Abs(target.y - key.y) <= forwardMovement && (target.x - key.x == 0)))
        {
            return IRule.RaycastBoard(key, target);
        }
            

        //lateral movement
        if (Math.Abs(target.x - key.x) <= lateralMovement && (target.y - key.y == 0))
        {
            return IRule.RaycastBoard(key, target);
        }
    
        
        return false;
    }
    public virtual bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;
        
        return GameManager.Instance.MoveToGrid(this.gameObject, target);

    }

    public virtual bool OnAttack(GameObject other)
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

    public virtual bool OnDestroy()
    {
        return true;
    }
}
