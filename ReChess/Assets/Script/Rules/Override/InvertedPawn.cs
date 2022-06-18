using System;
using System.Linq;
using UnityEngine;

public class InvertedPawn : Pawn
{
    public override int forwardMovement { get; protected set; } = 0;
    public override int lateralMovement { get; protected set; } = 1;

    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }
        //go two squares diagonal
        if (!hasMoved && (target.y - key.y == startMovement) && pieces.ContainsKey(target) && 
            Math.Abs(target.x - key.x) == (lateralMovement + 1) && pieces.ContainsKey(new Vector2(target.x, target.y - 1)))
            return true;


        //diagonal movement
        if (target.y - key.y == forwardMovement && Math.Abs(target.x - key.x) == (lateralMovement + 1) &&
            !pieces.ContainsKey(target))
            return true;
        //usual pawn movement
        if (target.y - key.y == forwardMovement && target.x - key.x == lateralMovement && pieces.ContainsKey(target))
            return true;
     
        //TODO: en passant

        return false;
    }
}
