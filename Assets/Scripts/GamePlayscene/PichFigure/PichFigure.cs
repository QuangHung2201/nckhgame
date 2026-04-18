using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PichFigure : MonoBehaviour 
{
    public static PichFigure instance;
    private ListFigure figureList;
    [SerializeField] private Transform contentFigure;
    public HorizontalLayoutGroup m_layout;
    [SerializeField] private ScrollRect m_scroll;

    RectTransform rect_content;
    RectTransform rect_croll;
    RectTransform m_RectTransform;
    public string idFigureChoose;
    private List<GameObject> listObj_figure;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        idFigureChoose = figureList.figure[0].id;
        listObj_figure = new List<GameObject>();
        m_RectTransform = GetComponent<RectTransform>();
        rect_content = contentFigure.GetComponent<RectTransform>();
        rect_croll = m_scroll.GetComponent<RectTransform>();
        initFigure();
    }
    private void Awake()
    {
        figureList = ConfigManager.instance.FigureList; // lấy list json từ configmanager
    }
    private void OnDisable()
    {
        instance = null;
    }
    // Update is called once per frame
    public void initFigure()
    {
        GameObject pref_itemFigure = Resources.Load<GameObject>("GamePlay_Manager/pichFigure/pichitem");
        if(pref_itemFigure != null )
        {
        for(int i = 0;i < figureList.figure.Count;i++)
        {
            GameObject itemfigure_clone = Instantiate(pref_itemFigure);
            itemfigure_clone.transform.SetParent(contentFigure,false);
            itemfigure_clone.GetComponent<ItemFigure>().setdata(figureList.figure[i].id, figureList.figure[i].name);
            listObj_figure.Add(itemfigure_clone);
        }
        }
    }
 
    public void Minfigurenear() // hàm lấy chỉ số nhân vật gần trung tâm nhất
    {
        float disNotabs = 0f;
        Vector3 Wpos_obj = m_RectTransform.TransformPoint(m_RectTransform.rect.center);
        int pointMin = 0;
        float disMin = 10000f;
        for(int i = 0; i< listObj_figure.Count; i++)
        {
            RectTransform rect_figure = listObj_figure[i].GetComponent<RectTransform>();
            Vector3 wpos_figure = rect_figure.TransformPoint(rect_figure.rect.center);
            float distance = math.abs(wpos_figure.x - Wpos_obj.x);

            if(distance < disMin)
            {
                disMin = distance;
                pointMin = i;
                disNotabs = Wpos_obj.x - wpos_figure.x;
            }    
        }
        Debug.Log("vị trí gần giữa nhất" + listObj_figure[pointMin].GetComponent<ItemFigure>().names);
        idFigureChoose = figureList.figure[pointMin].id; // lấy id figure ở gần giữa
        SnapToNearest(pointMin);
        //return pointMin;
    }
    float GetNormalizedPosition(RectTransform item) // hàm lấy nomallize của item
    {
        float contentWidth = rect_content.rect.width;
        float viewportWidth = m_scroll.viewport.rect.width;

        float itemPos = item.anchoredPosition.x;

        float target = (itemPos - viewportWidth / 2f) / (contentWidth - viewportWidth);

        return Mathf.Clamp01(target);
    }
    void SnapToNearest(int idFigure)
    {
        int index = idFigure;

        RectTransform item = listObj_figure[index].GetComponent<RectTransform>();

        float target = GetNormalizedPosition(item);

        m_scroll.StopMovement(); // 🔥 tránh bị inertia kéo ngược

        DOTween.To(
            () => m_scroll.horizontalNormalizedPosition,
            x => m_scroll.horizontalNormalizedPosition = x,
            target,
            0.5f
        ).SetEase(Ease.OutCubic);

        // update id
        idFigureChoose = figureList.figure[index].id;

        Debug.Log("Snap tới: " + listObj_figure[index]
            .GetComponent<ItemFigure>().names);
        scaleItem(index);
    }
    public void scaleItem(int idfigure)
    {
        for(int i = 0; i< listObj_figure.Count; i++)
        {
            RectTransform rectItem = listObj_figure[i].GetComponent<RectTransform>();
            if (idfigure == i)
            {
                rectItem.DOScale(1.4f,0.5f).SetEase(Ease.OutQuad);
            }
            else
            {
                rectItem.DOScale(1f, 0.3f);
            }
        }    
    }    
}
