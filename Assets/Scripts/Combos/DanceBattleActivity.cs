using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DanceBattleActivity : AActivity
{

    public override string GetActivityName()
    {
        return "Dance Battle";
    }

    public override bool CalculateRequirements(List<SlotModel> slotItems)
    {
     //   if (!slotItems.Any(r => r.type == SlotType.PRACTICE_DANCE))
        {
            return false;
        }

        return true;
    }

    public override bool CalculateActivity(List<Reel> reels)
    {
        int streak = 0;
        foreach (Reel reel in reels)
        {
          //  if (reel.currentSlotModel.type == SlotType.PRACTICE_DANCE)
            {
                streak++;
                if (streak >= 3)
                {
                    return true;
                }
            }
          //  else
            {
                streak = 0;
            }
        }
        return false;
    }

    public override void BuildItem(GameObject parent)
    {
        base.BuildItem(parent);

       // AddSlot(SlotType.PRACTICE_DANCE, parent);
      //  AddSlot(SlotType.PRACTICE_DANCE, parent);
       // AddSlot(SlotType.PRACTICE_DANCE, parent);
    }

}
