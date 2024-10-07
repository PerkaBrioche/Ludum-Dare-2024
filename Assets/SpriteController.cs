using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public List<Sprite> SpriteLips;
    public SpriteRenderer SpriteRenderer;

    public void UpdateSprite(int Index)
    {
        SpriteRenderer.sprite = SpriteLips[Index];
    }
}
