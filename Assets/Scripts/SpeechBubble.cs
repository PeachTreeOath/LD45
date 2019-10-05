using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image callout1;
    public Image callout2;
    public Image callout3;
    public Image callout4;
    public Image callout5;

    public void SpeakText(string words, int person)
    {
        text.text = words;

        callout1.enabled = false;
        callout2.enabled = false;
        callout3.enabled = false;
        callout4.enabled = false;
        callout5.enabled = false;

        if (person == 1)
            callout1.enabled = true;
        if (person == 2)
            callout2.enabled = true;
        if (person == 3)
            callout3.enabled = true;
        if (person == 4)
            callout4.enabled = true;
        if (person == 5)
            callout5.enabled = true;
    }
}
