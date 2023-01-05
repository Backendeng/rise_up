using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFrame : MonoBehaviour {

    public SnapScrollRect snapScrollRect;
    public ScrollRect scrollRect;
    public Button button;
    public Sprite selectSprite, buySprite;
    public Image rubyIcon;
    public static int[] PRICES = {0, 50, 50, 70, 100};
    public Text buttonText;

    void Start()
    {
        snapScrollRect.onPageChanged += OnPageChanged;
        int selectedType = CUtils.GetPlayerType();
        snapScrollRect.SetPage(selectedType);
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            scrollRect.content.GetChild(i).Find("Image").gameObject.SetActive(CUtils.IsPlayerUnlock(i));
        }
    }

    void OnPageChanged(int index)
    {
        UpdateButton(index);
    }

    private void UpdateButton(int index)
    {
        bool unlocked = CUtils.IsPlayerUnlock(index);
        buttonText.fontSize = unlocked ? 30 : 36;
        button.image.sprite = unlocked ? selectSprite : buySprite;
        rubyIcon.gameObject.SetActive(!unlocked);
        int selectedType = CUtils.GetPlayerType();
        button.interactable = !(index == selectedType);

        var price = index < PRICES.Length ? PRICES[index] : PRICES[PRICES.Length - 1];
        buttonText.text = unlocked ? ((index == selectedType) ? "SELECTED" : "SELECT") : price.ToString();
    }

    public void SelectTypeClick()
    {
        Sound.instance.PlayButton();
        if (CUtils.IsPlayerUnlock(snapScrollRect.index))
        {
            CUtils.SetPlayerType(snapScrollRect.index);
            UpdateButton(snapScrollRect.index);
            MainController.instance.player.UpdateSprite();
        }
        else
        {
            var price = snapScrollRect.index < PRICES.Length ? PRICES[snapScrollRect.index] : PRICES[PRICES.Length - 1];
            if (CurrencyController.GetBalance() >= price)
            {
                CurrencyController.DebitBalance(price);
                CUtils.SetPlayerUnlock(snapScrollRect.index);
                scrollRect.content.GetChild(snapScrollRect.index).Find("Image").gameObject.SetActive(true);
                UpdateButton(snapScrollRect.index);
            }
            else
            {
                Toast.instance.ShowMessage("Not enough ruby!");
            }
        }
    }
}
