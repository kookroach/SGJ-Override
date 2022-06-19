using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/LayoutData", order = 1)]

public class LayoutData : ScriptableObject
{
    public Layout[] layouts;
    public PieceData card1;
    public PieceData card2;
    public PieceData card3; 

    public PlayerMoves[] playerMoves;
    public AIMoves[] aiMoves;


    [Serializable]
    public class Layout
    {
        public GameObject ChessPiece;
        public Vector2 vector;
    }

    [Serializable]
    public class AIMoves
    {
        public CardSelect card;
        public GameObject ChessPiece;
        public Vector2 vector;
    }

    [Serializable]
    public class PlayerMoves
    {
        public CardSelect card;
        public GameObject ChessPiece;
        public Vector2 vector;
    }

    public enum CardSelect
    {
        first,
        second,
        third
    }
}
