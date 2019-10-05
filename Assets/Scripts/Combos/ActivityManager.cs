using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : Singleton<ActivityManager>
{

    public List<ActivityButton> activityButtons;

    private List<IActivity> allActivities = new List<IActivity>();

    public void Init()
    {
        // Add all activities here
        allActivities.Add(new WillToSurviveActivity());
    }

    public void RevealButtons(int numSlots)
    {
        if(numSlots > 4)
        {

        }
    }

    public void ShowAllAvailableActivities(List<SlotModel> slotItems)
    {
        foreach (IActivity activity in allActivities)
        {
            if (activity.AreRequirementsFulfilled(slotItems))
            {
               // view.AddActivity(activity);
            }
        }

       // view.CreateBlocks();
    }
}
