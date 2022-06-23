using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/MovementData", order = 1)]

public class PieceMovement : ScriptableObject
{

    public List<Vector2Int> movement = new List<Vector2Int>();
    public bool canMoveBackwards;
    public bool canJump = false;
    
    public bool[,] getArray()
    {
        var array = new bool[17, 9];


        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 17; i++)
            {
                array[(i) , (j)] = movement.Contains(new Vector2Int(i-8,8-j));
            }
            
        }

        return array;
    }


    public void setArray(bool[,] array)
    {
        movement.Clear();
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 17; i++)
            {
                if (array[i, j])
                {
                    if (j < 8 && canMoveBackwards)
                        movement.Add(new Vector2Int(i - 8, (8 - j) * -1));
                    movement.Add(new Vector2Int(i - 8,8 -j));
                }
            }

        }
    }
}
