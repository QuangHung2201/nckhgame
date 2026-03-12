using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerItem : MonoBehaviour
{
    [SerializeField] private Image my_sprites;
    void Start()
    {

    }

    public void setdata(string idSprite)
    {
       Sprite sprite = SpriteAslatHelper.Instance.atlasSticker.GetSprite(idSprite);
       my_sprites.sprite = sprite;
    }
}
