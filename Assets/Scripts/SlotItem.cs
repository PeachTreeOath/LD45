using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour {

    public SlotModel model;
    public SpriteRenderer foreground;

    public void SetModel(SlotModel model)
    {
        this.model = model;
        SetSprite();
    }

	public void SetSprite()
    {
        foreground.sprite = ResourceLoader.instance.GetSpriteForType(model.type);
    }
}
