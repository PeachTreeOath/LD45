using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStatManager : Singleton<TutorialStatManager>
{
    public TextMeshProUGUI actionsText;


    public void UpdateText()
    {
        actionsText.text = TutorialGameManager.instance.movesLeft + "/" + TutorialGameManager.instance.maxMoves;
    }
}
