using DG.Tweening;
using ExcelData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static UnityEngine.ParticleSystem;



public class SkillController : MonoBehaviour
{
    private static SkillController instance;

    public int temporPersonaKey;

    public int studentCount;
    public int guestSoldCount;

    [SerializeField] AudioSource shakeAudioSource;
    [SerializeField] AudioClip shakeAudio;

    public Action<int,GameObject> SkillMethod_Financial;
    public Action<int, GameObject> SkillMethod_Student;
    public Action<int, GameObject> SkillMethod_Game;
    public Action<int, GameObject> SkillMethod_Physical;
    public Action<int, GameObject> SkillMethod_House;
    public Action<int, GameObject> SkillMethod_Art;
    public Action<int, GameObject> SkillMethod_Food;
    public Action<int, GameObject> SkillMethod_FreeLnacer;
    public Action<int, GameObject> SkillMethod_Education;
    public Action<int, GameObject> SkillMethod_Medic;
    public Action<int, GameObject> SkillMethod_Jobless;
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

    public void SkillTrigerControll()
    {
        List<GameObject> temporList = ApartmentController.Instance.GetGuestInApartment().
            OrderBy(go => go.transform.position.x).
            ThenByDescending(go => go.transform.position.y).ToList();
        FieldSkillOrderlyTrigger(temporList);
    }

    public void FieldSkillOrderlyTrigger(List<GameObject> guestInApartmentGroup)
    {
        StartCoroutine(Wait(0, guestInApartmentGroup));
        //foreach (var guest in guestInApartmentGroup)
        //{
        //    FieldSkillTrigger(guest);
        //    Debug.Log(guest.GetComponent<GuestInApartment>().guestName + "   " + guestInApartmentGroup.IndexOf(guest));
        //}
    }

    public void FieldSkillTrigger(GameObject guest)
    {
        switch (guest.GetComponent<GuestInApartment>().fieldSkillID)
        {
            case 50:
                SkillTrigger_Student(guest);
                break;
            case 51: 
                SkillTrigger_Financial(guest);
                break;
            case 52:
                SkillTrigger_Game(guest);
                break;
            case 53:
                SkillTrigger_FreeLancer(guest);
                break;
            case 54:
                SkillTrigger_Education(guest);
                break;
            case 56:
                SkillTrigger_Physical(guest);
                break;
            case 57:
                SkillTrigger_House(guest);
                break;
            case 58:
                SkillTrigger_Art(guest);
                break;
            case 59:
                SkillTrigger_Food(guest);
                break;
            case 63:
                SkillTrigger_Medic(guest);
                break;
            case 64:
                SkillTrigger_Jobless(guest);
                break;
        }

    }

    public void SkillTrigger_Student(GameObject guest)
    {
        int count = GetFieldCount(1);
        SkillMethod_Student?.Invoke(count,guest);
    }


    public void SkillTrigger_Financial(GameObject guest)
    {
        int count = GetFieldCount(2);
        SkillMethod_Financial?.Invoke(count,guest);
    }

    public void SkillTrigger_Game(GameObject guest)
    {
        int count = GetFieldCount(3);
        SkillMethod_Game?.Invoke(count,guest);
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

    public void SkillTrigger_FreeLancer(GameObject guest)
    {
        int count = GetFieldCount(4);
        SkillMethod_FreeLnacer?.Invoke(count,guest);
    }

    public void SkillTrigger_Education(GameObject guest)
    {
        int count = GetFieldCount(5);
        SkillMethod_Education?.Invoke(count,guest);
    }

    public void SkillTrigger_Physical(GameObject guest)
    {
        int count = GetFieldCount(7);
        SkillMethod_Physical?.Invoke(count,guest);
    }

    public void SkillTrigger_House(GameObject guest)
    {
        int count = GetFieldCount(8);
        SkillMethod_House?.Invoke(count,guest);
    }

    public void SkillTrigger_Art(GameObject guest)
    {
        int count = GetFieldCount(9);
        SkillMethod_Art?.Invoke(count,guest);
    }

    public void SkillTrigger_Food(GameObject guest)
    {
        int count = GetFieldCount(10);
        SkillMethod_Food?.Invoke(count,guest);
    }

    public void SkillTrigger_Medic(GameObject guest)
    {
        int count = GetFieldCount(14);
        SkillMethod_Medic?.Invoke(count,guest);
    }

    public void SkillTrigger_Jobless(GameObject guest)
    {
        int count = GetFieldCount(15);
        SkillMethod_Jobless?.Invoke(count,guest);
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

    public void Skill_Student(int studentCount,GameObject guestInApartment)
    {
        if (CheckFieldCount(1))
        {
            int increaseNumber = SkillLevelSelector(50, studentCount);
            skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), increaseNumber);
            //foreach (var guest in GetFieldGuest(1))
            //{
            //    skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
            //}
        }
        
    }

    public void Skill_Financial(int financialCount,GameObject guestInApartment)
    {
        if (CheckFieldCount(2))
        {
            int increaseNumber = SkillLevelSelector(51, financialCount);
            skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), increaseNumber * guestSoldCount);
            //foreach (var guest in GetFieldGuest(2))
            //{
            //    skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber * guestSoldCount);
            //}
        }
        
    }

    public void Skill_Game(int gameCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(3))
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
                skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), guestInApartment.GetComponent<GuestInApartment>().guestBudget * increaseNumber);
                foreach (var guest in GetFieldGuest(3))
                {
                    skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), guest.GetComponent<GuestInApartment>().guestBudget * increaseNumber);
                }
            }
        }
        
        
    }

    public void Skill_FreeLancer(int freeLancerCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(4))
        {
            int storageCount = GuestController.Instance.GuestInApartmentPrefabStorage.Count - 20;
            if (storageCount > 0)
            {
                int increaseNumber = SkillLevelSelector(53, freeLancerCount);
                ApartmentController.Instance.vaultMoney += increaseNumber * storageCount;
            }
        }
        
    }

    public void Skill_Education(int educationCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(5))
        {
            int budgetIncreaseNumber = 0;
            int priceIncreaseNumber = 0;
            switch (educationCount)
            {
                case 0:
                    budgetIncreaseNumber = 0;
                    priceIncreaseNumber = 0;
                    break;
                case 1:
                    budgetIncreaseNumber = 0;
                    priceIncreaseNumber = 1;
                    break;
                case 2:
                    budgetIncreaseNumber = 1;
                    priceIncreaseNumber = -1;
                    break;
                case 3:
                    budgetIncreaseNumber = 2;
                    priceIncreaseNumber = -2;
                    break;
                default:
                    budgetIncreaseNumber = 2;
                    priceIncreaseNumber = -2;
                    break;
            }
            skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), budgetIncreaseNumber);
            skillEffect.IncreaseTemporPrice(guestInApartment.GetComponent<GuestInApartment>(), priceIncreaseNumber);
        }
        
    }

    public void Skill_EShop(int eshopCount,string behave)
    {
        if (CheckFieldCount(6))
        {
            int increaseNumber = 0;
            if (behave == "sell")
            {
                increaseNumber = -SkillLevelSelector(55, eshopCount);
            }
            else if (behave == "buy")
            {
                increaseNumber = SkillLevelSelector(55, eshopCount);
            }

            foreach (var guest in GetFieldGuest(6))
            {
                skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
            }
        }
        
    }

    public void Skill_Physical(int physicalCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(7))
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
                skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), (physicalCount - biggestCount) * increaseNumber);
            }
        }
        
    }

    public void Skill_House(int houseCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(8))
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
            skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), notHouseGuest.Count * increaseNumber);
            
        }
        
    }

    public void Skill_Art(int artCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(9))
        {
            int increaseNumber = SkillLevelSelector(58, artCount);
            skillEffect.IncreaseTemporBudget(guestInApartment.GetComponent<GuestInApartment>(), GameManager.Instance.guestRemoveCount * increaseNumber);
            
        }
        
    }

    public void Skill_Food(int foodCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(10))
        {
            int increaseNumber = SkillLevelSelector(59, foodCount);
            if (guestInApartment.transform.parent != null)
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
        if (CheckFieldCount(11))
        {
            int increaseNumber = SkillLevelSelector(60, mediaCount);
            foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
            {
                skillEffect.IncreaseBasicBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);
            }
            GameManager.Instance.isAllowBuy = true;
            GameManager.Instance.isAllowSell = true;
        }
        
    }


    public void Skill_Tour(int tourCount,GuestInApartment guestInApartment)
    {
        if (CheckFieldCount(12))
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
        
        

    }

    public void Skill_Jewel()
    {
        //已独立实现
    }

    public void Skill_Medic(int medicCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(14))
        {
            int increaseNumber = SkillLevelSelector(63, medicCount);
            if (guestInApartment.GetComponent<GuestInApartment>().guestDays > 10)
            {
                int days = guestInApartment.GetComponent<GuestInApartment>().guestDays - 10;
                foreach (var medicGuest in GetFieldGuest(14))
                {
                    skillEffect.IncreaseTemporBudget(medicGuest.GetComponent<GuestInApartment>(), days * increaseNumber);
                }
            }
            
        }
        
    }

    public void Skill_Jobless(int joblessCount, GameObject guestInApartment)
    {
        if (CheckFieldCount(15))
        {
            if (guestInApartment.GetComponent<GuestInApartment>().guestBudget > 0)
            {
                guestInApartment.GetComponent<GuestInApartment>().guestBudget -= 1;
            }
            
        }
        
    }

    public bool CheckFieldCount(int fieldID)
    {
        bool isMoreThanOne = false;
        if (GetFieldCount(fieldID) > 0)
        {
            isMoreThanOne = true;
        }
        else
        {
            isMoreThanOne = false;
        }
        return isMoreThanOne;
    }

    public IEnumerator Wait(int index,List<GameObject> guestInApartmentGroup)
    {
        if (index >= guestInApartmentGroup.Count)
        {
            yield break; // 退出协程  
        }

        var guest = guestInApartmentGroup[index];
        FieldSkillTrigger(guest);

        shakeAudioSource.PlayOneShot(shakeAudio);
        yield return guest.transform.DOShakePosition(0.4f, 1, 90).WaitForCompletion();
        Debug.Log(guest.GetComponent<GuestInApartment>().guestName + "   " + index);


        // 递归调用，处理下一个Guest  
        StartCoroutine(Wait((index + 1), guestInApartmentGroup));
    }
}
