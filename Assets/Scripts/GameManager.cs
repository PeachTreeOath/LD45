using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [HideInInspector] public int morale;
    [HideInInspector] public int food;
    [HideInInspector] public int wood;
    [HideInInspector] public int weapons;
    [HideInInspector] public int tech;

    [HideInInspector] public int maxMorale = 10;
    [HideInInspector] public int maxFood = 10;
    [HideInInspector] public int maxWood = 10;
    [HideInInspector] public int maxWeapons = 10;
    [HideInInspector] public int maxTech;

    [HideInInspector] public int moraleGainAmt = 1;
    [HideInInspector] public int foodGainAmt = 1;
    [HideInInspector] public int foodLossAmt = 3;
    [HideInInspector] public int woodGainAmt = 1;
    [HideInInspector] public int woodLossAmt = 5;
    [HideInInspector] public int weaponsGainAmt = 1;
    [HideInInspector] public int weaponsLossAmt = 3;
    [HideInInspector] public int techGainAmt = 1;

    [HideInInspector] public int day;

    [HideInInspector] public int movesLeft;
    [HideInInspector] public int maxMoves;

    [HideInInspector] public int numCharacters;

    public Button spinButton;

    public bool shelterBuilt;

    // Use this for initialization
    void Start()
    {
        //morale = 5;
        //food = 5;
        foodLossAmt = 3;
        woodLossAmt = 5;
        weaponsLossAmt = 3;

        maxTech = 50;

        maxMoves = 4;
        numCharacters = 1;

        StartNewDay();
        StatManager.instance.ShowStatObjectCount(2);
        ActivityManager.instance.RevealButtons(1);
        ActivityManager.instance.ShowAllAvailableActivities(SlotManager.instance.GetCurrentReelModels());
        SpeechBubble.instance.HideImmediate();
        //SpeechBubble.instance.SpeakText(new List<string> { "Ow my head...", "Why me?" }, new List<int> { 1, 4 });
    }

    public void DoWork()
    {
        bool shelterUsed = false;

        foreach (Reel reel in SlotManager.instance.reels)
        {
            switch (reel.currentSlotModel.type)
            {
                case SlotType.MORALE:
                    morale += moraleGainAmt;
                    break;
                case SlotType.FOOD:
                    food += foodGainAmt;
                    break;
                case SlotType.WOOD:
                    wood += woodGainAmt;
                    break;
                case SlotType.WEAPONS:
                    weapons += weaponsGainAmt;
                    break;
                case SlotType.TECH:
                    tech += techGainAmt;
                    break;
                case SlotType.RATS:
                    food -= foodLossAmt;
                    break;
                case SlotType.STORM:
                    if (!shelterUsed)
                        shelterUsed = true;
                    else
                    {
                        wood -= woodLossAmt;
                    }
                    break;
                case SlotType.TIGER:
                    weapons -= weaponsLossAmt;
                    break;
                case SlotType.GORILLA:
                    morale -= 25;
                    break;
            }
        }

        MoveAfterWork();
    }

    public void MoveAfterWork()
    {
        if (movesLeft > 0)
            SlotManager.instance.SpinReels();
        else
            UseMove();
    }

    // Return true if new day
    public bool UseMove()
    {
        movesLeft--;
        StatManager.instance.UpdateText();
        if (movesLeft == 0)
        {
            spinButton.interactable = false;
        }
        else if (movesLeft == -1)
        {
            EndDay();
            StartNewDay();
            return true;
        }

        return false;
    }

    public void EndDay()
    {
        if (food < 0)
        {
            morale += food;
            food = 0;
        }

        if (wood < 0)
        {
            morale += wood;
            wood = 0;
        }

        if (weapons < 0)
        {
            morale += weapons;
            weapons = 0;
        }
    }

    public void StartNewDay()
    {
        day++;

        // Calculate new day stats
        if (morale > maxMorale)
            morale = maxMorale;

        if (food > maxFood)
            food = maxFood;

        if (wood > maxWood)
            wood = maxWood;

        if (weapons > maxWeapons)
            weapons = maxWeapons;

        if (tech > maxTech)
            tech = maxTech;

        spinButton.interactable = true;

        // New day conditions
        if (day == 3)
        {

        }

        movesLeft = 100; // Just a buffer to get initial moves in
        if (day == 1)
        {
            InitSlotData();
        }
        else
        {
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();
        }
        movesLeft = maxMoves;
        StatManager.instance.UpdateText();
    }

    public void DoubleStorage()
    {
        maxMorale = 20;
        maxFood = 20;
        maxWood = 20;
        maxWeapons = 20;
        StatManager.instance.UpdateText();
    }

    public void UnlockNewCharacter()
    {
        numCharacters++;
        StatManager.instance.ShowStatObjectCount(numCharacters + 1);
        ActivityManager.instance.RevealButtons(numCharacters);
        SlotManager.instance.ShowNewCharacter(numCharacters - 1);
    }

    private void InitSlotData()
    {
        SlotManager.instance.Init();
        SlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        SlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        SlotManager.instance.AddModel(new SlotModel(SlotType.MORALE));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
        SlotManager.instance.AddModel(new SlotModel(SlotType.WEAPONS));
        SlotManager.instance.AddModel(new SlotModel(SlotType.WOOD));
        SlotManager.instance.CreateNewGlobalReel();
        SlotManager.instance.SpinReels();
    }
}
