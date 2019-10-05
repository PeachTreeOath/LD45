using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivityButton : MonoBehaviour
{

    public TextMeshProUGUI titleText;
    public List<GameObject> requirementObjects;

    private const string blankText = "???";
    private string origText;

    private void Start()
    {
        titleText.text = blankText;

        foreach (GameObject obj in requirementObjects)
            obj.SetActive(false);

        //TODO Check for requirements
    }
}
