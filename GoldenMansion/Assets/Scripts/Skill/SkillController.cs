using ExcelData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillController : MonoBehaviour
{
    private static SkillController instance;

    public int temporPersonaKey;

    public int studentCount;
    public int guestSoldCount;

    public Action<int> SkillMethod_Financial;
    public Action<int> SkillMethod_Student;
    public Action<int> SkillMethod_Game;
    public Action<int> SkillMethod_Physical;
    public Action<int> SkillMethod_House;
    public Action<int> SkillMethod_Art;
    public Action<int> SkillMethod_Food;
    public Action<int> SkillMethod_FreeLnacer;
    public Action<int> SkillMethod_Education;
    public Action<int> SkillMethod_Medic;
    public Action<int> SkillMethod_Jobless;
    public Action<int> SkillMethod_MediaIncreaseBudget;
    public Action<int, string> SkillMethod_EShop;
    public Action<int> SkillMethod_Media;
    public Action<int, GuestInApartment> SkillMethod_Tour;

    SkillEffect skillEffect = new SkillEffect();


    public static SkillController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<SkillController>();
                if (instance == null)
                {
                    GameObject skillController = new GameObject("SkillController");
                    instance = skillController.AddComponent<SkillController>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FieldSkillRegister();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mediaDays == 0)
        {
            SkillTrigger_MediaIncreaseBudget();
            GameManager.Instance.mediaDays = -1;
        }
    }

    public List<GameObject> GetFieldGuest(int fieldID)
    {
        List<GameObject> fieldGuest = new List<GameObject>();
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().field == fieldID)
            {
                fieldGuest.Add(guest);
            }
        }
        return fieldGuest;
    }

    public int GetFieldCount(int fieldID)
    {
        int count = 0;
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().field == fieldID)
            {
                count += 1;
            }
        }
        return count;
    }

    public int SkillLevelSelector(int skillID,int condition)
    {
        int index = 0;
        if (condition == 0)
        {
            return 0;
        }
        else
        {
            for (int i = 0; i < SkillData.GetItem(skillID).conditionValue.Length; i++)
            {
                if (condition >= SkillData.GetItem(skillID).conditionValue[i])
                {
                    index = i;
                }
            }
            return SkillData.GetItem(skillID).effectValue[index];
        }
        
    }

    public void FieldSkillTrigger()
    {
        SkillTrigger_Student();
        SkillTrigger_Financial();
        SkillTrigger_Game();
        SkillTrigger_Art();
        SkillTrigger_Education();
        SkillTrigger_Food();
        SkillTrigger_FreeLancer();
        SkillTrigger_House();
        SkillTrigger_Jobless();
        SkillTrigger_MediaIncreaseBudget();
        SkillTrigger_Medic();
        SkillTrigger_Physical();
    }

    public void SkillTrigger_Student()
    {
        int count = GetFieldCount(1);
        SkillMethod_Student?.Invoke(count);
    }


    public void SkillTrigger_Financial()
    {
        int count = GetFieldCount(2);
        SkillMethod_Financial?.Invoke(count);
    }

    public void SkillTrigger_Game()
    {
        int count = GetFieldCount(3);
        SkillMethod_Game?.Invoke(count);
    }

    //已在Guest以及GuestInStorage分别触发
    public void SkillTrigger_EShop(string behave)
    {
        int count = GetFieldCount(6);
        SkillMethod_EShop?.Invoke(count, behave);
    }

    public void SkillTrigger_MediaIncreaseBudget()
    {
        int count = GetFieldCount(11);
        SkillMethod_MediaIncreaseBudget?.Invoke(count);
    }

    public void SkillTrigger_FreeLancer()
    {
        int count = GetFieldCount(4);
        SkillMethod_FreeLnacer?.Invoke(count);
    }

    public void SkillTrigger_Education()
    {
        int count = GetFieldCount(5);
        SkillMethod_Education?.Invoke(count);
    }

    public void SkillTrigger_Physical()
    {
        int count = GetFieldCount(7);
        SkillMethod_Physical?.Invoke(count);
    }

    public void SkillTrigger_House()
    {
        int count = GetFieldCount(8);
        SkillMethod_House?.Invoke(count);
    }

    public void SkillTrigger_Art()
    {
        int count = GetFieldCount(9);
        SkillMethod_Art?.Invoke(count);
    }

    public void SkillTrigger_Food()
    {
        int count = GetFieldCount(10);
        SkillMethod_Food?.Invoke(count);
    }

    public void SkillTrigger_Medic()
    {
        int count = GetFieldCount(14);
        SkillMethod_Medic?.Invoke(count);
    }

    public void SkillTrigger_Jobless()
    {
        int count = GetFieldCount(15);
        SkillMethod_Jobless?.Invoke(count);
    }
    

    public void FieldSkillRegister()
    {
        SkillMethod_Financial += Skill_Financial;
        SkillMethod_Student += Skill_Student;
        SkillMethod_EShop += Skill_EShop;
        SkillMethod_FreeLnacer += Skill_FreeLancer;
        SkillMethod_Education += Skill_Education;
        SkillMethod_Physical += Skill_Physical;
        SkillMethod_House += Skill_House;
        SkillMethod_Art += Skill_Art;
        SkillMethod_Food += Skill_Food;
        SkillMethod_MediaIncreaseBudget += Skill_MediaIncreaseBudget;
        SkillMethod_Tour += Skill_Tour;//已在ApartmentController调用
        SkillMethod_Medic += Skill_Medic;
        SkillMethod_Jobless += Skill_Jobless;
    }

    public void Skill_Student(int studentCount)
    {
        int increaseNumber = SkillLevelSelector(50, studentCount);
        foreach (var guest in GetFieldGuest(1))
        {           
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);           
        }
    }

    public void Skill_Financial(int financialCount)
    {
        int increaseNumber = SkillLevelSelector(51, financialCount);
        foreach (var guest in GetFieldGuest(2))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber*guestSoldCount);
        }
    }

    public void Skill_Game(int gameCount)
    {
        int fieldCount = 0;
        for (int i = 1; i < 16; i++)
        {
            if (GetFieldCount(i) > 1)
            {
                fieldCount += 1;
            }
        }
        if (fieldCount >= 2)
        {
            int increaseNumber = SkillLevelSelector(52, gameCount);
            foreach (var guest in GetFieldGuest(3))
            {
                skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), guest.GetComponent<GuestInApartment>().guestBudget * increaseNumber);
            }
        }
        
    }

    public void Skill_FreeLancer(int freeLancerCount)
    {
        int storageCount = GuestController.Instance.GuestInApartmentPrefabStorage.Count - 20;
        if (storageCount > 0)
        {
            int increaseNumber = SkillLevelSelector(53, freeLancerCount);
            ApartmentController.Instance.vaultMoney += increaseNumber * storageCount;
        }
    }

    public void Skill_Education(int educationCount)
    {
        int budgetIncreaseNumber = 0;
        int priceIncreaseNumber = 0;
        switch (educationCount)
        {
            case 0:budgetIncreaseNumber = 0;
                priceIncreaseNumber = 0;
                break;
            case 1:budgetIncreaseNumber = 0;
                priceIncreaseNumber = 1;
                break;
            case 2:budgetIncreaseNumber = 1;
                priceIncreaseNumber = -1;
                break;
            case 3:budgetIncreaseNumber = 2;
                priceIncreaseNumber = -2;
                break;
            default:budgetIncreaseNumber = 2;
                priceIncreaseNumber = -2;
                break;
        }
        foreach (var guest in GetFieldGuest(5))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), budgetIncreaseNumber);
            skillEffect.IncreaseTemporPrice(guest.GetComponent<GuestInApartment>(), priceIncreaseNumber);
        }
    }

    public void Skill_EShop(int eshopCount,string behave)
    {
        int increaseNumber = 0;
        if (behave == "sell")
        {
           increaseNumber = -SkillLevelSelector(55, eshopCount);
        }
        else if(behave == "buy")
        {
            increaseNumber = SkillLevelSelector(55, eshopCount);
        }
        
        foreach (var guest in GetFieldGuest(6))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
        }
    }

    public void Skill_Physical(int physicalCount)
    {
        int biggestCount = 0;
        physicalCount = GetFieldCount(7);
        for (int i = 1; i < 7; i++)
        {
            if (biggestCount < GetFieldCount(i))
            {
                biggestCount = GetFieldCount(i);
            }
        }
        for (int i = 8; i < 16; i++)
        {
            if (biggestCount < GetFieldCount(i))
            {
                biggestCount = GetFieldCount(i);
            }
        }
        if (physicalCount > biggestCount)
        {
            int increaseNumber = SkillLevelSelector(56, physicalCount);
            foreach (var guest in GetFieldGuest(7))
            {
                skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), (physicalCount - biggestCount) * increaseNumber);
            }
        }
    }

    public void Skill_House(int houseCount)
    {
        int increaseNumber = SkillLevelSelector(57, houseCount);
        List<GameObject> notHouseGuest = new List<GameObject>();
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().field != 8 && guest.transform.parent != null)
            {
                notHouseGuest.Add(guest);
            }
        }
        foreach (var guest in notHouseGuest)
        {
            guest.GetComponent<GuestInApartment>().guestExtraBudget -= increaseNumber;
        }
        foreach (var guest in GetFieldGuest(8))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), notHouseGuest.Count * increaseNumber);
        }
    }

    public void Skill_Art(int artCount)
    {
        int increaseNumber = SkillLevelSelector(58, artCount);
        foreach (var guest in GetFieldGuest(9))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), GameManager.Instance.guestRemoveCount * increaseNumber);
        }
    }

    public void Skill_Food(int foodCount)
    {
        int increaseNumber = SkillLevelSelector(59, foodCount);
        foreach (var foodGuest in GetFieldGuest(10))
        {
            if (foodGuest.transform.parent != null)
            {
                foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
                {
                    if (guest.GetComponent<GuestInApartment>().field != 10)
                    {
                        skillEffect.IncreaseBasicBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
                    }
                }
            }
        }
    }

    //不需要注册
    public void Skill_MediaBanShop()
    {
        if (GameManager.Instance.isAllowSell && GameManager.Instance.isAllowBuy)
        {
            GameManager.Instance.isAllowBuy = false;
            GameManager.Instance.isAllowSell = false;
            GameManager.Instance.mediaDays = 3;
        }
    }

    public void Skill_MediaIncreaseBudget(int mediaCount)
    {
        int increaseNumber = SkillLevelSelector(60, mediaCount);
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            skillEffect.IncreaseBasicBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
        }
        GameManager.Instance.isAllowBuy = true;
        GameManager.Instance.isAllowSell = true;
    }


    public void Skill_Tour(int tourCount,GuestInApartment guestInApartment)
    {
        if (guestInApartment.tourDays == 5)
        {
            int increaseNumber = SkillLevelSelector(61, tourCount);
            skillEffect.IncreaseTemporBudget(guestInApartment, increaseNumber * guestInApartment.guestBudget);
            guestInApartment.tourDays = 0;
        }
        else if (guestInApartment.tourDays != 5)
        {
            guestInApartment.tourDays += 1;
        }
        

    }

    public void Skill_Jewel()
    {
        //已独立实现
    }

    public void Skill_Medic(int medicCount)
    {
        int increaseNumber = SkillLevelSelector(63, medicCount);
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().guestDays > 10)
            {
                int days = guest.GetComponent<GuestInApartment>().guestDays - 10;
                foreach (var medicGuest in GetFieldGuest(14))
                {
                    skillEffect.IncreaseTemporBudget(medicGuest.GetComponent<GuestInApartment>(), days * increaseNumber);
                }
            }
        }
    }

    public void Skill_Jobless(int joblessCount)
    {
        foreach (var guest in GetFieldGuest(15))
        {
            if (guest.GetComponent<GuestInApartment>().guestBudget > 0)
            {
                guest.GetComponent<GuestInApartment>().guestBudget -= 1;
            }          
        }
    }
}
