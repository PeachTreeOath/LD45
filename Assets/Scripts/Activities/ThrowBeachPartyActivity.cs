using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This is now build shelter
public class ThrowBeachPartyActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (SlotManager.instance.GetModelCount(SlotType.STORM) == 0)
            return false;

        for (int i = 0; i < slotItems.Count - 2; i++)
        {
            if (slotItems[i].type == SlotType.WOOD &&
                slotItems[i + 1].type == SlotType.WOOD &&
                slotItems[i + 2].type == SlotType.WOOD)
            {

                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        if (SlotManager.instance.GetModelCount(SlotType.STORM) == 0)
            SpeechBubble.instance.SpeakText(new List<string> { "There are no Storms!" }, new List<int> { 1 });
        else
            SpeechBubble.instance.SpeakText(new List<string> { "Make sure the reels have 3 Wood in a row." }, new List<int> { 3 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        SlotManager.instance.RemoveModel(SlotType.STORM);
        SlotManager.instance.CreateNewGlobalReel();

        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Removes 1 Storm";
    }
}
