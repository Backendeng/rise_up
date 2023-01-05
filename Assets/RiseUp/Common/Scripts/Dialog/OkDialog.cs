using UnityEngine;
using System.Collections;
using System;

public class OkDialog : Dialog {
    public Action onOkClick;
    public GameObject title, message;
    public virtual void OnOkClick()
    {
        Sound.instance.PlayButton();
        if (onOkClick != null) onOkClick();
        Sound.instance.PlayButton();
        Close();
    }
}
