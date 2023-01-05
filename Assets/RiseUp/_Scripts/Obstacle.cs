using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    protected Rigidbody2D rigid;
    protected bool isMoveObs;
    public bool noGravity;
    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        transform.SetParent(MainController.instance.spawnRegion);
    }

    public void StartFall(bool hasCollision = false)
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        if (hasCollision && noGravity)
        {
            noGravity = false;
            rigid.gravityScale = 0.6f;
        }
    }

    public virtual void Update()
    {
        if ((noGravity || rigid.bodyType == RigidbodyType2D.Kinematic) && MainController.IsPlaying())
            transform.localPosition = transform.localPosition + Vector3.down * Time.deltaTime * 2;
        if (transform.position.y < -20 || transform.position.x > 10 || transform.position.x < -10)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Obstacle" && (collision.collider.tag == "Obstacle" || collision.collider.tag == "Protection"))
        {
            if (this is MoveObstacle)
            {
                if (!GetComponent<MoveObstacle>().staticUntilMove)
                    StartFall(true);
            }
            else
                StartFall(true);
        }
    }
}
