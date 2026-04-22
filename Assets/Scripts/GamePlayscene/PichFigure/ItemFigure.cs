using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ItemFigure : MonoBehaviour
{
    [SerializeField] private Image imgFituge;
    [SerializeField] private TextMeshProUGUI tmp_name;
    private SpriteAtlas atlasFiture;

    public string id;
    public string names;
    void Start()
    {

    }
    private void Awake()
    {
        atlasFiture = Resources.Load<SpriteAtlas>("spritesAtlas/FigureImg");
    }

    public void setdata(string idimg, string name)
    {
        Debug.Log(idimg);
        id = idimg;
        names = name;
        imgFituge.sprite = atlasFiture.GetSprite(idimg);
        tmp_name.text = name;
    }    
}
