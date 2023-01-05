using UnityEngine;
using System.Collections;

public class MonoUtils : MonoBehaviour {

    public static MonoUtils instance;

    private void Awake()
    {
        instance = this;
    }
}
