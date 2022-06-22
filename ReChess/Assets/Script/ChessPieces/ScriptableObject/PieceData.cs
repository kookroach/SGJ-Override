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
        _ => null,
    };
    Type GetSpecialPieceType(CustomChessPiece customChessPiece) => customChessPiece switch
    {
        CustomChessPiece.InversePawn => typeof(InvertedPawn),
        CustomChessPiece.TwoPawn => typeof(DoublePawn),
        CustomChessPiece.ImmovableKing => typeof(DisabledKing),
        CustomChessPiece.ZombieQueen => typeof(ZombieQueen),
        CustomChessPiece.TwoRook => typeof(TwoRook),
        CustomChessPiece.BigKnight => typeof(BigHorse),
        CustomChessPiece.HorselessKnight => typeof(HorselessKnight),
        CustomChessPiece.DontStopMeNowQueen => typeof(ChargeQueen),
        CustomChessPiece.KamikazeBishop => typeof(KamikazeBishop),
        CustomChessPiece.CanonRook => typeof(CanonRook),
        CustomChessPiece.CannibalKing => typeof(CannibalKing),
        CustomChessPiece.WeardownBishop => typeof(WearDownBishop),
        _ => null,
    };



    public void SelectCard()
    {
        var list = GameManager.Instance.GetPiecesOfType(GetPieceType(changeFrom));
        foreach (var piece in list)
        {
            Destroy(piece.GetComponent(GetPieceType(changeFrom)));
            piece.AddComponent(GetSpecialPieceType(changeTo));
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
        TwoPawn,
        ImmovableKing,
        ZombieQueen,
        TwoRook,
        BigKnight,
        HorselessKnight,
        DontStopMeNowQueen,
        KamikazeBishop,
        CanonRook,
        CannibalKing,
        WeardownBishop,
    }
}
