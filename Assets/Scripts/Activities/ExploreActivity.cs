using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExploreActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 2; i++)
        {
            if (slotItems[i].type == SlotType.WEAPONS &&
                slotItems[i + 1].type == SlotType.WEAPONS &&
                slotItems[i + 2].type == SlotType.WEAPONS)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Danger ahead, let's make sure we all have weapons!" }, new List<int> { 2 });
    }

    public void PerformActivity()
    {
        // SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 4th person to the group (adds 1 Tech)";
    }
}
