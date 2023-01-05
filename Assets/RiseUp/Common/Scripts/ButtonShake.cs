using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShake : MonoBehaviour {

    public Animator anim;

    public bool shakeOnStart = true;
    public float repeatRateSeconds = 1;
    public float inTimeSeconds = 0;

    private void Start()
    {
        if (shakeOnStart)
        {
            InvokeRepeating("DoShake", inTimeSeconds, repeatRateSeconds);
        }
    }

    public void DoShake()
    {
        anim.SetTrigger("shake");
    }
}
