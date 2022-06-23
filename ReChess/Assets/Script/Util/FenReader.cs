using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FenReader
{
    public const string startFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";


    public static void LoadPositionFromFen(string fen)
    {
        var pieces = new Dictionary<char, GameObject>() {
            ['k'] = GameManager.Instance.kingBlue,
            ['K'] = GameManager.Instance.kingRed,
            ['p'] = GameManager.Instance.pawnBlue,
            ['P'] = GameManager.Instance.pawnRed,
            ['n'] = GameManager.Instance.knightBlue,
            ['N'] = GameManager.Instance.knightRed,
            ['b'] = GameManager.Instance.bishopBlue,
            ['B'] = GameManager.Instance.bishopRed,
            ['r'] = GameManager.Instance.rookBlue,
            ['R'] = GameManager.Instance.rookRed,
            ['q'] = GameManager.Instance.queenBlue,
            ['Q'] = GameManager.Instance.queenRed,
        };

        string fenBoard = fen.Split(' ')[0];
        int file = 0, rank = 7;
        

        foreach(char symbol in fenBoard)
        {
            if(symbol == '/')
            {
                file = 0;
                rank--;
            }
            else
            {
                if (char.IsDigit(symbol))
                {
                    file += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    GameObject piece = pieces[symbol];
                    GameManager.Instance.AddPiece(piece, file, rank);
                    file++;
                }
            }
        }
    }

    public static string LoadFenFromBoard(Dictionary<Vector2Int, GameObject> board)
    {
        string fen = "";

        int rank = 7;

        //Piece positions
        while(rank >= 0)
        {
            int empty = 0, file = 0;
            while(file < 8)
            {
                var vec = new Vector2Int(file, rank);
                file++;
                GameObject obj;

                if (!board.TryGetValue(vec, out obj))
                {
                    empty++;
                    continue;
                }
                if (empty != 0)
                {
                    fen += empty.ToString();
                    empty = 0;
                }
                fen += obj.CompareTag("White") ? obj.GetComponent<PieceBehaviour>().ToString().ToUpper() : obj.GetComponent<PieceBehaviour>().ToString();
            }
            if (empty != 0)
            {
                fen += empty.ToString();
                empty = 0;
            }
            if (rank != 0)
            {
                fen += '/';
            }
            rank--;
        }

        fen += " ";

        GameManager.Instance.SetTurn(!GameManager.Instance.GetTurn());
        fen += GameManager.Instance.GetTurn() ? "w" : "b";
        fen += " ";

        //check for Castling
        fen += GameManager.Instance.GetBoard().canWhiteKingSideCastling ? "K" : "";
        fen += GameManager.Instance.GetBoard().canWhiteQueenSideCastling ? "Q" : "";
        fen += GameManager.Instance.GetBoard().canBlackKingSideCastling ? "k" : "";
        fen += GameManager.Instance.GetBoard().canBlackQueenSideCastling ? "q" : "";
        fen += " ";

        //TODO: check for En Passant

        //TODO: track Halfmove clock
        //TODO: track Fullmove number

        return fen;
    }

}
