using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BouncyBishop : Bishop
{
    public override bool CanMoveToTarget(Vector2Int target)
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

    public override bool OnAction(Vector2Int target)
    {
        return base.OnAction(target);
    }

    public override bool OnAttack(GameObject other)
    {
        return base.OnAttack(other);
    }
}
