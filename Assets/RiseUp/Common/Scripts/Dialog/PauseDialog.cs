using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseDialog : YesNoDialog {
    public Toggle music, sound;

    protected override void Start()
    {
        base.Start();
        onYesClick = MenuClick;
        onNoClick = ContinueClick;
        music.isOn = Music.instance.IsEnabled();
        sound.isOn = Sound.instance.IsEnabled();
    }

    private void MenuClick()
    {
        GotoMenu();
    }

    private void GotoMenu()
    {
        SaveSetting();
        CUtils.LoadScene(0);
    }

    private void ContinueClick()
    {
        SaveSetting();
    }

    public void SaveSetting()
    {
        Music.instance.SetEnabled(music.isOn, true);
        Sound.instance.SetEnabled(sound.isOn);
    }

    public void OnToggleChanged()
    {
        Sound.instance.PlayButton();
    }
}
