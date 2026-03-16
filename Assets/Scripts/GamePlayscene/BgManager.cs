using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using DG.Tweening;

public class BgManager : MonoBehaviour
{
    public static BgManager instance;
    [SerializeField] private Image bgImg;
    [SerializeField] private Image layoutImg;
    [SerializeField] private SpriteAtlas aslatBG;
    [SerializeField] private SpriteAtlas aslatLayout;
    
    private CanvasGroup bgGroup;
    void Start()
    {
        bgGroup = bgImg.GetComponent<CanvasGroup>();
        setBg();
        ResetAlpha();
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public void setBg() // hàm set bg hiện tại
    {
        string idToppic = ManagerSceneGame.Instance.idToppic;
        if (idToppic != null) Debug.Log("không tìm được id");
        Sprite spriBG = aslatBG.GetSprite(idToppic);
        Sprite spriLayout = aslatLayout.GetSprite(idToppic);
        bgImg.sprite = spriBG;
        layoutImg.sprite = spriLayout;
    }    

    public void setAlphaBG() // hàm thay đổi alpha
    {
        float alpha = bgGroup.alpha;
        bgGroup.DOFade(alpha + 0.3f, 1f);
    }
    public void ResetAlpha()
    {
        bgGroup.alpha = 0.1f;
    }
}
