using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogMenu : UIMenu {
    public TextMeshProUGUI content;
    public TextMeshProUGUI title;

    public void setText(string text)
    {
        content.text = text;
    }
    public void setTitle(string text)
    {
        title.text = text;
    }
}
