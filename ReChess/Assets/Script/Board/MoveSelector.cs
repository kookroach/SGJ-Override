using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MoveSelector : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject attackLocationPrefab;

    private GameObject _tileHighlight;

    private GameObject _movingPiece;

    private Dictionary<Vector2Int, GameObject> allMoveHighlights = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, GameObject> allAttackHighlights = new Dictionary<Vector2Int, GameObject>();


    public void EnterState(GameObject piece)
    {
        _movingPiece = piece;
        this.enabled = true;

        if ((allAttackHighlights == null || allAttackHighlights.Count() == 0) && (allMoveHighlights == null || allMoveHighlights.Count() == 0))
            SetPossibleMoves(_movingPiece.GetComponent<PieceBehaviour>().PieceMovement.movement, _movingPiece);
    }

    private void Start()
    {
        this.enabled = false;
        _tileHighlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
    }

    public List<Vector2Int> SetPossibleMoves(List<Vector2Int> allowedMoves, GameObject currentObj, bool instatiate = true)
    {
        var currentPos = GameManager.Instance.GridAtPiece(currentObj);
        var list = allowedMoves.Where(vec =>
        {
            if (vec.x + currentPos.x >= 8 || vec.x + currentPos.x < 0 || vec.y  + currentPos.y >= 8 || vec.y  + currentPos.y < 0)
                return false;

            return true;
        }).ToList();

        for (int i = 0; i < list.Count(); i++)
        { 
            list[i] += currentPos;
            if (instatiate)
            {
                var pieceBehaviour = currentObj.GetComponent<PieceBehaviour>();
                var eval = pieceBehaviour.CanMoveToTarget(list[i]);
                if (eval.hasObstacle)
                {
                    if (pieceBehaviour.CanAttack(eval.obstaclePos) && !allAttackHighlights.ContainsKey(eval.obstaclePos))
                    {
                        allAttackHighlights.Add(eval.obstaclePos, Instantiate(attackLocationPrefab, new Vector3(eval.obstaclePos.x, 0.1f, eval.obstaclePos.y), Quaternion.identity, gameObject.transform));
                    }
                    continue;
                }

                allMoveHighlights.Add(eval.obstaclePos, Instantiate(moveLocationPrefab, new Vector3(eval.obstaclePos.x, 0.1f, eval.obstaclePos.y), Quaternion.identity, gameObject.transform));
            }
        }
        return list;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick();
    }

    private void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        Vector3 point = hit.point;

        int x = Mathf.RoundToInt(hit.point.x);
        int z = Mathf.RoundToInt(hit.point.z);
        Vector2Int gridPoint = new Vector2Int(x, z);

        if (hit.collider.gameObject.layer == (LayerMask.NameToLayer("Highlight")))
        {
            _movingPiece.GetComponent<PieceBehaviour>().OnAction(gridPoint);

        }
        ExitState();
    }

    private void ExitState()
    {

        this.enabled = false;
        _tileHighlight.SetActive(false);
        _movingPiece = null;

        for (int i = 0; i < allMoveHighlights.Count(); i++)
        {
            Destroy(allMoveHighlights.ElementAt(i).Value);
        }

        for (int i = 0; i < allAttackHighlights.Count(); i++)
        {
            Destroy(allAttackHighlights.ElementAt(i).Value);
        }

        allAttackHighlights.Clear();
        allMoveHighlights.Clear();

        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();

    }
}
