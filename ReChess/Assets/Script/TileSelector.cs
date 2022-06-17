using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileSelector : MonoBehaviour
{
    [SerializeField]private GameObject tileHighlightPrefab;

    [SerializeField]private GameObject tileHighlight;


    private void Start()
    {
        Vector2Int gridPoint = new Vector2Int(0, 0);
        Vector3 point = new Vector3(gridPoint.x, 0, gridPoint.y);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity,gameObject.transform);
        tileHighlight.SetActive(false);

        
    }

    public void EnterState()
    {
        enabled = true;
    }

    private void Update()
    {
        if (Camera.main is not null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                Debug.DrawLine(ray.origin, hit.point,Color.red);

                int x = Mathf.RoundToInt(hit.point.x);
                int z = Mathf.RoundToInt(hit.point.z);
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = new Vector3(x, 0, z);
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectedPiece = 
                        GameManager.Instance.PieceAtGrid(new Vector2Int(x,z));
                        //TODO: Check if piece belongs to active Player
                        GameManager.Instance.SelectPiece(selectedPiece);
                        // Reference Point 1: add ExitState call here later
                    
                    
                }
                
            }
            else
            {
                tileHighlight.SetActive(false);
            }
        }
    }
}
