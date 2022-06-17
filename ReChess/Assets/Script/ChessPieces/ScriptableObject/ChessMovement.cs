using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/PieceData", order = 1)]
public class PieceData : ScriptableObject
{
    public string Name;
    public string Description;
    public PieceType pieceType;

}

public enum PieceType
{
    Pawn,
    Rook,
    Bishop,
    Knight,
    King,
    Queen,
}