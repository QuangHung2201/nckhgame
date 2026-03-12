using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpStickerWH : MonoBehaviour
{
    public static PopUpStickerWH instance;
    public Transform TransformPopUp;
    public Transform popupPar;
    void Start()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
}
