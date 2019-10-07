using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildFireActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 1; i++)
        {
            if (slotItems[i].type == SlotType.WOOD &&
                slotItems[i + 1].type == SlotType.MORALE)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Need a Fire reel followed by a Morale reel." }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 3rd person to the group (adds 2 Weapons)";
    }
}
