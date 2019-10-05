using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AActivity : MonoBehaviour, IActivity {

    protected float xPos;
    protected float yPos;

    protected void AddSlot(SlotType type, GameObject parent)
    {
        SlotItemActivity slot = Instantiate(ResourceLoader.instance.GetPrefab("ActivitySlot")).GetComponent<SlotItemActivity>();
        slot.ChangeSprite(type);
        slot.transform.position = new Vector2(xPos, yPos);
        slot.transform.SetParent(parent.transform);
        xPos += ResourceLoader.instance.tileSize / 2;
    }

    protected void AddNum(int num, GameObject parent)
    {
        SlotItemActivity slot = Instantiate(ResourceLoader.instance.GetPrefab("ActivitySlot")).GetComponent<SlotItemActivity>();
        slot.ChangeNumber(num);
        slot.transform.position = new Vector2(xPos, yPos);
        slot.transform.SetParent(parent.transform);
        xPos += ResourceLoader.instance.tileSize / 2;
    }

    public virtual void BuildItem(GameObject parent)
    {
        xPos = parent.transform.position.x;
        yPos = parent.transform.position.y;
    }

    public abstract string GetActivityName();
    public abstract bool CalculateRequirements(List<SlotModel> slotItems);
    public abstract bool CalculateActivity(List<Reel> reels);
}
