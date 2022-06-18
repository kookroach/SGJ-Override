using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queen : MonoBehaviour, IRule
{
    public int forwardMovement = 1;
    public int lateralMovement = 1;

    public bool CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        var key = pieces.Where(x => x.Value == this.gameObject).FirstOrDefault().Key;
        

        if (key == null)
            return false;
        
        return false;
    }
    public bool OnAction(Vector2Int target) 
    {
        if (!CanMoveToTarget(target))
            return false;
        
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
