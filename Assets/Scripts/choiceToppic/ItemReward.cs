
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ItemReward : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text_reward;
    public SpriteAtlas SpriteAtlas_sticker;
    void Start()
    {
        
    }
    public void setdata(string Idsprite, string name,int number)
    {
        text_reward.text = "Sticker " + name + " x" + number;
        icon.sprite = SpriteAtlas_sticker.GetSprite(Idsprite);
    }    
 
}
