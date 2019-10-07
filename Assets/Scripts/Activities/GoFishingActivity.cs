using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoFishingActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        if (slotItems.Any(r => r.type == SlotType.FOOD))
        {
            return true;
        }

        return false;
    }

    public void FailActivity()
    {
        SpeechBubble.instance.SpeakText(new List<string> { "Um, I'm gonna need Food selected in my reel to do that..." }, new List<int> { 1 });
    }

    public void PerformActivity()
    {
        //SpeechBubble.instance.SpeakText(new List<string> { "I'm starving... can't believe I finally caught some-HEY a person!",
        //    "FOOOOOOOOD *scarfs fish down*",
        //    "There's enough for both of us you know...",
        //    "Oh please. I am a descendant of Spanish explorers. I can get my own fish... once I eat these ones.",
        //    "Well I'm still starving, but I see you have a hatchet. We can work with that.",
        //}, new List<int> { 1, 2, 1, 2, 1 });

        GameManager.instance.UnlockNewCharacter();
        GameManager.instance.MoveAfterWork();
    }

    public string GetTooltip()
    {
        return "Adds a 2nd person to the group (adds 2 Wood)";
    }
}
