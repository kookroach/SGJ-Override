using System;
using UnityEngine;
using System.Linq;


public class Pawn : ChessPiece, IRule
{
    public bool hasMoved = false;
    public virtual int forwardMovement { get; protected set; } = 1;
    public virtual int lateralMovement { get; protected set; } = 0;
    public int startMovement = 2;

    private void Start()
    {
        
        if (this.gameObject.CompareTag("Black"))
        {
            forwardMovement *= -1;
            startMovement *= -1;
        }
    }
    public virtual bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }
        //go two squares forward
        if (!hasMoved && (target.y - key.y == startMovement) && !pieces.ContainsKey(target) &&
            target.x - key.x == lateralMovement && !pieces.ContainsKey(new Vector2(target.x, target.y - 1)))
        {
            return true;
        }
            
        //usual pawn movement
        if (target.y - key.y == forwardMovement && target.x - key.x == lateralMovement && !pieces.ContainsKey(target))
            return true;
        //take enemy piece
        if (target.y - key.y == forwardMovement && Math.Abs(target.x - key.x) == (lateralMovement + 1) &&
            pieces.ContainsKey(target))
            return true;
        
        //TODO: en passant
        

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