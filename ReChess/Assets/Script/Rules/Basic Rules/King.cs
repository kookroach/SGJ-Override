using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : ChessPiece, IRule
{
    public int forwardMovement = 1;
    public int lateralMovement = 1;
    public bool hasMoved;
    
    private AudioManager dj;

    private void Start()
    {
        dj = FindObjectOfType<AudioManager>();
    }

    public bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (key == null)
            return false;

        //standard movement
        if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.x - key.x) <= lateralMovement)
        {
            dj.Play("king");
        }
            return true;
        
        //castling logic
        if (!hasMoved && Math.Abs(target.x - key.x) == lateralMovement + 1 && target.y - key.y == 0)
        {
            if (IRule.RaycastBoard(key, target))
            {
                if (this.gameObject.CompareTag("White"))
                {
                    Component _;
                    if (target.x - key.x == - lateralMovement - 1 && GameManager.Instance.PieceAtGrid(new Vector2Int(0, 0)).TryGetComponent(typeof(Rook),out _))
                    {
                        GameManager.Instance.PieceAtGrid(new Vector2Int(0, 0)).GetComponent<IRule>().OnAction(new Vector2Int(3, 0));
                        return true;
                    }
                    if (target.x - key.x == lateralMovement + 1 && GameManager.Instance.PieceAtGrid(new Vector2Int(7, 0)).TryGetComponent(typeof(Rook),out _))
                    {
                        GameManager.Instance.PieceAtGrid(new Vector2Int(7, 0)).GetComponent<IRule>().OnAction(new Vector2Int(5, 0));
                        return true;
                    }
                }

                if (this.gameObject.CompareTag("Black"))
                {
                    Component _;
                    if (target.x - key.x == - lateralMovement - 1 && GameManager.Instance.PieceAtGrid(new Vector2Int(0, 7)).TryGetComponent(typeof(Rook),out _))
                    {
                        GameManager.Instance.PieceAtGrid(new Vector2Int(0, 7)).GetComponent<IRule>().OnAction(new Vector2Int(3, 7));
                        return true;
                    }
                    if (target.x - key.x == lateralMovement + 1 && GameManager.Instance.PieceAtGrid(new Vector2Int(7, 7)).TryGetComponent(typeof(Rook),out _))
                    {
                        GameManager.Instance.PieceAtGrid(new Vector2Int(7, 7)).GetComponent<IRule>().OnAction(new Vector2Int(5, 7));
                        return true;
                    }
                }
            }
        }

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
            dj.Play("attack");
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