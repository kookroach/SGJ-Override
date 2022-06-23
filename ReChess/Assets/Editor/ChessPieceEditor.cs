using UnityEngine;
using UnityEditor;

public class ChessPieceEditor : EditorWindow
{

    bool[,] fieldsArray = new bool[17, 9];
    bool canMoveBackwards;
    bool canJump;
    PieceBehaviour behaviour = null;
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
        canJump = GUILayout.Toggle(canJump, "Can jump over other pieces");

        CreateApplyButton();
        CreateDataButton();
        
    }

    private void CreateApplyButton(){
         if( piece != null && piece.GetComponent<PieceBehaviour>().PieceMovement != null){
            if (GUILayout.Button("Apply current Data"))
            {
                PieceMovement pieceMovement = piece.GetComponent<PieceBehaviour>().PieceMovement;
                pieceMovement.canMoveBackwards = canMoveBackwards;
                pieceMovement.setArray(fieldsArray);
                pieceMovement.canJump = canJump;

                EditorUtility.SetDirty(pieceMovement);
                AssetDatabase.SaveAssetIfDirty(pieceMovement);
                AssetDatabase.Refresh(); 
            }
        }else{
            GUILayout.Space(20);
        }
    }

    private void CreateDataButton(){
        GUILayout.Space(160);
        if (GUILayout.Button("Create New Move Data", EditorStyles.toolbarButton))
        {   
            var name = piece.name.Split("Red");
            name = name[0].Split("Blue");
            if(AssetDatabase.FindAssets("MovementData"+name).Length > 1){
                    //TODO
            }

            ScriptableObject newObject = ScriptableObject.CreateInstance(typeof(PieceMovement));

            

            AssetDatabase.CreateAsset(newObject, "Assets/Data/PieceData/CustomData/MovementData_"+ name[0] +".asset");
            AssetDatabase.SaveAssets();

            piece.GetComponent<PieceBehaviour>().PieceMovement = newObject as PieceMovement;
            Repaint();
        }
    }


    private void Update()
    {
        if (Selection.gameObjects.Length != 0 && piece != Selection.gameObjects[0])
        {
            var obj = Selection.gameObjects[0];

            PieceBehaviour _;
            if (obj.TryGetComponent(out _))
            {
                PieceMovement pieceMovement = _.PieceMovement;
                if(pieceMovement != null){
                    fieldsArray = pieceMovement.getArray();
                    canMoveBackwards = pieceMovement.canMoveBackwards;
                    canJump = pieceMovement.canJump;
                }
               

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
        canJump = false;

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
