using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class KamikazeBishop : Bishop
{
    //public override bool CanMoveToTarget(Vector2Int target)
    //{
    //    var pieces = GameManager.pieces;
    //    var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

    //    if (pieces[key] == null)
    //    {
    //        return false;
    //    }
        
    //    //normal bishop movement
    //    if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.y - key.y) == Math.Abs(target.x - key.x) && IRule.amountOfRaycastHits(key, target) == 0)
    //    {
    //        return true;
    //    }
        
    //    //take enemy piece(s)
    //    if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.y - key.y) == Math.Abs(target.x - key.x) && IRule.amountOfRaycastHits(key, target) > 0)
    //    {
            
    //        RaycastHit[] hits = IRule.raycastHits(key, target);
    //        foreach (var hit in hits)
    //        {
    //            Destroy(hit.collider.gameObject);
    //        }
    //        Destroy(this.gameObject);
    //    }
        
            
            

    //    return false;
    //}

    //public override bool OnAction(Vector2Int target)
    //{
    //    return base.OnAction(target);
    //}

    //public override bool OnAttack(GameObject other)
    //{
    //    return base.OnAttack(other);
    //}
}
