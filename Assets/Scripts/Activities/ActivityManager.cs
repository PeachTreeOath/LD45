using System;
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
        allActivities.Add(new GoFishingActivity());
        allActivities.Add(new BuildShelterActivity());
        allActivities.Add(new BuildRatTrapActivity());
        allActivities.Add(new BuildFireActivity());
        allActivities.Add(new ThrowBeachPartyActivity());
        allActivities.Add(new ExploreActivity());
        allActivities.Add(new CraftBowActivity());
        allActivities.Add(new CraftRopeActivity());
        allActivities.Add(new BuildLabActivity());
        allActivities.Add(new KillTigerKingActivity());
        allActivities.Add(new BuildRaftActivity());
    }

    public void RevealButtons(int numSlots)
    {
        if (numSlots > 4)
        {
            activityButtons[9].ShowActivity();
            activityButtons[10].ShowActivity();
            activityButtons[11].ShowActivity();
        }
        if (numSlots > 3)
        {
            activityButtons[7].ShowActivity();
            activityButtons[8].ShowActivity();
        }
        if (numSlots > 2)
        {
            activityButtons[5].ShowActivity();
            activityButtons[6].ShowActivity();
        }
        if (numSlots > 1)
        {
            activityButtons[2].ShowActivity();
            activityButtons[3].ShowActivity();
            activityButtons[4].ShowActivity();
        }
        else
        {
            activityButtons[0].ShowActivity();
            activityButtons[1].ShowActivity();
        }
    }

    public void PerformActivity(int activityNumber)
    {
        // check reqs
        IActivity activity = allActivities[activityNumber];
        if (activity.AreRequirementsFulfilled(SlotManager.instance.GetCurrentReelModels()))
        {
            ActivityButton activityButton = activityButtons[activityNumber];
            if (activityButton.isActivated)
            {
                if (!activityButton.isCompleted)
                {
                    activityButtons[activityNumber].SetToComplete();
                    activity.PerformActivity();
                }
            }
        }
        else
        {
            activity.FailActivity();
        }
    }

    public void ShowAllAvailableActivities(List<SlotModel> slotItems)
    {
        int activityNumber = 0;
        foreach (IActivity activity in allActivities)
        {
            if (activity.AreRequirementsFulfilled(SlotManager.instance.GetCurrentReelModels()))
                activityButtons[activityNumber].ToggleHighlight(true);
            else
                activityButtons[activityNumber].ToggleHighlight(false);

            activityNumber++;
        }
    }

    public void ShowTooltip(int activityNumber)
    {
        SpeechBubble.instance.ShowTooltip(allActivities[activityNumber].GetTooltip());
    }

    public void HideTooltip()
    {
        SpeechBubble.instance.HideTooltip();
    }
}
