using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : MonoBehaviour
{

    public virtual void Update()
    {
        //if (MainController.IsPlaying())
        //    transform.localPosition = transform.localPosition + Vector3.down * Time.deltaTime * 2;
        if (transform.position.y < -20)
            Destroy(gameObject);
    }
}
