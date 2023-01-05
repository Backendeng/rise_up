using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGroupObstacle : GroupObstacle {

    public Obstacle obstaclePrefab;

    void Start()
    {
        StartCoroutine(IESpawnObstacles());
    }
    
    public IEnumerator IESpawnObstacles()
    {
        while(true)
        {
            Obstacle obs = SpawnNewObstacle();
            obs.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 0));
            yield return new WaitForSeconds(1);
        }
    }

    private Obstacle SpawnNewObstacle()
    {
        Obstacle obs = (Obstacle)Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity);
        obs.transform.SetParent(transform);
        obs.transform.localPosition = new Vector3(-4, 0);
        obs.transform.localScale = Vector3.one;
        return obs;
    }
}
