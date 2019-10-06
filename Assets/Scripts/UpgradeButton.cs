using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public int resourceType;

    private static string tooltip0 = "Morale: If you have negative Morale at end of day you will lose the game. When upgraded you will gain 1 Action a day.";
    private static string tooltip1 = "Food: Each person eats 2 Food a day. You will lose Morale when < 0 Food at end of day. When upgraded you will gain 1 Action a day.";
    private static string tooltip2 = "Wood: Protects from Storms (-3 Wood). You will lose Morale when < 0 Wood at end of day. When upgraded you will gain 1 Action a day.";
    private static string tooltip3 = "Morale: Fends off Tigers (-3 Weapons). You will lose Morale when < 0 Weapon at end of day. When upgraded you will gain 1 Action a day.";
    private static string tooltip4 = "Tech: Necessary to win the game. Cannot lose or upgrade Tech.";

    public void OnClick()
    {
        GameManager.instance.UpgradeResource(resourceType);
    }

    public void OnEnter()
    {
        switch (resourceType)
        {
            case 0:
                SpeechBubble.instance.ShowTooltip(tooltip0);
                break;
            case 1:
                SpeechBubble.instance.ShowTooltip(tooltip1);
                break;
            case 2:
                if (GameManager.instance.numCharacters > 1)
                    SpeechBubble.instance.ShowTooltip(tooltip2);
                break;
            case 3:
                if (GameManager.instance.numCharacters > 2)
                    SpeechBubble.instance.ShowTooltip(tooltip3);
                break;
            case 4:
                if (GameManager.instance.numCharacters > 3)
                    SpeechBubble.instance.ShowTooltip(tooltip4);
                break;
        }
    }

    public void OnExit()
    {
        SpeechBubble.instance.HideTooltip();
    }
}
