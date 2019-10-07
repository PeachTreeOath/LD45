using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillTigerKingActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Count == 5)
        {
            if (slotItems[0].type == SlotType.WOOD &&
                slotItems[1].type == SlotType.WEAPONS &&
                slotItems[2].type == SlotType.TECH &&
                slotItems[3].type == SlotType.RATS &&
                slotItems[4].type == SlotType.WEAPONS)
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
