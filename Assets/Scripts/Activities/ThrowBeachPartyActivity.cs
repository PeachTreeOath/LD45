using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowBeachPartyActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 2; i++)
        {
            if (slotItems[i].type == SlotType.FOOD &&
                slotItems[i + 1].type == SlotType.MORALE &&
                slotItems[i + 2].type == SlotType.FOOD)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "This is a little different, make sure the reels are Food, Morale, Food, in that exact order." }, new List<int> { 3 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.morale += 50;
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Gives 50 Morale instantly";
    }
}
