using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelLogCl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtcontent;
    void Start()
    { 
        setdata();
    }

    public void setdata()
    {
        txtcontent.text = "Khu vực hiện tại đang tạm thời đóng cửa. Vui lòng khám phá xong khu vực :"
             + SetDataScreenItem.Instance.name_MapPresent 
            + "! Để mở khoá.";
    }    

}
