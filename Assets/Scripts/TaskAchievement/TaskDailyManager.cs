using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDailyManager : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        //int coin = PrefManager.PrefMoney.getNumberCoin();
        //Debug.Log("So coin hien tai la: " + coin);
    }

    // Update is called once per frame
    void Update()
    {
        int coin = PrefManager.PrefMoney.getNumberCoin();
        Debug.Log("So coin hien tai la: " + coin);
    }

    void DebugCoin()
    {
        Debug.Log("Coin hien tai" + PrefManager.PrefMoney.getNumberCoin());
    }



}
