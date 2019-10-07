using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WillToSurviveActivity : IActivity
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
        SpeechBubble.instance.SpeakText(new List<string> { "We can only do this if Morale is selected in a reel." }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "No use sulking about the situation, time to get to work!" }, new List<int> { 1 });
        SlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        SlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));

        SlotManager.instance.CreateNewGlobalReel();

        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds 2 Morale and 2 Food slots";
    }
}
