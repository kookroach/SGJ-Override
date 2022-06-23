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
            ['k'] = GameManager.Instance.king,
            ['p'] = GameManager.Instance.pawn,
            ['n'] = GameManager.Instance.knight,
            ['b'] = GameManager.Instance.bishop,
            ['r'] = GameManager.Instance.rook,
            ['q'] = GameManager.Instance.queen,
        };

        var split = fen.Split(' ');

        string fenBoard = split[0];
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
                    GameObject piece = pieces[char.ToLower(symbol)];
                    GameManager.Instance.AddPiece(piece, file, rank, char.IsUpper(symbol));
                    file++;
                }
            }
        }

        GameManager.Instance.SetTurn(split[1] == "w");

        var board = GameManager.Instance.GetBoard();

        board.canWhiteKingSideCastling = split[2].Contains('K');
        board.canWhiteQueenSideCastling = split[2].Contains('Q');
        board.canBlackKingSideCastling = split[2].Contains('k');
        board.canBlackQueenSideCastling = split[2].Contains('q');

        board.enPassant = split[3];

        board.halfmove = int.Parse(split[4]);
        board.fullmove = int.Parse(split[5]);
    }

    public static string LoadFenFromBoard(Dictionary<Vector2Int, GameObject> pieces)
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

                if (!pieces.TryGetValue(vec, out obj))
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

        fen += GameManager.Instance.GetTurn() ? "w" : "b";
        fen += " ";

        Board board = GameManager.Instance.GetBoard();
        fen += board.canWhiteKingSideCastling ? "K" : "";
        fen += board.canWhiteQueenSideCastling ? "Q" : "";
        fen += board.canBlackKingSideCastling ? "k" : "";
        fen += board.canBlackQueenSideCastling ? "q" : "";
        fen += " ";

        fen += board.enPassant;
        fen += " ";

        fen += board.halfmove;
        fen += " ";

        fen += board.fullmove;

        return fen;
    }

}
