using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReelSwitch : MonoBehaviour
{

    public bool isDirectionUp;

    private Sprite normalSprite;
    private Sprite hoverSprite;
    private SpriteRenderer spriteRenderer;
    private int reelNumber;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        if (isDirectionUp)
            hoverSprite = ResourceLoader.instance.GetSprite("reelUpHover");
        else
            hoverSprite = ResourceLoader.instance.GetSprite("reelDownHover");
        reelNumber = transform.GetComponentInParent<Reel>().reelNumber;
    }

    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            if (isDirectionUp)
                TutorialSlotManager.instance.MoveReelUp(reelNumber);
            else
                TutorialSlotManager.instance.MoveReelDown(reelNumber);
        }
        else
        {
            if (isDirectionUp)
                SlotManager.instance.MoveReelUp(reelNumber);
            else
                SlotManager.instance.MoveReelDown(reelNumber);
        }
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
