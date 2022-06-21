using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRule
{
    public (bool hasObstacle, Vector2Int obstaclePos) CanMoveToTarget(Vector2Int target);
    public bool OnAction(Vector2Int target);
    public bool OnDestroy();
    public bool OnAttack(GameObject other);
    public bool CanAttack(Vector2Int target);

    static int amountOfRaycastHits(Vector2 origin, Vector2 target)
    {
        return raycastHits(origin, target).Length;
    }

    static RaycastHit[] raycastHits(Vector2 origin, Vector2 target)
    {
        RaycastHit[] hits;
        float distance = Vector3.Distance(origin, target);
        hits = Physics.RaycastAll(new Vector3(origin.x, 1, origin.y),
            new Vector3(target.x, 1, target.y) - new Vector3(origin.x, 1, origin.y), distance);
        return hits;
    }
    
}