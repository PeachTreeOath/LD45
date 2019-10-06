using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup group;

    private bool isActivated;
    private float startTime;

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            float alpha = Mathf.Lerp(0, 1, Time.time - startTime);
            group.alpha = alpha;
        }
    }

    public void Activate()
    {
        startTime = Time.time;
        isActivated = true;
        group.blocksRaycasts = true;
        group.interactable = true;
    }
}
