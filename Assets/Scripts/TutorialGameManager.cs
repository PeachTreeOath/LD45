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
        movesLeft = 999;
        maxMoves = 999;
    }

    public void DoWork()
    {

    }

    // Return true if new day
    public bool UseMove()
    {
        movesLeft--;
        StatManager.instance.UpdateText();
        if (movesLeft == 0)
        {
            movesLeft = 999;
        }

        return false;
    }


    private void InitSlotData()
    {
        SlotManager.instance.Init();
        SlotManager.instance.AddModel(new SlotModel(SlotType.START));
        SlotManager.instance.AddModel(new SlotModel(SlotType.GAME));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.AddModel(new SlotModel(SlotType.FOOD));
        SlotManager.instance.CreateNewGlobalReel();
        SlotManager.instance.SpinReels();
    }
}
