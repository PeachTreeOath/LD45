using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDay : MonoBehaviour
{
    public Image cross;
    public TextMeshProUGUI title;

    private void Start()
    {
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
}
