using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : Singleton<StatManager>
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI moraleText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI weaponsText;
    public TextMeshProUGUI techText;

    public Image moraleImage;
    public Image foodImage;
    public Image woodImage;
    public Image weaponsImage;
    public Image techImage;

    public TextMeshProUGUI actionsText;

    private Color errorColor = new Color(.9f, .2f, .1f);

    public void UpdateText()
    {
        dayText.text = "Day " + GameManager.instance.day;

        moraleText.text = GameManager.instance.morale + "/" + GameManager.instance.maxMorale;
        foodText.text = GameManager.instance.food + "/" + GameManager.instance.maxFood;
        woodText.text = GameManager.instance.wood + "/" + GameManager.instance.maxWood;
        weaponsText.text = GameManager.instance.weapons + "/" + GameManager.instance.maxWeapons;
        techText.text = GameManager.instance.tech + "/" + GameManager.instance.maxTech;
        actionsText.text = GameManager.instance.movesLeft + "/" + GameManager.instance.maxMoves;
        if (GameManager.instance.movesLeft == 0)
            actionsText.color = errorColor;
        else
            actionsText.color = Color.white;

        if (GameManager.instance.morale <= GameManager.instance.maxMorale)
            GameManager.instance.upgradeArrows[0].enabled = false;
        else
            GameManager.instance.upgradeArrows[0].enabled = true;

        if (GameManager.instance.food <= GameManager.instance.maxFood)
            GameManager.instance.upgradeArrows[1].enabled = false;
        else
            GameManager.instance.upgradeArrows[1].enabled = true;

        if (GameManager.instance.wood <= GameManager.instance.maxWood)
            GameManager.instance.upgradeArrows[2].enabled = false;
        else
            GameManager.instance.upgradeArrows[2].enabled = true;

        if (GameManager.instance.weapons <= GameManager.instance.maxWeapons)
            GameManager.instance.upgradeArrows[3].enabled = false;
        else
            GameManager.instance.upgradeArrows[3].enabled = true;
    }

    public void ShowStatObjectCount(int count)
    {
        if (count < 5)
        {
            techText.enabled = false;
            techImage.enabled = false;
        }
        else
        {
            techText.enabled = true;
            techImage.enabled = true;
        }

        if (count < 4)
        {
            weaponsText.enabled = false;
            weaponsImage.enabled = false;
        }
        else
        {
            weaponsText.enabled = true;
            weaponsImage.enabled = true;
        }

        if (count < 3)
        {
            woodText.enabled = false;
            woodImage.enabled = false;
        }
        else
        {
            woodText.enabled = true;
            woodImage.enabled = true;
        }
    }
}
