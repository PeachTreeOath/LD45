using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{

    public int reelNumber;
    public SlotModel currentSlotModel;

    public float spinSpeed;
    public Sprite slotSprite;

    private Queue<SlotItem> items;
    private float yLimit;
    private float totalSlotHeight;
    private bool isSpinning;
    private SlotItem singleSlot; //TODO: Temporary, change from single to reel

    // Use this for initialization
    void Start()
    {
        yLimit = transform.position.y - ResourceLoader.instance.tileSize;
        //totalSlotHeight = CalculateTotalSlotHeight();

        singleSlot = GetComponentInChildren<SlotItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            float delta = spinSpeed * Time.deltaTime;
            Vector3 deltaVector = new Vector3(0, delta, 0);
            foreach (SlotItem item in items)
            {
                item.transform.position -= deltaVector;
                if (delta < yLimit)
                {
                    item.transform.position += new Vector3(0, totalSlotHeight, 0);
                }
            }
        }
    }

    public void SetCurrentSlotModel(SlotModel model)
    {
        currentSlotModel = model;
        singleSlot.SetModel(model);
    }

    public void Spin()
    {
        isSpinning = true;
    }

    private float CalculateTotalSlotHeight()
    {
        return ResourceLoader.instance.tileSize * items.Count;
    }
}
