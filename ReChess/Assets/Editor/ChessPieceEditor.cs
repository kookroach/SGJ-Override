using UnityEngine;
using UnityEditor;

public class ChessPieceEditor : EditorWindow
{

    bool[,] fieldsArray = new bool[17, 9];
    bool canMoveBackwards;
    ChessPiece behaviour = null;
    GameObject piece;


    [MenuItem("Window/Chess Piece Editor")]
    public static void ShowWindow() {
        EditorWindow window = EditorWindow.GetWindow(typeof(ChessPieceEditor));
        window.maxSize = new Vector2(500 , 500);
        window.minSize = window.maxSize;
    }

    void OnGUI()
    {

        GUI.DrawTexture(new Rect(234, 195, 20, 20), (Texture)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/chess-pawn.png", typeof(Texture)));
        GUILayout.Label("Chess Piece Selected : " + (piece == null ? "None" : piece.name), EditorStyles.boldLabel);

        if (piece == null)
        {
            GUI.enabled = false;
        }
        else
        {
            GUI.enabled = true;
        }
        GUILayout.Label("Piece Movement Array", EditorStyles.centeredGreyMiniLabel);
        ChangeArrayWidthAndHeight();

        GUILayout.Space(30);
        canMoveBackwards = GUILayout.Toggle(canMoveBackwards, "Can move backwards");
                

        if (GUILayout.Button("Apply"))
        {
            PieceMovement pieceMovement = piece.GetComponent<ChessPiece>().PieceMovement;
            pieceMovement.canMoveBackwards = canMoveBackwards;
            pieceMovement.setArray(fieldsArray);
        }
    }

    private void Update()
    {
        if (Selection.gameObjects.Length != 0 && piece != Selection.gameObjects[0])
        {
            var obj = Selection.gameObjects[0];

            ChessPiece _;
            if (obj.TryGetComponent(out _))
            {
                PieceMovement pieceMovement = _.PieceMovement;
                fieldsArray = pieceMovement.getArray();
                canMoveBackwards = pieceMovement.canMoveBackwards;

                piece = obj;
                Repaint();
                return;
            }
            else
            {
                Reset();
            }
        }

        if (Selection.gameObjects.Length == 0)
        {
            Reset();

        }

    }
    private void Reset()
    {
        fieldsArray = new bool[17, 9];
        canMoveBackwards = false;

        piece = null;
        Repaint();
    }


    void ChangeArrayWidthAndHeight()
    {
        for (int j = 0; j < 9; j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < 17; i++)
            {
                if (j == 8 && i == 8)
                {
                    EditorGUILayout.Space(25);
                    continue;
                }
                fieldsArray[i, j] = EditorGUILayout.Toggle(fieldsArray[i, j]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
