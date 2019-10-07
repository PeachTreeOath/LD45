using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : Singleton<ScreenFader>
{
    public CanvasGroup introGroup;
    public CanvasGroup loseGroup;

    private bool isIntroing;
    private float introStartTime;

    private bool isLosing;
    private float loseStartTime;

    // Update is called once per frame
    void Update()
    {
        if (isIntroing)
        {
            float alpha = Mathf.Lerp(1, 0, Time.time - introStartTime);
            introGroup.alpha = alpha;

            if (Time.time - introStartTime > 1)
            {
                isIntroing = false;
                introGroup.blocksRaycasts = false;
                introGroup.interactable = false;
            }
        }
        if (isLosing)
        {
            float alpha = Mathf.Lerp(0, 1, (Time.time - loseStartTime) / 3);
            loseGroup.alpha = alpha;
            if ((Time.time - loseStartTime) / 3 > 1)
            {
                isLosing = false;
            }
        }
    }

    public void Activate()
    {
        loseStartTime = Time.time;
        isLosing = true;
        loseGroup.blocksRaycasts = true;
        loseGroup.interactable = true;
    }

    public void DeactivateIntro()
    {
        introStartTime = Time.time + 2;
        isIntroing = true;
        introGroup.alpha = 1;
        introGroup.blocksRaycasts = true;
        introGroup.interactable = true;
    }
}
