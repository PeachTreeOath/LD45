using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivityManager : Singleton<TutorialActivityManager>
{

    public List<ActivityButton> activityButtons;

    private List<IActivity> allActivities = new List<IActivity>();

    public void Init()
    {
        // Add all activities here
        allActivities.Add(new StartGameActivity());
    }

    public void RevealButtons(int numSlots)
    {
        activityButtons[0].ShowActivity();
    }

    public void PerformActivity(int activityNumber)
    {
        // check reqs
        IActivity activity = allActivities[activityNumber];
        if (activity.AreRequirementsFulfilled(TutorialSlotManager.instance.GetCurrentReelModels()))
        {
            ActivityButton activityButton = activityButtons[activityNumber];
            if (activityButton.isActivated)
            {
                if (!activityButton.isCompleted)
                {
                    if (!activityButton.isRepeatable)
                        activityButtons[activityNumber].SetToComplete();

                    activity.PerformActivity();
                }
            }
        }
        else
        {
            ActivityButton activityButton = activityButtons[activityNumber];
            if (!activityButton.isCompleted)
                activity.FailActivity();
        }
    }

    public void ShowAllAvailableActivities(List<SlotModel> slotItems)
    {
        int activityNumber = 0;
        foreach (IActivity activity in allActivities)
        {
            if (activity.AreRequirementsFulfilled(TutorialSlotManager.instance.GetCurrentReelModels()))
                activityButtons[activityNumber].ToggleHighlight(true);
            else
                activityButtons[activityNumber].ToggleHighlight(false);

            activityNumber++;
        }
    }

    public void ShowTooltip(int activityNumber)
    {
    }

    public void HideTooltip()
    {
    }
}
