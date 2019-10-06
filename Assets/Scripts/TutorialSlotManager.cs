using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialSlotManager : Singleton<TutorialSlotManager>
{

    private Dictionary<int, int> reelMap = new Dictionary<int, int>(); // Maps reel number to global reel index
    private List<SlotModel> slotItems = new List<SlotModel>(); // Global slot list
    public List<Reel> setupReels = new List<Reel>(); // In hierarchy reels
    [HideInInspector] public List<Reel> reels = new List<Reel>(); // In play reels

    private System.Random rand;
    private SlotManagerView view;
    private int currId;

    public void Init()
    {
        rand = new System.Random();
        GameObject viewObj = ResourceLoader.instance.GetPrefab("SlotManagerView");
        view = Instantiate(viewObj).GetComponent<SlotManagerView>();
        view.transform.SetParent(transform);
        view.Init();


        reels.Add(setupReels[0]);
        reels.Add(setupReels[1]);
    }

    public void ShowNewCharacter(int charSlot)
    {
        setupReels[charSlot].gameObject.SetActive(true);
        reels.Add(setupReels[charSlot]);
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

    public List<SlotModel> GetCurrentReelModels()
    {
        return reels.Select(x => x.currentSlotModel).ToList();
    }

    /// <summary>
    /// Takes each reel and gives it a random item from global list
    /// </summary>
    public void SpinReels()
    {
        if (TutorialGameManager.instance.movesLeft <= 0)
            return;

        foreach (Reel reel in reels)
        {
            int idx = rand.Next(0, slotItems.Count);
            SetReelToModel(reel.reelNumber, idx);
        }

        TutorialGameManager.instance.UseMove();
    }

    public void CreateNewGlobalReel()
    {
        ShuffleModels();
        view.CreateNewGlobalReel(slotItems);
    }

    /// <summary>
    /// Moves up global list and sets images
    /// </summary>
    /// <param name="reelNumber"></param>
    public void MoveReelUp(int reelNumber)
    {
        if (TutorialGameManager.instance.movesLeft <= 0)
            return;

        int nextIndex = reelMap[reelNumber];
        nextIndex--;
        if (nextIndex < 0)
        {
            nextIndex = slotItems.Count - 1;
        }

        SetReelToModel(reelNumber, nextIndex);
        TutorialGameManager.instance.UseMove();
    }

    /// <summary>
    /// Moves down global list and sets images
    /// </summary>
    /// <param name="reelNumber"></param>
    public void MoveReelDown(int reelNumber)
    {
        if (TutorialGameManager.instance.movesLeft <= 0)
            return;

        int nextIndex = reelMap[reelNumber];
        nextIndex++;
        if (nextIndex >= slotItems.Count)
        {
            nextIndex = 0;
        }

        SetReelToModel(reelNumber, nextIndex);
        TutorialGameManager.instance.UseMove();
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
                if (prevModel != null)
                    view.SetSlotModelDecorations(reel.reelNumber, prevModel, false); // Set preview decorations
                view.SetSlotModelDecorations(reel.reelNumber, currModel, true); // Set preview decorations
                reelMap[reel.reelNumber] = slotIdx;
                break;
            }
        }
    }
}
