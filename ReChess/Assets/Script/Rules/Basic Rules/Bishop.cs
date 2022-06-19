using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bishop : ChessPiece, IRule
{
    public int forwardMovement = 8;
    public int lateralMovement = 8;

    public virtual bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (pieces[key] == null)
        {
            return false;
        }
        
        if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.y - key.y) == Math.Abs(target.x - key.x))
        {
            return IRule.RaycastBoard(key, target);
        }

        return false;
        
    }
    public virtual bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;

        FxManager.Instance.CreateSFX(this.gameObject, FxManager.SFX_TYPE.Bishop);
        return GameManager.Instance.MoveToGrid(this.gameObject, target);

    }

    public virtual bool OnAttack(GameObject other)
    {
        if (other.CompareTag(this.gameObject.tag))
            return false;

        if (other.GetComponent<IRule>().OnDestroy())
        {
            StartCoroutine(WaitForDeath(other));
            return true;
        }

        return false;
    }

    public virtual bool OnDestroy()
    {
        FxManager.Instance.CreateSFX(this.gameObject, FxManager.SFX_TYPE.Clash);
        return true;
    }

}
