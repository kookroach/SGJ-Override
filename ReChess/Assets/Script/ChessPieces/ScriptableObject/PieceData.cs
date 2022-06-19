using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ChessData/PieceData", order = 1)]
public class PieceData : ScriptableObject
{
    public string Name;
    public string Description;

    public ChessPiece changeFrom;
    public CustomChessPiece changeTo;

    Type GetPieceType(ChessPiece chessPiece) => chessPiece switch
    {
        ChessPiece.Pawn => typeof(Pawn),
        ChessPiece.Knight => typeof(Knight),
        ChessPiece.Bishop => typeof(Bishop),
        ChessPiece.Rook => typeof(Rook),
        ChessPiece.Queen => typeof(Queen),
        ChessPiece.King => typeof(King),
    };
    Type GetSpecialPieceType(CustomChessPiece customChessPiece) => customChessPiece switch
    {
        CustomChessPiece.InversePawn => typeof(InvertedPawn),
    };



    public void SelectCard()
    {
        Component xdd;
        var list = GameManager.pieces.Where(x => x.Value.TryGetComponent(GetPieceType(changeFrom), out xdd)).ToList();
        foreach (var piece in list)
        {
            Destroy(GameManager.pieces[piece.Key].GetComponent(GetPieceType(changeFrom)));
            GameManager.pieces[piece.Key].AddComponent(GetSpecialPieceType(changeTo));
        }
    }    

    public enum ChessPiece
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
    }

    public enum CustomChessPiece
    {
        InversePawn,
        
    }
}
