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
}
