using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow
{
    [MenuItem("Window/Custom Editor")]
    public static void ShowWindow()
    {
        // Create the window
        var window = GetWindow<CustomEditorWindow>();
        Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Icons/MyIcon.png");
        window.titleContent = new GUIContent("My Window", icon);
    }

    private void OnGUI()
    {
        GUILayout.Label("This is a custom editor window!", EditorStyles.boldLabel);
    }
}
