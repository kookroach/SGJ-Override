using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ZombieQueen : Queen
{
    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        Vector2 key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }


        //forward(/backward) movement
        if ((Math.Abs(target.y - key.y) <= forwardMovement && (target.x - key.x == 0)) && !pieces.ContainsKey(target))
        {
            return IRule.RaycastBoard(key, target);
        }


        //lateral movement
        if (Math.Abs(target.x - key.x) <= lateralMovement && (target.y - key.y == 0) && !pieces.ContainsKey(target))
        {
            return IRule.RaycastBoard(key, target);
        }


        //covers diagonal movement
        if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.y - key.y) == Math.Abs(target.x - key.x) &&
            !pieces.ContainsKey(target))
        {
            return IRule.RaycastBoard(key, target);
        }

        //take enemy piece forward(/backward)
        if ((Math.Abs(target.y - key.y) <= forwardMovement && (target.x - key.x == 0)) && pieces.ContainsKey(target) &&
            IRule.RaycastBoard(key, target))
        {
            return false;
        }

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