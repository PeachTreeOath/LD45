using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [HideInInspector] public int maxTech = 10;

    [HideInInspector] public int moraleGainAmt = 1;
    [HideInInspector] public int foodGainAmt = 1;
    [HideInInspector] public int woodGainAmt = 1;
    [HideInInspector] public int weaponsGainAmt = 1;
    [HideInInspector] public int techGainAmt = 1;

    [HideInInspector] public int day;
    [HideInInspector] public int movesLeft;
    [HideInInspector] public int maxMoves;

    [HideInInspector] public int numCharacters;

    // Use this for initialization
    void Start()
    {
        morale = 5;
        food = 5;
        maxMoves = 5;
        numCharacters = 1;

        StartNewDay();
        StatManager.instance.ShowStatObjectCount(2);
    }

    public void DoWork()
    {
        // Check for all of a kind
        SlotType firstSlotType = SlotManager.instance.reels[0].currentSlotModel.type;
        bool allOfAKind = true;
        foreach (Reel reel in SlotManager.instance.reels)
        {
            if (reel.currentSlotModel.type != firstSlotType)
            {
                allOfAKind = false;
                break;
            }
        }

        // Process all of a kind
        if (allOfAKind)
        {
            // check tracks
        }

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
            }
        }

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

        if (movesLeft == -1)
        {
            StartNewDay();
            return true;
        }

        return false;
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


        // New day conditions
        if(day == 3)
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

    public void UnlockNewCharacter()
    {
        numCharacters++;
        StatManager.instance.ShowStatObjectCount(numCharacters + 1);
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
