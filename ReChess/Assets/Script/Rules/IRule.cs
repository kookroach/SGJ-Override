using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IRule 
{
    public bool CanMoveToTarget(Vector2Int target);
    public bool OnAction(IPiece piece, List<Vector2Int> locations);
    public void OnDestroy(IPiece piece, IPiece other);
    public void OnAttack(IPiece piece);

}
