using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildLabActivity : IActivity
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
        SpeechBubble.instance.SpeakText(new List<string> { "Is this your first day on the job? Double-check the requirements." }, new List<int> { 5 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.maxMoves++;
        GameManager.instance.MoveAfterWork();
    }
}
