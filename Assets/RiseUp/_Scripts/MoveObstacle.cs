using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : Obstacle {
    public bool autoRotate, clockwiseRotate, hasLeftForce, hasUpForce, fallInPos, fallInTime, staticUntilMove;
    public int rotateForce, leftForce, upForce;
    public int startFallPosY, startTimingPosY = 0;
    public float startFallTime; //second
    private double startTime;
    private bool timeStart = false;

    public override void Start()
    {
        isMoveObs = true;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if(fallInTime && !timeStart && transform.position.y < startTimingPosY)
        {
            startTime = CUtils.GetCurrentTimeInMills();
            timeStart = true;
        }
        if (rigid.bodyType == RigidbodyType2D.Kinematic && ((fallInPos && transform.position.y < startFallPosY) || (fallInTime && timeStart && CUtils.GetCurrentTimeInMills() - startTime > startFallTime * 1000)))
        {
            StartFall();
            if (autoRotate)
                rigid.AddTorque((clockwiseRotate ? -1 : 1) * rotateForce);
            if (hasLeftForce)
                rigid.AddForce(Vector2.left * leftForce);
            if (hasUpForce)
                rigid.AddForce(Vector2.up * upForce);
        }
    }
}
