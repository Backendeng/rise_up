using UnityEngine;
using UnityEditor;

public class SuperpowWindowEditor
{
    [MenuItem("Superpow/Reset the game")]
    static void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Superpow/Set ruby to 1000")]
    static void SetRuby1000()
    {
        CurrencyController.SetBalance(1000);
    }

    [MenuItem("Superpow/Set ruby to 0")]
    static void SetRuby0()
    {
        CurrencyController.SetBalance(0);
    }
}