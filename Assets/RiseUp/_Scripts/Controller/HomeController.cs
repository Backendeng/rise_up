using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeController : BaseController {
    private const int PLAY = 0;
    private const int SETTINGS = 1;

    public void OnClick(int index)
    {
        switch (index)
        {
            case PLAY:
                CUtils.LoadScene(1);
                break;
            case SETTINGS:
                DialogController.instance.ShowDialog(DialogType.Settings);
                break;
        }
        Sound.instance.PlayButton();
    }

}
