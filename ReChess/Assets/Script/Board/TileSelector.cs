using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TileSelector : MonoBehaviour
{
    [SerializeField]private GameObject tileHighlightPrefab;

    [SerializeField]private GameObject tileHighlight;

    private Dictionary<Vector2Int, GameObject> allMoveHighlights = new Dictionary<Vector2Int, GameObject>();


    private void Start()
    {
        Vector2Int gridPoint = new Vector2Int(0, 0);
        Vector3 point = new Vector3(gridPoint.x, 0, gridPoint.y);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity,gameObject.transform);
        tileHighlight.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var obj = Instantiate(tileHighlightPrefab, new Vector3(i, 0.1f, j), Quaternion.identity, gameObject.transform);
                obj.SetActive(false);
                allMoveHighlights.Add(new Vector2Int(i, j),obj);
            }
        }
    }

    public void SetPossibleMoves(List<Vector2Int> allowedMoves, Vector2Int currentPos)
    {
        var list = allowedMoves.Where(vec =>
        {
            if (vec.x + currentPos.x >= 8 || vec.x + currentPos.x < 0 || vec.y + currentPos.y >= 8 || vec.y + currentPos.y < 0)
                return false;

            return true;
        }).ToList();

        foreach(var move in list)
        {
            if (GameManager.Instance.PieceAtGrid(move + currentPos) == null)
                allMoveHighlights[move + currentPos].SetActive(true);
        }
    }

    public void EnterState()
    {
        enabled = true;
    }
    
    private void ExitState(GameObject movingPiece)
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        foreach(var highlight in allMoveHighlights)
        {
            highlight.Value.SetActive(false);
        }
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }

    private void Update()
    {
        if (Camera.main is not null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                Debug.DrawLine(ray.origin, hit.point,Color.red);

                int x = Mathf.RoundToInt(hit.point.x);
                int z = Mathf.RoundToInt(hit.point.z);
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = new Vector3(x, 0.1f, z);
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectedPiece = 
                        GameManager.Instance.PieceAtGrid(new Vector2Int(x,z));


                    if (selectedPiece != null && GameManager.Instance.playerWhite.Contains(selectedPiece))
                    {

                        ExitState(selectedPiece);
                    }                                     
                }
                
            }
            else
            {
                tileHighlight.SetActive(false);
            }
        }
    }
}
