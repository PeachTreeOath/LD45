using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    public Collider2D upButton;
    public Collider2D downButton;

    public int reelNumber;
    public SlotModel toppestSlotModel; //Buffer square
    public SlotModel topSlotModel;
    public SlotModel currentSlotModel;
    public SlotModel bottomSlotModel;
    public SlotModel bottomestSlotModel; //Buffer square

    public SlotItem toppestSlotItem; //Buffer square
    public SlotItem topSlotItem;
    public SlotItem currentSlotItem;
    public SlotItem bottomSlotItem;
    public SlotItem bottomestSlotItem; //Buffer square

    public float spinSpeed;

    public SpriteRenderer spinSprite;

    private List<SlotItem> items = new List<SlotItem>();
    private float yLimit;
    private float totalSlotHeight;
    private bool isSpinning;
    private SlotItem singleSlot; //TODO: Temporary, change from single to reel
    private float spinStartTime;
    private float moveStartTime;
    private bool isMovingUp;
    private bool isMovingDown;
    private List<Vector3> oldPositions = new List<Vector3>();
    private List<Vector3> newPositions = new List<Vector3>();
    private int nextIndex;

    private float spinDuration = 0.5f;
    private float movingTime = 0.1f;
    private Vector3 upDistToTravel = new Vector3(0, 1, 0);
    private Vector3 downDistToTravel = new Vector3(0, -1, 0);

    // Use this for initialization
    void Start()
    {
        if (items.Count == 0)
        {
            items.Add(toppestSlotItem);
            items.Add(topSlotItem);
            items.Add(currentSlotItem);
            items.Add(bottomSlotItem);
            items.Add(bottomestSlotItem);
        }

        yLimit = transform.position.y - ResourceLoader.instance.tileSize;
        totalSlotHeight = CalculateTotalSlotHeight();

        spinSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingUp)
        {
            float lerpValue = (Time.time - moveStartTime) / movingTime;
            int i = 0;
            foreach (SlotItem item in items)
            {
                item.transform.position = Vector3.Lerp(oldPositions[i], newPositions[i], lerpValue);
                i++;
            }
            if (lerpValue >= 1)
            {
                isMovingUp = false;
                FinishedMovingUp();
            }
        }
        else if (isMovingDown)
        {
            float lerpValue = (Time.time - moveStartTime) / movingTime;
            int i = 0;
            foreach (SlotItem item in items)
            {
                item.transform.position = Vector3.Lerp(oldPositions[i], newPositions[i], lerpValue);
                i++;
            }
            if (lerpValue >= 1)
            {
                isMovingDown = false;
                FinishedMovingDown();
            }
        }
        else if (isSpinning)
        {
            if (Time.time - spinStartTime > spinDuration)
            {
                spinSprite.enabled = false;
                isSpinning = false;
                FinishedMoving();
            }
        }
    }

    private void FinishedMoving()
    {
        upButton.enabled = true;
        downButton.enabled = true;
    }

    private void FinishedMovingUp()
    {
        FinishedMoving();

        //Reset slot positions
        int i = 0;
        foreach (SlotItem item in items)
        {
            item.transform.position = oldPositions[i];
            i++;
        }

        SlotManager.instance.FinishedMoving(reelNumber, nextIndex);
    }

    private void FinishedMovingDown()
    {
        FinishedMoving();

        //Reset slot positions
        int i = 0;
        foreach (SlotItem item in items)
        {
            item.transform.position = oldPositions[i];
            i++;
        }

        SlotManager.instance.FinishedMoving(reelNumber, nextIndex);
    }

    public void SetCurrentSlotModel(SlotModel model)
    {
        if (items.Count == 0)
        {
            items.Add(toppestSlotItem);
            items.Add(topSlotItem);
            items.Add(currentSlotItem);
            items.Add(bottomSlotItem);
            items.Add(bottomestSlotItem);
        }

        currentSlotModel = model;
        SlotItem item = items.ElementAt(2);
        items.ElementAt(2).SetModel(model);
    }

    public void SetTopSlotModel(SlotModel model)
    {
        topSlotModel = model;
        items.ElementAt(1).SetModel(model);
    }

    public void SetBottomSlotModel(SlotModel model)
    {
        bottomSlotModel = model;
        items.ElementAt(3).SetModel(model);
    }

    public void SetToppestSlotModel(SlotModel model)
    {
        toppestSlotModel = model;
        items.ElementAt(0).SetModel(model);
    }

    public void SetBottomestSlotModel(SlotModel model)
    {
        bottomestSlotModel = model;
        items.ElementAt(4).SetModel(model);
    }

    public void Spin()
    {
        spinStartTime = Time.time;
        isSpinning = true;
        spinSprite.enabled = true;
    }

    public void AnimateReelUp(int nextIndex)
    {
        moveStartTime = Time.time;
        isMovingUp = true;
        this.nextIndex = nextIndex;
        upButton.enabled = false;
        downButton.enabled = false;

        oldPositions = items.Select(o => o.transform.position).ToList();
        newPositions = items.Select(o => o.transform.position + downDistToTravel).ToList();
    }

    public void AnimateReelDown(int nextIndex)
    {
        moveStartTime = Time.time;
        isMovingDown = true;
        this.nextIndex = nextIndex;
        upButton.enabled = false;
        downButton.enabled = false;

        oldPositions = items.Select(o => o.transform.position).ToList();
        newPositions = items.Select(o => o.transform.position + upDistToTravel).ToList();
    }

    private float CalculateTotalSlotHeight()
    {
        return ResourceLoader.instance.tileSize * items.Count;
    }
}
