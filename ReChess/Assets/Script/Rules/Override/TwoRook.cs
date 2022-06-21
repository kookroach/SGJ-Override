using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwoRook : Rook
{
    //public override bool CanMoveToTarget(Vector2Int target)
    //{
    //    var pieces = GameManager.pieces;
    //    var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

    //    if (pieces[key] == null)
    //    {
    //        return false;
    //    }
        
    //    //forward(/backward) movement
    //    if ((Math.Abs(target.y - key.y)  % 2 == 0 && (target.x - key.x == 0)))
    //    {
    //        return IRule.RaycastBoard(key, target);
    //    }
            

    //    //lateral movement
    //    if ((Math.Abs(target.x - key.x)  % 2 == 0 && (target.y - key.y == 0)))
    //    {
    //        return IRule.RaycastBoard(key, target);
    //    }
    
        
    //    return false;
    //}
}
