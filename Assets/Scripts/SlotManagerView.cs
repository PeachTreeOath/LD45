using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManagerView : MonoBehaviour
{

    private List<SlotItemPreview> slots = new List<SlotItemPreview>();
    private GameObject newSlotObj;
    private float slotSize;

    public void Init()
    {
        newSlotObj = ResourceLoader.instance.GetPrefab("PreviewSlot");
        slotSize = ResourceLoader.instance.tileSize / 2;
        slotSize /= 1.6f; // Due to further resizing
    }

    public void CreateNewGlobalReel(List<SlotModel> slotItems)
    {
        CleanUpReel();
        GenerateReel(slotItems);
    }

    private void CleanUpReel()
    {
        foreach (SlotItemPreview slot in slots)
        {
            Destroy(slot.gameObject);
        }
    }

    private void GenerateReel(List<SlotModel> slotItems)
    {
        List<SlotItemPreview> newSlots = new List<SlotItemPreview>();
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        foreach (SlotModel item in slotItems)
        {
            SlotItemPreview newSlot = Instantiate(newSlotObj).GetComponent<SlotItemPreview>();
            newSlot.SetModel(item);
            newSlot.transform.position = new Vector2(xPos, yPos);
            newSlot.transform.SetParent(transform);
            newSlots.Add(newSlot);
            yPos -= slotSize;
        }

        slots = newSlots;
    }

    public void SetSlotModelDecorations(int reelNum, SlotModel model, bool toggle)
    {
        foreach (SlotItemPreview slot in slots)
        {
            if (slot.model.slotId == model.slotId)
            {
                slot.ToggleSelected(reelNum, toggle);
                break;
            }
        }
    }
}
