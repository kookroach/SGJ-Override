using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pawn : MonoBehaviour, IRule
{
    private bool hasMoved = false;
    public int forwardMovement = 1;
    public int lateralMovement = 1;
    public int startMovement = 2;

    public bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (key == null)
            return false;

        


        GameObject objectOnTarget;
        var isFree = !pieces.TryGetValue(target, out objectOnTarget);
        if (hasMoved)
        {
            if (target.y - key.y != forwardMovement && target.y - key.y != -forwardMovement)
                return false;

            if (target.x - key.x == lateralMovement || target.x - key.x == -lateralMovement)
            {
                if (!isFree && !objectOnTarget.CompareTag(this.tag))
                {
                    return true;
                }
                return false;
            }
        }
        else
        {
            if (target.y - key.y != startMovement && target.y - key.y != -startMovement)
                if (target.y - key.y != forwardMovement && target.y - key.y != -forwardMovement)
                    return false;
        }
        
        if (target.x - key.x == 0 && isFree)
            return true;

        return false;
    }

    public bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;

        GameManager.Instance.MoveToGrid(this.gameObject, target);
        if (!hasMoved)
            hasMoved = true;
        return true;
    }

    public void OnAttack(IPiece other)
    {
    }

    public void OnAttack()
    {
    }

    public void OnDestroy()
    {
       
    }
}
