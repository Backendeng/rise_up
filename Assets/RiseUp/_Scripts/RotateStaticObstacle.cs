using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStaticObstacle : StaticObstacle
{
    public bool clockwise;
    public int rotateForce;
    public int startPosY;
    public override void Update()
    {
        base.Update();
        if (transform.position.y < startPosY)
        {
            transform.Rotate(new Vector3(0, 0, (clockwise ? -1 : 1) * Time.deltaTime * rotateForce));
        }
    }
}
