using System;
using UnityEngine;
using System.Linq;


public class Pawn : PieceBehaviour
{
    public override string ToString() => "p";

    public override (bool hasObstacle, Vector2Int obstaclePos) CanMoveToTarget(Vector2Int target){
        //if lateral movement give false positive as if there is a obstacle
        if(target.x != Mathf.RoundToInt(transform.position.x))
            return (true, target);

        //if cannot double move give false positive to a vector where no Piece will exist
        if(Math.Abs(target.y - Mathf.RoundToInt(transform.position.z)) > 1 && !CanDoubleMove())
            return (true, new Vector2Int(-1,-1));

        //if move is legal do normal obstacle check
        return base.CanMoveToTarget(target);
    }

    public override bool CanAttack(Vector2Int target){
        //if target in front of pawn return false
        if(target.x == Mathf.RoundToInt(transform.position.x))
            return false;
        
        return base.CanAttack(target);
    }
    private bool CanDoubleMove(){
        if(gameObject.CompareTag("Black")){
            if(Mathf.RoundToInt(transform.position.z) == 6){
                return true;
            }
        }else if(Mathf.RoundToInt(transform.position.z) == 1){
                return true;
        }

        return false;
    }

    public override bool OnAction(Vector2Int target)
    {
        var returnBool = base.OnAction(target);

        if (Math.Abs(target.y - Mathf.RoundToInt(transform.position.z)) > 1)
            GameManager.Instance.GetBoard().enPassant = AlgebraicReader.GridToAlgebraic(new Vector2Int(target.x, target.y - 1));

        return returnBool; 
    }
}