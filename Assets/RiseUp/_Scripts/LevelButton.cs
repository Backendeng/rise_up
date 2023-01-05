using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    public Sprite lockSprite, currentSprite, passSprite;
    private int level, unlockLvl;
    public void Start()
    {
        level = transform.GetSiblingIndex() + 1;
        UpdateLevelState();
    }

    public void UpdateLevelState()
    {
        unlockLvl = LevelController.GetUnlockLevel();
        bool isLock = level > unlockLvl;
        GetComponent<Image>().sprite = isLock ? lockSprite : (level == unlockLvl ? currentSprite : passSprite);
    }

    public void OnClick()
    {
        if (level <= unlockLvl)
        {
            Sound.instance.PlayButton();
            Superpow.Utils.SetGameMode(Superpow.Utils.CHALLENGE_MODE);
            LevelController.SetCurrentLevel(level);
        }
    }
}
