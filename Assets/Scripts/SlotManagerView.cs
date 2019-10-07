using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManagerView : MonoBehaviour
{

    private List<SlotItemPreview> slots = new List<SlotItemPreview>();
    private GameObject newSlotObj;
    private float slotSize;

    private bool isScrambling1;
    private bool isScrambling2;
    private bool isScrambling3;
    private bool isScrambling4;
    private bool isScrambling5;

    private float scrambleSpeed;
    private float scrambleDuration = 0.5f; // careful, linked with Reel.cs
    private float scrambleTime1;
    private float scrambleTime2;
    private float scrambleTime3;
    private float scrambleTime4;
    private float scrambleTime5;

    private SlotModel scrambleModel1;
    private SlotModel scrambleModel2;
    private SlotModel scrambleModel3;
    private SlotModel scrambleModel4;
    private SlotModel scrambleModel5;

    private int slotRowLimit = 22;

    public void Init()
    {
        newSlotObj = ResourceLoader.instance.GetPrefab("PreviewSlot");
        slotSize = ResourceLoader.instance.tileSize / 2;
        slotSize /= 2f; // Due to further resizing
    }

    private void Update()
    {
        if (isScrambling1)
        {
            ClearSlotDecorationForReels(1);
            if (Time.time - scrambleTime1 > scrambleDuration)
            {
                isScrambling1 = false;
                SetSlotModelDecorations(1, scrambleModel1, true);
            }
            else
            {
                PickRandomSlot(1);
            }
        }

        if (isScrambling2)
        {
            ClearSlotDecorationForReels(2);
            if (Time.time - scrambleTime2 > scrambleDuration)
            {
                isScrambling2 = false;
                SetSlotModelDecorations(2, scrambleModel2, true);
            }
            else
            {
                PickRandomSlot(2);
            }
        }

        if (isScrambling3)
        {
            ClearSlotDecorationForReels(3);
            if (Time.time - scrambleTime3 > scrambleDuration)
            {
                isScrambling3 = false;
                SetSlotModelDecorations(3, scrambleModel3, true);
            }
            else
            {
                PickRandomSlot(3);
            }
        }

        if (isScrambling4)
        {
            ClearSlotDecorationForReels(4);
            if (Time.time - scrambleTime4 > scrambleDuration)
            {
                isScrambling4 = false;
                SetSlotModelDecorations(4, scrambleModel4, true);
            }
            else
            {
                PickRandomSlot(4);
            }
        }

        if (isScrambling5)
        {
            ClearSlotDecorationForReels(5);
            if (Time.time - scrambleTime5 > scrambleDuration)
            {
                isScrambling5 = false;
                SetSlotModelDecorations(5, scrambleModel5, true);
            }
            else
            {
                PickRandomSlot(5);
            }
        }
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

        if (slotItems.Count > 22)
        {
            xPos += .5f;
        }

        int i = 0;
        bool secondCol = false;
        foreach (SlotModel item in slotItems)
        {
            if (i >= 22 && !secondCol)
            {
                secondCol = true;
                xPos = transform.position.x - .5f;
                yPos = transform.position.y;
            }

            SlotItemPreview newSlot = Instantiate(newSlotObj).GetComponent<SlotItemPreview>();
            newSlot.SetModel(item);
            newSlot.transform.position = new Vector2(xPos, yPos);
            newSlot.transform.SetParent(transform);
            newSlots.Add(newSlot);
            yPos -= slotSize;
            i++;
        }

        slots = newSlots;
    }

    private void ClearSlotDecorationForReels(int reelNum)
    {
        foreach (SlotItemPreview slot in slots)
        {
            slot.ToggleSelected(reelNum, false);
        }
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

    private void PickRandomSlot(int reelNum)
    {
        int rnd = Random.Range(0, slots.Count);

        slots[rnd].ToggleSelected(reelNum, true);
    }

    public void Scramble(int reelNum, SlotModel model)
    {
        switch (reelNum)
        {
            case 1:
                isScrambling1 = true;
                scrambleTime1 = Time.time;
                scrambleModel1 = model;
                break;
            case 2:
                isScrambling2 = true;
                scrambleTime2 = Time.time;
                scrambleModel2 = model;
                break;
            case 3:
                isScrambling3 = true;
                scrambleTime3 = Time.time;
                scrambleModel3 = model;
                break;
            case 4:
                isScrambling4 = true;
                scrambleTime4 = Time.time;
                scrambleModel4 = model;
                break;
            case 5:
                isScrambling5 = true;
                scrambleTime5 = Time.time;
                scrambleModel5 = model;
                break;
        }
    }
}
