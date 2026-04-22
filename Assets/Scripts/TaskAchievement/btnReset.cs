using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnReset : MonoBehaviour
{
    public void OnClick()
    {
        bool confirm = true;

        if (!confirm)
        {
            Debug.Log("Hủy reset");
            return;
        }

        JsonHelper.ResetDataDaily();
        JsonHelper.ResetDataMonthly();

        JsonHelper.LoadDataDaily();
        JsonHelper.LoadDataMonthly();
    }
}
