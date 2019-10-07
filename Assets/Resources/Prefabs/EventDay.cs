using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDay : MonoBehaviour
{
    public Image cross;
    public TextMeshProUGUI title;
    private Color origColor;
    private void Start()
    {
        origColor = title.color;
        cross.enabled = false;
    }

    public void SetDay(int day)
    {
        title.text = "Day " + day + ":";
    }

    public void CrossOut()
    {
        cross.enabled = true;
    }

    public void ToggleTitle(bool toggle)
    {
        if (toggle)
        {
            title.color = Color.white;
        }
        else
        {
            title.color = Color.black;
        }
    }
}
