using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildLabActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 2; i++)
        {
            if (slotItems[i].type == SlotType.WOOD &&
                slotItems[i + 1].type == SlotType.STORM &&
                slotItems[i + 2].type == SlotType.TECH)
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
        return "Doubles no. of Actions Left (this day only)";
    }
}
