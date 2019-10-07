using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildRatTrapActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (SlotManager.instance.GetModelCount(SlotType.RATS) == 0)
            return false;

        for (int i = 0; i < slotItems.Count - 1; i++)
        {
            if (slotItems[i].type == SlotType.MORALE &&
                slotItems[i + 1].type == SlotType.FOOD)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        if (SlotManager.instance.GetModelCount(SlotType.RATS) == 0)
            SpeechBubble.instance.SpeakText(new List<string> { "There are no Rats!" }, new List<int> { 1 });
        else
        SpeechBubble.instance.SpeakText(new List<string> { "Not quite there. We need Morale and Food reels side by side." }, new List<int> { 2 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        SlotManager.instance.RemoveModel(SlotType.RATS);
        SlotManager.instance.CreateNewGlobalReel();

        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Removes 1 Rat";
    }
}
