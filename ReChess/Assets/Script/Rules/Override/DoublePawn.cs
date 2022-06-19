using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoublePawn : Pawn
{
    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }
        //go two squares forward
        if ( !pieces.ContainsKey(target) &&
            target.x - key.x == lateralMovement && !pieces.ContainsKey(new Vector2(target.x, target.y - 1)))
        {
            return true;
        }
        
        if (target.y - key.y == forwardMovement && Math.Abs(target.x - key.x) == (lateralMovement + 1) &&
            pieces.ContainsKey(target))
            return true;

        return false;
    }
}

