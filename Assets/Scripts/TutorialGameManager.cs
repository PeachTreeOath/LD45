using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialGameManager : Singleton<TutorialGameManager>
{

    [HideInInspector] public int movesLeft;
    [HideInInspector] public int maxMoves;

    public Button spinButton;

    // Use this for initialization
    void Start()
    {
        movesLeft = 999;
        maxMoves = 999;

        InitSlotData();

        TutorialActivityManager.instance.RevealButtons(1);
        TutorialActivityManager.instance.ShowAllAvailableActivities(TutorialSlotManager.instance.GetCurrentReelModels());
    }

    public void DoWork()
    {
        List<SlotModel> slots = TutorialSlotManager.instance.GetCurrentReelModels();
        if (slots[0].type == SlotType.START && slots[1].type == SlotType.GAME)
            SceneManager.LoadScene("Game");
    }

    // Return true if new day
    public bool UseMove()
    {
        movesLeft--;
        TutorialStatManager.instance.UpdateText();
        if (movesLeft == 0)
        {
            movesLeft = 999;
        }

        return false;
    }


    private void InitSlotData()
    {
        TutorialSlotManager.instance.Init();
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.START));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.GAME));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.WOOD));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.WEAPONS));
        TutorialSlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
        TutorialSlotManager.instance.CreateNewGlobalReel();
        TutorialSlotManager.instance.SpinReels();
    }
}
