using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillTigerKingActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Any(r => r.type == SlotType.FOOD) &&
            slotItems.Any(r => r.type == SlotType.WOOD) &&
                    slotItems.Count(r => r.type == SlotType.WEAPONS) > 1 &&
                    slotItems.Any(r => r.type == SlotType.TECH))
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Lets use a mousey to lure it out!" }, new List<int> { 4 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.dontSpawnTigers = true;
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Prevent more tigers from spawning";
    }
}
