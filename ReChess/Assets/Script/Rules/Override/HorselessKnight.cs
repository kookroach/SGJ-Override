using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HorselessKnight : Knight
{
    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }

        //forward movement
        if ((target.y - key.y == 2 && Math.Abs(target.x - key.x) == 1) &&
            !pieces.ContainsKey(new Vector2Int(key.x, key.y + 1)))
        {
            return true;
        }

        //backward movement
        if ((target.y - key.y == -2 && Math.Abs(target.x - key.x) == 1) &&
            !pieces.ContainsKey(new Vector2Int(key.x, key.y - 1)))
        {
            return true;
        }

        //left movement
        if ((target.x - key.x == 2 && Math.Abs(target.y - key.y) == 1) &&
            !pieces.ContainsKey(new Vector2Int(key.x + 1, key.y)))
        {
            return true;
        }

        //right movement
        if ((target.x - key.x == -2 && Math.Abs(target.y - key.y) == 1) &&
            !pieces.ContainsKey(new Vector2Int(key.x - 1, key.y)))
        {
            return true;
        }


        return false;
    }

    public override bool OnAction(Vector2Int target)
    {
        if (!CanMoveToTarget(target))
            return false;

        return GameManager.Instance.MoveToGrid(this.gameObject, target);
    }

    public override bool OnAttack(GameObject other)
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

    public override bool OnDestroy()
    {
        return true;
    }
}