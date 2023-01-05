using UnityEditor;

[InitializeOnLoad]
class EditorUpdate
{
    static EditorUpdate()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        EditorApplication.update -= Update;

#if UNITY_ANDROID
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
#endif
    }
}