using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillTigerKingActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Any(r => r.type == SlotType.MORALE))
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "This beast needs wayyy more than that to take down!" }, new List<int> { 4 });
    }


    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        //todo
        GameManager.instance.MoveAfterWork();
    }


    public string GetTooltip()
    {
        return "Prevent more tigers from spawning";
    }
}
