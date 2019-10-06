using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : Singleton<SpeechBubble>
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI continueText;
    public TextMeshProUGUI tooltipText;

    public Image bubbleImage;
    public Image callout1;
    public Image callout2;
    public Image callout3;
    public Image callout4;
    public Image callout5;

    public Button nextButton;

    private float fadeTime = 0.25f;
    private List<string> currTextList;
    private List<int> currPersonList;
    private int lineIdx;
    private bool isShowingTooltip;
    private bool isShowingSpeech;

    private static Color whiteFadeColor = new Color(1, 1, 1, 0);
    private static Color blackFadeColor = new Color(0, 0, 0, 0);

    public void ShowTooltip(string toolTip)
    {
        text.enabled = false;
        continueText.enabled = false;
        bubbleImage.color = Color.white;

        callout1.enabled = false;
        callout2.enabled = false;
        callout3.enabled = false;
        callout4.enabled = false;
        callout5.enabled = false;

        tooltipText.enabled = true;

        tooltipText.text = toolTip;
        isShowingTooltip = true;
    }

    public void HideTooltip()
    {
        tooltipText.enabled = false;
        isShowingTooltip = false;

        if (isShowingSpeech)
            SpeakText(currTextList, currPersonList, lineIdx);
        else
            HideImmediate();
    }

    public void SpeakText(List<string> textList, List<int> personList, int index = 0)
    {
        StopAllCoroutines();

        tooltipText.enabled = false;
        isShowingTooltip = false;

        nextButton.enabled = true;

        bubbleImage.color = Color.white;
        callout1.color = Color.white;
        callout2.color = Color.white;
        callout3.color = Color.white;
        callout4.color = Color.white;
        callout5.color = Color.white;
        text.color = Color.black;

        if (textList.Count > 1)
            continueText.enabled = true;
        else
            continueText.enabled = false;

        currTextList = textList;
        currPersonList = personList;
        lineIdx = index;

        SpeakLine(currTextList[lineIdx], currPersonList[lineIdx]);
        isShowingSpeech = true;
    }

    private void SpeakLine(string line, int person)
    {
        text.enabled = true;
        text.text = line;

        bubbleImage.enabled = true;

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
        if (person == 6)
        {
            callout1.enabled = true;
            callout2.enabled = true;
        }
        if (person == 7)
        {
            callout1.enabled = true;
            callout2.enabled = true;
            callout3.enabled = true;
            callout4.enabled = true;
            callout5.enabled = true;
        }
    }

    public void NextText()
    {
        if (isShowingTooltip)
        {
            HideTooltip();
            return;
        }

        lineIdx++;

        if (currTextList != null && lineIdx < currTextList.Count)
        {
            SpeakLine(currTextList[lineIdx], currPersonList[lineIdx]);
            if (lineIdx + 1 < currTextList.Count)
                continueText.enabled = true;
            else
                continueText.enabled = false;
        }
        else
        {
            nextButton.enabled = false;
            continueText.enabled = false;
            isShowingSpeech = false;
            StartCoroutine("FadeOut");
        }
    }

    public void HideImmediate()
    {
        bubbleImage.color = whiteFadeColor;
        callout1.color = whiteFadeColor;
        callout2.color = whiteFadeColor;
        callout3.color = whiteFadeColor;
        callout4.color = whiteFadeColor;
        callout5.color = whiteFadeColor;
        text.color = blackFadeColor;
        continueText.enabled = false;
    }

    IEnumerator FadeOut()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeTime)
        {
            float fadeAmount = (Time.time - startTime) / fadeTime;
            Color whiteFade = Color.Lerp(Color.white, whiteFadeColor, fadeAmount);
            Color blackFade = Color.Lerp(Color.black, blackFadeColor, fadeAmount);

            bubbleImage.color = whiteFade;
            callout1.color = whiteFade;
            callout2.color = whiteFade;
            callout3.color = whiteFade;
            callout4.color = whiteFade;
            callout5.color = whiteFade;
            text.color = blackFade;

            yield return null;
        }

        bubbleImage.color = whiteFadeColor;
        callout1.color = whiteFadeColor;
        callout2.color = whiteFadeColor;
        callout3.color = whiteFadeColor;
        callout4.color = whiteFadeColor;
        callout5.color = whiteFadeColor;
        text.color = blackFadeColor;
        continueText.enabled = false;
    }
}
