using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject _tileHighlight;
    private GameObject _movingPiece;
    
    
    public void EnterState(GameObject piece)
    {
        _movingPiece = piece;
        this.enabled = true;
    }

    private void Start()
    {
        this.enabled = false;
        _tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        _tileHighlight.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            
            int x = Mathf.RoundToInt(hit.point.x);
            int z = Mathf.RoundToInt(hit.point.z);
            Vector2Int gridPoint = new Vector2Int(x, z);

            _tileHighlight.SetActive(true);
            _tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            
            
            if (Input.GetMouseButtonDown(0))
            {
                // Reference Point 2: check for valid move location
                if (GameManager.Instance.PieceAtGrid(gridPoint) == null)
                {
                    _movingPiece.GetComponent<IRule>().OnAction(gridPoint);
                }
                // Reference Point 3: capture enemy piece here later
                ExitState();
            }
        }
        else
        {
            _tileHighlight.SetActive(false);
        }
    }
    
    private void ExitState()
    {
        this.enabled = false;
        _tileHighlight.SetActive(false);
        GameManager.Instance.DeselectPiece(_movingPiece);
        _movingPiece = null;
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }
}