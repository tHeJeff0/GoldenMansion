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
    public bool isEndlessMode;
    public int mediaDays = -1;
    public float endlessModeTarget;
    public float endlessModeTargetTimes;
    public int endlessModeDays;
    public int endlessModeDaysPlus;
    public List<string> guestID = new List<string>();
    public List<int> guestKey = new List<int>();
    public List<string> guestPersona = new List<string>();
    public List<int> guestBasicCost = new List<int>();
    public List<int> guestExtraCost = new List<int>();
    public List<int> guestBasicPrice = new List<int>();
    public List<int> guestExtraPrice = new List<int>();
    public List<int> guestBudget = new List<int>();
    public List<int> guestExtraBudget = new List<int>();
    public List<bool> guestIsDestroyable = new List<bool>();
    public List<int> guestTourDays = new List<int>();
    public List<int> guestDays = new List<int>();
    public List<int> guestMBTI = new List<int>();
    public List<int> guestAdjancentPrice = new List<int>();

    public int personaIDOne;
    public int personaIDTwo;
    public int guestIDOne;
    public int guestIDTwo;
    public int guestIDThree;
    public int rerollTime;
    //public List<GameObject> guestStorage = new List<GameObject>();
}
