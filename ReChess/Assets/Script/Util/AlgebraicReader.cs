using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AlgebraicReader 
{
    public static string GridToAlgebraic(Vector2Int gridPoint)
    {
        var str = "";
        str += char.ToLower((char)(gridPoint.x + 65));
        str += gridPoint.y + 1; 

        return str;
    }

    public static Vector2Int AlgebraicToGrid(string AN)
    {
        var arr = AN.ToCharArray();
        int x = char.ToUpper(arr[0]) - 65;
        int y = (int)char.GetNumericValue(arr[1]) - 1;

        return new Vector2Int(x, y);
    }
}
