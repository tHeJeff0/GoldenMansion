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
    public Action<int> SkillMethod_FreeLnacer;
    public Action<int> SkillMethod_Education;
    public Action<int, string> SkillMethod_EShop;

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

    public void SkillTrigger_EShop(string behave)
    {
        int count = GetFieldCount(6);
        SkillMethod_EShop?.Invoke(count, behave);
    }

    public void FieldSkillRegister()
    {
        SkillMethod_Financial += Skill_Financial;
        SkillMethod_Student += Skill_Student;
        SkillMethod_EShop += Skill_EShop;
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
                guest.GetComponent<GuestInApartment>().guestExtraBudget += guest.GetComponent<GuestInApartment>().guestBudget * increaseNumber;
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
            guest.GetComponent<GuestInApartment>().guestExtraBudget += budgetIncreaseNumber;
            guest.GetComponent<GuestInApartment>().guestExtraPrice += priceIncreaseNumber;
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
            guest.GetComponent<GuestInApartment>().guestExtraBudget += increaseNumber;
        }
    }

    public void Skill_Physical()
    {
        int biggestCount = 0;
        int fieldCount = GetFieldCount(7);
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
        if (fieldCount > biggestCount)
        {
            int increaseNumber = SkillLevelSelector(56, GetFieldCount(7));
            foreach (var guest in GetFieldGuest(7))
            {
                guest.GetComponent<GuestInApartment>().guestExtraBudget += (fieldCount - biggestCount) * increaseNumber;
            }
        }
    }

    public void Skill_House()
    {

    }

    public void Skill_Art()
    {

    }

    public void Skill_Food()
    {

    }

    public void Skill_Media()
    {

    }

    public void Skill_Tour()
    {

    }

    public void Skill_Jewel()
    {

    }

    public void Skill_Medic()
    {

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
