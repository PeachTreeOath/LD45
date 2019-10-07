using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftRopeActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 3; i++)
        {
            if (slotItems[i].type == SlotType.WOOD &&
                slotItems[i + 1].type == SlotType.TECH &&
                slotItems[i + 2].type == SlotType.WOOD &&
                slotItems[i + 3].type == SlotType.TECH)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "We'll need materials and brains to build these..." }, new List<int> { 3 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 5th person to the group (adds 2 Tech)";
    }
}
