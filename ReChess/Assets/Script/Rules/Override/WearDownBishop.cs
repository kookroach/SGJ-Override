using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WearDownBishop : Bishop
{
    public GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(true);
    }

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
            
            if(IRule.RaycastBoard(key, target))
            {
                if (forwardMovement != 0)
                {
                    forwardMovement--;
                }
                return true;
            }
        }

        return false;
    }

    
}
