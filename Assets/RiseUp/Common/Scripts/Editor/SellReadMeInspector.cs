//#define UAS
//#define CHUPA
#define SMA

using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SellReadMe))]
public class SellReadMeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("1. Edit Game Settings (Admob, In-app Purchase..)", EditorStyles.boldLabel);

        if (GUILayout.Button("Edit Game Settings", GUILayout.MinHeight(40)))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/RiseUp/Common/Prefabs/GameMaster.prefab");
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("2. Game Documentation", EditorStyles.boldLabel);

        if (GUILayout.Button("Open Full Documentation", GUILayout.MinHeight(40)))
        {
            Application.OpenURL("https://drive.google.com/open?id=16xpT4Q0GoLckJ045p43zncE0GZGp4LwjzS_e9Giji1g");
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("How to add more characters", GUILayout.MinHeight(40)))
        {
            Application.OpenURL("https://www.youtube.com/watch?v=YCdJIHgUs-s");
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Build For iOS Video Guide", GUILayout.MinHeight(40)))
        {
            Application.OpenURL("https://drive.google.com/open?id=1rkgXuyFlJ2BhyNZkcn5ASuHunNExDwW5ypmFdXcd0uA");
        }
        
        EditorGUILayout.LabelField("3. My Other Great Source Codes", EditorStyles.boldLabel);
        if (GUILayout.Button("Word Cookies: Word Search", GUILayout.MinHeight(30)))
        {
#if UAS
            Application.OpenURL("https://assetstore.unity.com/packages/templates/packs/word-search-cookies-88864");
#elif CHUPA
            Application.OpenURL("https://www.chupamobile.com/unity-puzzle/word-chef-cookies-top-free-game-17252");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/word-chef-cookies-top-free-game/");
#endif
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Hexa Block Puzzle", GUILayout.MinHeight(30)))
        {
#if UAS
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/85474");
#elif CHUPA
            Application.OpenURL("https://www.chupamobile.com/unity-board/block-hexa-puzzle-unity-in-top-free-game-16762");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/hexa-puzzle-blocks-top-free-game/");
#endif
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Unroll Ball - Slide Puzzle", GUILayout.MinHeight(30)))
        {
#if UAS
            Application.OpenURL("https://assetstore.unity.com/packages/templates/packs/unroll-ball-slide-puzzle-99663");
#elif CHUPA
            Application.OpenURL("https://www.chupamobile.com/unity-puzzle/unroll-ball-slide-puzzle-18557");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/unroll-ball-slide-puzzle/");
#endif
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Unblock Me", GUILayout.MinHeight(30)))
        {
#if UAS
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/69070");
#elif CHUPA
            Application.OpenURL("https://www.chupamobile.com/unity-casual/unblock-me-12590");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/unblock-me-2/");
#endif
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Cut The Rope", GUILayout.MinHeight(30)))
        {
            Application.OpenURL("https://www.chupamobile.com/unity-arcade/cut-my-rope-12320");
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Rolling Sky", GUILayout.MinHeight(30)))
        {
#if CHUPA || UAS
            Application.OpenURL("https://www.chupamobile.com/unity-arcade/rolling-ball-on-sky-15053");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/rolling-ball-on-sky/");
#endif
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Tetris: Block Puzzle", GUILayout.MinHeight(30)))
        {
#if UAS
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/106690");
#elif CHUPA
            Application.OpenURL("https://www.chupamobile.com/unity-casual/tetris-block-puzzle-19285");
#elif SMA
            Application.OpenURL("https://www.sellmyapp.com/downloads/tetris-block-puzzle/");
#endif
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("4. Contact Us For Support", EditorStyles.boldLabel);
        EditorGUILayout.TextField("Email: ", "phuongdong0702@gmail.com");
        EditorGUILayout.TextField("Skype: ", "phuongdong0702");
    }
}