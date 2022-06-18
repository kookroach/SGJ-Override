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

    private void Start()
    {
        if(this.gameObject.CompareTag("Black"))
        {
            forwardMovement *= -1;
            startMovement *= -1;
        }
    }
    public bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (key == null)
            return false;

        
        if (hasMoved)
        {
            if (target.y - key.y != forwardMovement)
                return false;

            if ((target.x - key.x == lateralMovement || target.x - key.x == -lateralMovement) && pieces.ContainsKey(target))
            {
                return true;
            }
        }
        else
        {
            if (target.y - key.y != startMovement)
                if (target.y - key.y != forwardMovement)
                    return false;
        }
        
        if (target.x - key.x == 0 && !pieces.ContainsKey(target))
            return true;

        return false;
    }

    public bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;

        if (!hasMoved)
            hasMoved = true;
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
