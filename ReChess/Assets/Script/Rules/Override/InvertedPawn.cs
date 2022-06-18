using System;
using System.Linq;
using UnityEngine;

public class InvertedPawn : Pawn
{
    public override int forwardMovement { get; protected set; } = 1;
    public override int lateralMovement { get; protected set; } = 0;

    public override bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;


        if (pieces[key] == null)
        {
            return false;
        }
        //go two squares diagonal
        if (!hasMoved && (target.y - key.y == startMovement) && (Math.Abs(target.x - key.x) == startMovement) && !pieces.ContainsKey(target) && !pieces.ContainsKey(new Vector2(target.x - 1, target.y - 1)) && !pieces.ContainsKey(new Vector2(target.x + 1, target.y - 1)))
            return true;


        //"standard" diagonal movement
        if (target.y - key.y == forwardMovement && Math.Abs(target.x - key.x) == (lateralMovement + 1) &&
            !pieces.ContainsKey(target))
            return true;
        //take enemy piece
        if (target.y - key.y == forwardMovement && target.x - key.x == lateralMovement && pieces.ContainsKey(target))
            return true;
     
        //TODO: en passant

        return false;
    }
}
