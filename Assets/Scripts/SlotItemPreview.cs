using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemPreview : SlotItem {

    public SpriteRenderer border;
    public SpriteRenderer blue;
    public SpriteRenderer red;
    public SpriteRenderer green;
    public SpriteRenderer yellow;
    public SpriteRenderer purple;

    public void ToggleSelected(int reel, bool toggle)
    {
        switch(reel)
        {
            case 1:
                blue.enabled = toggle;
                break;
            case 2:
                red.enabled = toggle;
                break;
            case 3:
                green.enabled = toggle;
                break;
            case 4:
                yellow.enabled = toggle;
                break;
            case 5:
                purple.enabled = toggle;
                break;
        }

        if(blue.enabled || red.enabled || green.enabled || yellow.enabled || purple.enabled)
        {
            border.enabled = true;
        }
        else
        {
            border.enabled = false;
        }
    }
}
