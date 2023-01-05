using UnityEngine;
using System.Collections;

public class ConfigController : MonoBehaviour {
    public GameConfig config;

    public static GameConfig Config
    {
        get { return instance.config; }
    }

    public static ConfigController instance;

    private void Awake()
    {
        instance = this;
    }
}
