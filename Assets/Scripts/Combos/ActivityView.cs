using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityView : MonoBehaviour
{

    public IActivity activity;

    // Called after activity is already set
    public void Init()
    {
        GetComponentInChildren<Text>().text = activity.GetActivityName();
        activity.BuildItem(transform.Find("Icons").gameObject);
    }
}
