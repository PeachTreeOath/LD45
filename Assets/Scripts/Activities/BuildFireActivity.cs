using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildFireActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Any(r => r.type == SlotType.MORALE) && slotItems.Any(r => r.type == SlotType.WOOD))
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "We don't have the right materials, but once we do the smoke should signal others." }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 3rd person to the group";
    }
}
