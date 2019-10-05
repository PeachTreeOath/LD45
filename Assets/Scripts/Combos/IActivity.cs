using System.Collections.Generic;
using UnityEngine;

public interface IActivity {

    string GetActivityName();

    bool CalculateRequirements(List<SlotModel> slotItems);

    bool CalculateActivity(List<Reel> reels);

    void BuildItem(GameObject parent);
}
