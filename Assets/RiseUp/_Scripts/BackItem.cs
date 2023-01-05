using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackItem : MonoBehaviour {

    public Image bottomImage;
    public Sprite[] botSprites;
    public Sprite[] backSprites;
    public Text levelText;
    public Action onNewLevel, onPassLevel;
    [HideInInspector]
    public RectTransform rect;
    private bool isFirstBack, isHomeBack, newLevelFired, passLevelFired;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetUp(int colorIndex, int botIndex, bool showBottom, bool isHomeBack, int level)
    {
        newLevelFired = false;
        passLevelFired = false;
        float canvasWidth = transform.parent.parent.GetComponent<RectTransform>().sizeDelta.x;
        float canvasHeight = transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y;
        GetComponent<Image>().sprite = backSprites[colorIndex];
        rect.sizeDelta = new Vector2(canvasWidth, (isHomeBack ? canvasHeight : Const.BACK_HEIGHT));
        bottomImage.sprite = botSprites[botIndex];
        bottomImage.gameObject.SetActive(showBottom);
        bottomImage.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, botSprites[botIndex].bounds.size.y * 100);
        levelText.text = level.ToString();
        this.isHomeBack = isHomeBack;
        isFirstBack = showBottom && !isHomeBack;
        levelText.gameObject.SetActive(isFirstBack);
    }

    public void Update()
    {
        if (MainController.IsPlaying())
            transform.localPosition = transform.localPosition + Vector3.down * Time.deltaTime * 200;
        if (!newLevelFired && isFirstBack && rect.anchoredPosition.y < 500)
        {
            if (onNewLevel != null) onNewLevel();
            newLevelFired = true;
        }
        if (!passLevelFired && !isFirstBack && !isHomeBack && rect.anchoredPosition.y < -1000)
        {
            if (onPassLevel != null) onPassLevel();
            passLevelFired = true;
        }
        if (rect.anchoredPosition.y < -1400)
        {
            MainController.instance.MoveBackgroundToTop(this);
            transform.SetAsLastSibling();
        }
    }
}
