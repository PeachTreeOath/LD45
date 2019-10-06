using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExploreActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Count(r => r.type == SlotType.WEAPONS) > 2)
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "I sense danger, let's make sure we all have weapons!" }, new List<int> { 2 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 4th person to the group";
    }
}
