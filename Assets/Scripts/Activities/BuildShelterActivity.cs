using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Now is cut vegetations
public class BuildShelterActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (SlotManager.instance.GetModelCount(SlotType.VEG) == 0)
            return false;

        for (int i = 0; i < slotItems.Count - 1; i++)
        {
            if (slotItems[i].type == SlotType.WOOD &&
                slotItems[i + 1].type == SlotType.WOOD)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        if (SlotManager.instance.GetModelCount(SlotType.VEG) == 0)
            SpeechBubble.instance.SpeakText(new List<string> { "There is no Vegetation left!" }, new List<int> { 1 });
        else
            SpeechBubble.instance.SpeakText(new List<string> { "We need 2 Wood reels next to each other!" }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        SlotManager.instance.RemoveModel(SlotType.VEG);
        SlotManager.instance.CreateNewGlobalReel();

        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Removes 1 Vegetation";
    }
}
