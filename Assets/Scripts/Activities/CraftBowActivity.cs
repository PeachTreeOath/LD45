using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//NOTE: This is now build storage
public class CraftBowActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Any(r => r.type == SlotType.WOOD) &&
                    slotItems.Count(r => r.type == SlotType.WEAPONS) > 1 &&
                    slotItems.Any(r => r.type == SlotType.TECH))
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Teamwork guys! We need all sorts of help on this one." }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.DoubleStorage();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Doubles each resource's maximum capacity";
    }
}
