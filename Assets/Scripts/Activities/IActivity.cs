using System.Collections.Generic;
using UnityEngine;

public interface IActivity
{

    bool AreRequirementsFulfilled(List<SlotModel> slotItems);
    void FailActivity();
    void PerformActivity();
    string GetTooltip();
}
