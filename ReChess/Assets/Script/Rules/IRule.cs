using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRule
{
    public bool CanMoveToTarget(Vector2Int target);
    public bool OnAction(Vector2Int target);
    public bool OnDestroy();
    public bool OnAttack(GameObject other);

    static bool RaycastBoard(Vector2 origin, Vector2 target)
    {
        RaycastHit hit;
        float distance = Vector3.Distance(origin, target);


        Physics.Raycast(new Vector3(origin.x, 1, origin.y),
            new Vector3(target.x, 1, target.y) - new Vector3(origin.x, 1, origin.y), out hit, distance);

        int x = Mathf.RoundToInt(hit.point.x);
        int z = Mathf.RoundToInt(hit.point.z);
        Debug.Log(x + " " + z);
        Debug.Log(target);
        if (x == target.x && z == target.y)
        {
            return true;
        }


        if (!Physics.Raycast(new Vector3(origin.x, 1, origin.y),
            new Vector3(target.x, 1, target.y) - new Vector3(origin.x, 1, origin.y), distance))
        {
            return true;
        }

        return false;
    }

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