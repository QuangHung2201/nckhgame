using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossPopup : MonoBehaviour , IDragHandler
{
    private RectTransform m_RectTransform;
    public Canvas Canvas;
    private float m_minX,m_maxX,m_minY,m_maxY;
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        limitPosDrag();
    }

    public void limitPosDrag()
    {
        float widthPopup = m_RectTransform.rect.width;
        float hightPopup = m_RectTransform.rect.height;

        float widthScreen = Screen.width / Canvas.scaleFactor; // lấy màn hình chia cho độ scale canvas => tỷ lệ thực tế
        float hightScreen = Screen.height / Canvas.scaleFactor;

        m_minX = widthPopup / 2f;
        m_maxX = widthScreen - (widthPopup / 2f);

        m_minY = hightPopup / 2f;
        m_maxY = hightScreen - (hightPopup / 2f);
    }    
    public void OnDrag(PointerEventData eventData)
    {
      Vector2 targetPos =  m_RectTransform.anchoredPosition + eventData.delta / Canvas.scaleFactor; // => vị trí thực tế trên màn hình
      
      targetPos.x = Mathf.Clamp(targetPos.x, m_minX, m_maxX); // ép vị trí dự kiến trong limit
      targetPos.y = Mathf.Clamp(targetPos.y, m_minY, m_maxY);

      m_RectTransform.anchoredPosition = targetPos;
    }    
}
