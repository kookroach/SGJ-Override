using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour, IRule
{
    public virtual PieceMovement PieceMovement { get => _pieceMovement; set => _pieceMovement = value; }
    [SerializeField]
    private PieceMovement _pieceMovement;
    public virtual bool canJump => _pieceMovement.canJump;
    [SerializeField][Range(1, 10)] private float speed = 3;
    [HideInInspector]
    //false is black(blue), true is white(red) 
    public bool identifier = false; 


    public virtual (bool hasObstacle, Vector2Int obstaclePos) CanMoveToTarget(Vector2Int target)
    {
        var pieces = GameManager.pieces;
        Vector2Int origin = GameManager.Instance.GridAtPiece(gameObject);

        if (canJump)
        {
            var obj = GameManager.Instance.PieceAtGrid(target);
            if (obj != null)
                return (true, target);
            return (false, target);
        }

        if (pieces[origin] == null)
        {
            throw new System.ArgumentOutOfRangeException();
        }

        Vector2 dir = (target - origin);
        dir.Normalize();
        Vector2Int current = origin;
        int depth = 8;

        while (depth > 0)
        {
            if (current == target)
                return (false, target);

            current = (current + Vector2Int.RoundToInt(dir));
            var obj = GameManager.Instance.PieceAtGrid(current);
            if (obj != null)
            {
                return (true, current);
            }

            depth--;
        }


        return (true, current);
    }

    public virtual bool OnAction(Vector2Int target)
    {
        
        StartCoroutine(move(new Vector3(target.x, transform.position.y, target.y)));
        GameManager.Instance.MoveToGrid(gameObject, target);
        return true;
    }

    public virtual bool CanAttack(Vector2Int target)
    {
        return !GameManager.Instance.PieceAtGrid(target).CompareTag(gameObject.tag);
    }

    public virtual bool OnAttack(GameObject other)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public virtual IEnumerator WaitForDeath(GameObject other)
    {
        yield return new WaitForSeconds(3);
        Destroy(other);
    }

    public virtual IEnumerator move(Vector3 target)
    {
        var orig = transform.rotation;
        while (Vector3.Distance(transform.position, target) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            yield return new WaitForEndOfFrame();

        }
        transform.transform.rotation = orig;
        transform.position = target;
        GetComponent<Animator>().SetTrigger("Idle");
        yield return null;

    }
}
