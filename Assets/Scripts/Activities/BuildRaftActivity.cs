using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildRaftActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Count == 5)
        {
            if (slotItems[0].type == SlotType.MORALE &&
                slotItems[1].type == SlotType.FOOD &&
                slotItems[2].type == SlotType.WOOD &&
                slotItems[3].type == SlotType.WEAPONS &&
                slotItems[4].type == SlotType.TECH)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Alright, we need each of us to specialize in a material and do it in order. Let's go home!" }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "YAHOO!" }, new List<int> { 7 });

        //fade out
        SceneManager.LoadScene("Victory");
    }

    public string GetTooltip()
    {
        return "Wins the game!";
    }
}
