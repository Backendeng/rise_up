using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour {

    public SpriteRenderer icon;
    public Sprite[] iconSprites;
    public GameObject playerIconPrefab;
    public GameObject bubble;
    public GameObject[] bubbleParts;

    void Start()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        int selectedType = CUtils.GetPlayerType();
        icon.sprite = iconSprites[selectedType];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" && MainController.IsPlaying())
        {
            MainController.instance.CollideWithObstacle();
            MainController.instance.CheckAndShowContinue();
            icon.gameObject.SetActive(false);
            GetComponent<CircleCollider2D>().isTrigger = true;
            bubble.SetActive(false);
            foreach (GameObject p in bubbleParts)
            {
                p.SetActive(true);
            }
            GetComponent<Animator>().SetTrigger("Break");
            Sound.instance.Play(Sound.Others.Break);
            Timer.Schedule(this, 0.5f, () =>
            {
                Sound.instance.Play(Sound.Others.Die);
            });
            SpawnIcon();
        }
    }    

    private void SpawnIcon()
    {
        GameObject obj = (GameObject)Instantiate(playerIconPrefab);
        obj.transform.localPosition = icon.transform.position;
        obj.GetComponent< SpriteRenderer>().sprite = iconSprites[CUtils.GetPlayerType()];
        obj.transform.localScale = Vector3.one * 0.5f;
    }

    public void Reset()
    {
        icon.gameObject.SetActive(true);
        GetComponent<CircleCollider2D>().isTrigger = false;
        bubble.SetActive(true);
        foreach(GameObject p in bubbleParts)
        {
            p.SetActive(false);
        }
    }
}
