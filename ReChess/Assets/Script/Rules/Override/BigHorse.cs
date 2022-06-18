using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BigHorse : Knight
{
    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (pieces[key] == null)
        {
            return false;
        }
        if ((Math.Abs(target.y - key.y) == 4 && Math.Abs(target.x - key.x) == 3) ||
            (Math.Abs(target.y - key.y) == 3 && Math.Abs(target.x - key.x) == 4))
            return true;
        return false;
        
    }
}
