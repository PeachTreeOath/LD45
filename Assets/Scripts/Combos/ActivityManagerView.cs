using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManagerView : MonoBehaviour {

    private List<IActivity> availableActivities = new List<IActivity>();
    private List<ActivityView> blocks = new List<ActivityView>();
    private Vector2 blockSize;

    public void Init()
    {
       // blockSize = ResourceLoader.instance.activityTileSize;
    }

    public void ClearActivities()
    {
        availableActivities.Clear();
        foreach(ActivityView block in blocks)
        {
            Destroy(block.gameObject);
        }
        blocks.Clear();
    }

    public void AddActivity(IActivity activity)
    {
        availableActivities.Add(activity);
    }

    public void CreateBlocks()
    {
        foreach(IActivity activity in availableActivities)
        {
            GameObject obj = Instantiate(ResourceLoader.instance.GetPrefab("ActivityView"));
            ActivityView view = obj.GetComponent<ActivityView>();
            view.activity = activity;
            view.Init();
            obj.transform.SetParent(transform);
            blocks.Add(view);
        }
    }

    /// <summary>
    /// Lays out blocks by available/clickable blocks first
    /// </summary>
    /// <param name="slotItems"></param>
    public void LayoutBlocks(List<Reel> reels)
    {
        List<ActivityView> tempBlocks = new List<ActivityView>();
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        // Layout available blocks
        foreach (ActivityView activity in blocks)
        {
            if (activity.activity.CalculateActivity(reels))
            {
                activity.transform.position = new Vector3(xPos, yPos);
                yPos -= blockSize.y;
            }
            else
            {
                tempBlocks.Add(activity);
            }
        }

        // Layout leftovers
        foreach (ActivityView activity in tempBlocks)
        {
            activity.transform.position = new Vector3(xPos, yPos);
            yPos -= blockSize.y;
        }
    }
}
