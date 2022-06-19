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
        Vector2Int key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;

        
        ;
        if (pieces[key] == null)
        {
            return false;
        }
        
        


        //forward(/backward) movement
        if ((Math.Abs(target.y - key.y) <= forwardMovement && (target.x - key.x == 0)))
        {
            return IRule.RaycastBoard(key, target);
        }


        //lateral movement
        if (Math.Abs(target.x - key.x) <= lateralMovement && (target.y - key.y == 0))
        {
            return IRule.RaycastBoard(key, target);
        }


        //covers diagonal movement
        if (Math.Abs(target.y - key.y) <= forwardMovement && Math.Abs(target.y - key.y) == Math.Abs(target.x - key.x))
        {
            return IRule.RaycastBoard(key, target);
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
            GameObject othercopy = other;
            
            switch (other.GetComponent<IRule>())
            {
                case Rook:
                    Destroy(other);
                    GameManager.pieces.Remove(new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    GameManager.Instance.AddPiece(GameManager.Instance.rookRed,Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z));
                    GameManager.Instance.CheckMove(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x),Mathf.RoundToInt(gameObject.transform.position.z)),new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    return false;
                   
                case Knight:
                    Destroy(other);
                    GameManager.pieces.Remove(new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    GameManager.Instance.AddPiece(GameManager.Instance.knightRed,Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z));
                    GameManager.Instance.CheckMove(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x),Mathf.RoundToInt(gameObject.transform.position.z)),new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));

                    return false;

                case Bishop:
                    Destroy(other);
                    GameManager.pieces.Remove(new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    GameManager.Instance.AddPiece(GameManager.Instance.bishopRed,Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z));
                    GameManager.Instance.CheckMove(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x),Mathf.RoundToInt(gameObject.transform.position.z)),new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));

                    return false;

                case Queen:
                    Destroy(other);
                    GameManager.pieces.Remove(new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    GameManager.Instance.AddPiece(GameManager.Instance.queenRed,Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z));
                    GameManager.Instance.CheckMove(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x),Mathf.RoundToInt(gameObject.transform.position.z)),new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));

                    return false;
                case Pawn:
                    Destroy(other);
                    GameManager.pieces.Remove(new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));
                    GameManager.Instance.AddPiece(GameManager.Instance.pawnRed,Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z));
                    GameManager.Instance.CheckMove(new Vector2Int(Mathf.RoundToInt(gameObject.transform.position.x),Mathf.RoundToInt(gameObject.transform.position.z)),new Vector2Int(Mathf.RoundToInt(othercopy.gameObject.transform.position.x),Mathf.RoundToInt(othercopy.gameObject.transform.position.z)));

                    return false;
                default:
                    return false;
            }
            
            return false;
        }
       

        return false;
    }

    public bool OnDestroy()
    {
        return true;
    }
}