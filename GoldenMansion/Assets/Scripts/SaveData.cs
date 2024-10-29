using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int gameDays;
    public bool isChooseCardFinish = false;
    public bool isRoundEnd = false;
    public float vaultMoney;
    public int levelKey;
    public int basicRerollTime = 1;
    public int extraRerollTime;
    public int guestRemoveCount;
    public bool isAllowSell;
    public bool isAllowBuy;
    public int mediaDays = -1;
    public List<string> guestID = new List<string>();
    public List<int> guestKey = new List<int>();
    //public List<GameObject> guestStorage = new List<GameObject>();
}
