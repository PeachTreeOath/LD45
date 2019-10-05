using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemActivity : MonoBehaviour {

    public SpriteRenderer bg;

	public void ChangeSprite(SlotType type)
    {
        GetComponent<SpriteRenderer>().sprite = ResourceLoader.instance.GetSpriteForType(type);
        bg.enabled = true;
    }

    // Only accept #s 1-5 inclusive
    public void ChangeNumber(int num)
    {
        GetComponent<SpriteRenderer>().sprite = ResourceLoader.instance.GetSprite("x" + num);
        bg.enabled = false;
    }

}
