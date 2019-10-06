using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [HideInInspector] public int maxMorale = 5;
    [HideInInspector] public int maxFood = 5;
    [HideInInspector] public int maxWood = 5;
    [HideInInspector] public int maxWeapons = 5;
    [HideInInspector] public int maxTech;

    [HideInInspector] public int moraleGainAmt = 1;
    [HideInInspector] public int foodGainAmt = 1;
    [HideInInspector] public int foodLossAmt = 3;
    [HideInInspector] public int woodGainAmt = 1;
    [HideInInspector] public int woodLossAmt = 5;
    [HideInInspector] public int weaponsGainAmt = 1;
    [HideInInspector] public int weaponsLossAmt = 3;
    [HideInInspector] public int techGainAmt = 1;

    [HideInInspector] public int movesLeft;
    [HideInInspector] public int maxMoves;

    [HideInInspector] public int day;
    [HideInInspector] public int numCharacters;
    [HideInInspector] public bool shelterBuilt;
    [HideInInspector] public bool dontSpawnTigers;

    public List<Image> upgradeArrows;
    public Button spinButton;
    public TextMeshProUGUI tipText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            movesLeft += 10;
            StatManager.instance.UpdateText();
        }
    }

    // Use this for initialization
    void Start()
    {
        morale = 0;
        food = 0;
        foodLossAmt = 3;
        woodLossAmt = 5;
        weaponsLossAmt = 3;

        maxMorale = 5;
        maxFood = 5;
        maxWood = 5;
        maxWeapons = 5;
        maxTech = 50;

        maxMoves = 6;
        numCharacters = 1;

        StartNewDay();
        StatManager.instance.ShowStatObjectCount(2);
        ActivityManager.instance.RevealButtons(1);
        ActivityManager.instance.ShowAllAvailableActivities(SlotManager.instance.GetCurrentReelModels());
        SpeechBubble.instance.HideImmediate();
        //SpeechBubble.instance.SpeakText(new List<string> { "Ow my head...", "Why me?" }, new List<int> { 1, 4 });
    }

    public void UpgradeResource(int resourceType)
    {
        switch (resourceType)
        {
            case 0:
                if (morale <= maxMorale)
                    return;

                maxMorale += 5;
                morale = 0;
                break;
            case 1:
                if (food <= maxFood)
                    return;

                maxFood += 5;
                food = 0;
                break;
            case 2:
                if (wood <= maxWood)
                    return;

                maxWood += 5;
                wood = 0;
                break;
            case 3:
                if (weapons <= maxWeapons)
                    return;

                maxWeapons += 5;
                weapons = 0;
                break;
        }

        maxMoves++;
        StatManager.instance.UpdateText();
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
        food -= 1 * numCharacters;

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
        if (day == 5)  // Rats
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.RATS));
            SlotManager.instance.AddModel(new SlotModel(SlotType.RATS));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();

            int characterToSpeak = UnityEngine.Random.Range(0, numCharacters);
            switch (characterToSpeak)
            {
                case 0:
                    SpeechBubble.instance.SpeakText(new List<string> { "Whoa rats! We better guard our food supplies." }, new List<int> { 1 });
                    break;
                case 1:
                    SpeechBubble.instance.SpeakText(new List<string> { "Are those RATS by the food stash?! They will rue this day." }, new List<int> { 2 });
                    break;
            }
        }
        else if (day == 8) // Storm
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.STORM));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();

            int characterToSpeak = UnityEngine.Random.Range(0, numCharacters);
            switch (characterToSpeak)
            {
                case 0:
                case 1:
                    SpeechBubble.instance.SpeakText(new List<string> { "Looks like the weather's getting worse by the day. Let's keep our roofs patched up." }, new List<int> { 1 });
                    break;
                case 2:
                    SpeechBubble.instance.SpeakText(new List<string> { "A little rain never hurt an adventure, let's go explore some more!",
                    "Did I hear you say explore the indoors? Finally you're making sense." }, new List<int> { 2, 3 });
                    break;

            }
        }
        else if (day == 11) // Storm
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.STORM));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();

            int characterToSpeak = UnityEngine.Random.Range(0, numCharacters);
            switch (characterToSpeak)
            {
                case 0:
                case 1:
                case 2:
                    SpeechBubble.instance.SpeakText(new List<string> { "Okay maybe the storm is a LITTLE bit dangerous now.",
                        "Great! Come help me fix these holes in the wall then.",
                        "You always find a way to turn a comment into work huh?",
                        "That's right."
                    }, new List<int> { 2, 1, 2, 1 });
                    break;
                case 3:
                    SpeechBubble.instance.SpeakText(new List<string> { "Whoaaa look at the lightning from the clouds. This would make some great backdrops!",
                    "Kid, go play FFX. You'll never want to see a lightning bolt ever again." }, new List<int> { 4, 3 });
                    break;

            }
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

        if (numCharacters == 2)
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.WOOD));
            SlotManager.instance.AddModel(new SlotModel(SlotType.WOOD));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();
        }
        else if (numCharacters == 3)
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.WEAPONS));
            SlotManager.instance.AddModel(new SlotModel(SlotType.WEAPONS));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();
        }
        else if (numCharacters == 4)
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();
        }
        else if (numCharacters == 5)
        {
            SlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
            SlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
            SlotManager.instance.CreateNewGlobalReel();
            SlotManager.instance.SpinReels();
        }
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
        //SlotManager.instance.AddModel(new SlotModel(SlotType.TECH));
        //SlotManager.instance.AddModel(new SlotModel(SlotType.WEAPONS));
        //SlotManager.instance.AddModel(new SlotModel(SlotType.WOOD));
        SlotManager.instance.CreateNewGlobalReel();
        SlotManager.instance.SpinReels();
    }
}
