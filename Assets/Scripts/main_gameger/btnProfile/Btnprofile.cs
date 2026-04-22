using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Btnprofile : MonoBehaviour
{
    public static Btnprofile instance;
    [SerializeField] private SpriteAtlas Figure_atlas;
    [SerializeField] private Image m_img;
    void Start()
    {
        instance = this;

    }
    private void Awake()
    {
        setdatabtn();
    }

    private void OnDisable()
    {
        instance = null;
    }
    public void setdatabtn()
    {
        string idFigure = PrefManager.PrefProfiles.getUserIDFigure();
        if(idFigure == "" )
        {          
            Sprite spriteFigure = Figure_atlas.GetSprite("sp1");
            m_img.sprite = spriteFigure;
        }   
        else
        {
            Sprite spriteFigrue = Figure_atlas.GetSprite(idFigure);
            m_img.sprite = spriteFigrue;
        }    
    }    
}
