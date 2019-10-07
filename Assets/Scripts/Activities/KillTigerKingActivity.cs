using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillTigerKingActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 2; i++)
        {
            if (slotItems[i].type == SlotType.RATS &&
                slotItems[i + 1].type == SlotType.WEAPONS &&
                slotItems[i + 2].type == SlotType.FOOD)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Let's use a mouse to lure it out!" }, new List<int> { 4 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        int tigerCount = SlotManager.instance.GetModelCount(SlotType.TIGER);
        
        for (int i = 0; i < tigerCount; i++)
            SlotManager.instance.RemoveModel(SlotType.TIGER);

        SlotManager.instance.CreateNewGlobalReel();

        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Removes all Tigers (can only be used once!)";
    }
}
