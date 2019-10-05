using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{

    private Dictionary<int, int> reelMap = new Dictionary<int, int>(); // Maps reel number to global reel index
    private List<SlotModel> slotItems = new List<SlotModel>(); // Global slot list
    public List<Reel> reels = new List<Reel>(); // In hierarchy reels

    private System.Random rand;
    private SlotManagerView view;
   // private ActivityManager activityManager;
    private int currId;

    public void Init()
    {
      //  activityManager = gameObject.AddComponent<ActivityManager>();
      //  activityManager.Init();

        rand = new System.Random();
        GameObject viewObj = ResourceLoader.instance.GetPrefab("SlotManagerView");
        view = Instantiate(viewObj).GetComponent<SlotManagerView>();
        view.transform.SetParent(transform);
        view.Init();

        reels.Add(GameObject.Find("Reel1").GetComponent<Reel>());
        reels.Add(GameObject.Find("Reel2").GetComponent<Reel>());
        reels.Add(GameObject.Find("Reel3").GetComponent<Reel>());
        reels.Add(GameObject.Find("Reel4").GetComponent<Reel>());
        reels.Add(GameObject.Find("Reel5").GetComponent<Reel>());
    }

    /// <summary>
    /// Adds to global list of items
    /// </summary>
    /// <param name="slotModel"></param>
    public void AddModel(SlotModel slotModel)
    {
        slotModel.slotId = currId++;
        slotItems.Add(slotModel);
    }

    /// <summary>
    /// Shuffles global list of items
    /// </summary>
    public void ShuffleModels()
    {
        slotItems = slotItems.OrderBy(x => rand.Next()).ToList();
    }

    /// <summary>
    /// Takes each reel and gives it a random item from global list
    /// </summary>
    public void SpinReels()
    {
        if (GameManager.instance.movesLeft <= 0)
            return;

        foreach (Reel reel in reels)
        {
            int idx = rand.Next(0, slotItems.Count);
            SetReelToModel(reel.reelNumber, idx);
        }
        // activityManager.LayoutBlocks(reels);

        GameManager.instance.UseMove();
    }

    public void CreateNewGlobalReel()
    {
        ShuffleModels();
        view.CreateNewGlobalReel(slotItems);
       // activityManager.ShowAllAvailableActivities(slotItems);
    }

    /// <summary>
    /// Moves up global list and sets images
    /// </summary>
    /// <param name="reelNumber"></param>
    public void MoveReelUp(int reelNumber)
    {
        if (GameManager.instance.movesLeft <= 0)
            return;

        int nextIndex = reelMap[reelNumber];
        nextIndex--;
        if (nextIndex < 0)
        {
            nextIndex = slotItems.Count - 1;
        }

        SetReelToModel(reelNumber, nextIndex);

        GameManager.instance.UseMove();
    }

    /// <summary>
    /// Moves down global list and sets images
    /// </summary>
    /// <param name="reelNumber"></param>
    public void MoveReelDown(int reelNumber)
    {
        if (GameManager.instance.movesLeft <= 0)
            return;

        int nextIndex = reelMap[reelNumber];
        nextIndex++;
        if (nextIndex >= slotItems.Count)
        {
            nextIndex = 0;
        }

        SetReelToModel(reelNumber, nextIndex);

        GameManager.instance.UseMove();
    }

    /// <summary>
    /// Sets images tied to selection
    /// </summary>
    /// <param name="i"></param>
    /// <param name="currModel"></param>
    private void SetReelToModel(int reelNumber, int slotIdx)
    {
        SlotModel currModel = slotItems[slotIdx];
        SlotModel prevModel = null;
        if (reelMap.ContainsKey(reelNumber))
        {
            int prevNum = reelMap[reelNumber];
            prevModel = slotItems[prevNum];
        }
        foreach (Reel reel in reels)
        {
            if (reel.reelNumber == reelNumber)
            {
                reel.SetCurrentSlotModel(currModel); // Set reel image
                if(prevModel != null)
                    view.SetSlotModelDecorations(reel.reelNumber, prevModel, false); // Set preview decorations
                view.SetSlotModelDecorations(reel.reelNumber, currModel, true); // Set preview decorations
                reelMap[reel.reelNumber] = slotIdx;
                break;
            }
        }
    }
}
