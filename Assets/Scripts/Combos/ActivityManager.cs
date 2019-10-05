using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{

    private List<IActivity> allActivities = new List<IActivity>();
    private ActivityManagerView view;

    public void Init()
    {
        // Add all activities here
        allActivities.Add(gameObject.AddComponent<DanceBattleActivity>());

        view = Instantiate(ResourceLoader.instance.GetPrefab("ActivityManagerView")).GetComponent<ActivityManagerView>();
        view.Init();
    }

    public void ShowAllAvailableActivities(List<SlotModel> slotItems)
    {
        view.ClearActivities();

        foreach (IActivity activity in allActivities)
        {
            if (activity.CalculateRequirements(slotItems))
            {
                view.AddActivity(activity);
            }
        }

        view.CreateBlocks();
    }

    public void LayoutBlocks(List<Reel> reels)
    {
        view.LayoutBlocks(reels);
    }
}
