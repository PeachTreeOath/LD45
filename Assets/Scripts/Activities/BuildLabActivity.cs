using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildLabActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Count == 5)
        {
            if (slotItems[0].type == SlotType.WOOD &&
                slotItems[1].type == SlotType.TECH &&
                slotItems[2].type == SlotType.STORM &&
                slotItems[3].type == SlotType.FOOD &&
                slotItems[4].type == SlotType.WOOD)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Is this your first day on the job? We need an actual storm to help power the generator." }, new List<int> { 5 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.movesLeft *= 2;
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Doubles # of Actions Left (this day only)";
    }
}
