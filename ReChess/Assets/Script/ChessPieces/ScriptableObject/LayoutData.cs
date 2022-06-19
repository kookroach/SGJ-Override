using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/LayoutData", order = 1)]

public class LayoutData : ScriptableObject
{
    public Layout[] layouts;
    public PieceData card1;
    public PieceData card2;
    public PieceData card3; 

    [Serializable]
    public class Layout
    {
        public GameObject ChessPiece;
        public Vector2 vector;
    }
}
