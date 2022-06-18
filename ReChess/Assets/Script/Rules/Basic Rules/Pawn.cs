using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pawn : MonoBehaviour, IRule
{
    private bool hasMoved = false;
    public int forwardMovement => hasMoved ? 1 : 2;
    public int lateralMovement = 1;

    public bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.Instance.pieces;
        var key = pieces.FirstOrDefault(x => x.Value == this).Key;
        

        if (key == null)
            return false;

        if (target.x - key.x != forwardMovement && target.x - key.x != -forwardMovement)
            return false;


        GameObject objectOnTarget;
        var isFree = pieces.TryGetValue(target, out objectOnTarget);
        if (hasMoved)
        {
            if (target.y - key.y == lateralMovement || target.y - key.y == -lateralMovement)
            {
                if (!isFree && !objectOnTarget.CompareTag(this.tag))
                {
                    return true;
                }
                return false;
            }
        }

        if (target.y - key.y == 0 && isFree)
            return true;

        return false;
    }

    public bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;

        this.gameObject.transform.position = new Vector3(target.x, gameObject.transform.position.y, target.y);

        if(!hasMoved)
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
        //throw new System.NotImplementedException();
    }

    public void OnDestroy(IPiece other)
    {
    }
}
