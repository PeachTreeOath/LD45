using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//NOTE: This is now throw beach party
public class CraftBowActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 3; i++)
        {
            if (slotItems[i].type == SlotType.MORALE &&
                slotItems[i + 1].type == SlotType.FOOD &&
                slotItems[i + 2].type == SlotType.FOOD &&
                slotItems[i + 3].type == SlotType.MORALE)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Morale, Food, Food, Morale, just like that!" }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.maxMoves++;
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds 1 Action per day";
    }
}
