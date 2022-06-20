using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/MovementData", order = 1)]

public class PieceMovement : ScriptableObject
{

    public List<Vector2> movement;
    
    public bool[,] getArray()
    {
        var array = new bool[17, 9];


        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 17; i++)
            {
                array[(i + 8)%17 , (8-j)] = movement.Contains(new Vector2(i,j));
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
                    movement.Add(new Vector2Int(i - 8,8 -j));
                }
            }

        }
    }
}
