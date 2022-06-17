using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IRule 
{
    public bool CanMoveToTarget(Vector2Int target);
    public bool OnAction(Vector2Int target);
    public void OnDestroy(IPiece other);
    public void OnAttack();

}