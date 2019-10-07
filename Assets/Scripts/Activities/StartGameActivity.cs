using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartGameActivity : IActivity
{

    public bool AreRequirementsFulfilled(List<SlotModel> slotItems)
    {
        for (int i = 0; i < slotItems.Count - 1; i++)
        {
            if (slotItems[i].type == SlotType.START &&
                slotItems[i + 1].type == SlotType.GAME)
            {
                return true;
            }
        }

        return false;
    }

    public void FailActivity()
    {
    }

    public void PerformActivity()
    {
        SceneSwitcher.instance.StartGame();
    }

    public string GetTooltip()
    {
        return "";
    }
}
