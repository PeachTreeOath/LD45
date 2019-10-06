using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityButton : MonoBehaviour
{

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI completeText;
    public List<GameObject> requirementObjects;
    public bool hasFifthImage;
    public bool hasFifthText;
    public bool isCompleted;
    public bool isActivated;

    private string origText;

    private static string completeTextString = "COMPLETE";
    private const string blankText = "???";

    public Image buttonImageForHighlight;
    private static Color highlightColor = new Color(.7f, 1, .7f, 1);

    private void Awake()
    {
        origText = titleText.text;
        titleText.text = blankText;

        foreach (GameObject obj in requirementObjects)
            obj.SetActive(false);
    }

    public void ShowActivity()
    {
        titleText.text = origText;

        for (int i = 0; i < 10; i++)
        {
            if (i == 8)
            {
                if (hasFifthImage)
                    requirementObjects[i].SetActive(true);
            }
            else if (i == 9)
            {
                if (hasFifthText)
                    requirementObjects[i].SetActive(true);
            }
            else
                requirementObjects[i].SetActive(true);
        }

        isActivated = true;
    }

    public void SetToComplete()
    {
        if (!isActivated)
            return;

        foreach (GameObject obj in requirementObjects)
            obj.SetActive(false);

        completeText.text = completeTextString;
        isCompleted = true;
        ToggleHighlight(false);
    }

    public void ToggleHighlight(bool toggle)
    {
        if (!isActivated || isCompleted)
        {
            buttonImageForHighlight.color = Color.gray;
            return;
        }

        if (toggle)
            buttonImageForHighlight.color = highlightColor;
        else
            buttonImageForHighlight.color = Color.white;
    }
}
