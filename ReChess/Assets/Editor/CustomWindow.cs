using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{

    bool[,] fieldsArray = new bool[17, 9];
    GameObject piece;


    [MenuItem("Window/LevelEditor")]
    public static void ShowWindow() {
        EditorWindow window = EditorWindow.GetWindow(typeof(CustomWindow));
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
        if (GUILayout.Button("Apply"))
        {
            piece.GetComponent<ChessPiece>().PieceMovement.setArray(fieldsArray);
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
                fieldsArray = _.PieceMovement.getArray();
                piece = obj;
                Repaint();
                return;
            }
            else
            {
                fieldsArray = new bool[17, 9];
                piece = null;
                Repaint();
            }
        }

        if (Selection.gameObjects.Length == 0)
        {
            fieldsArray = new bool[17, 9];
            piece = null;
            Repaint();
        }
        
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
