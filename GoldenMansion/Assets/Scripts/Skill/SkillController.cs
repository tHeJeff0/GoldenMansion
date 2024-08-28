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

    public void FieldSkillRegister()
    {
        SkillMethod_Financial += Skill_Financial;
        SkillMethod_Student += Skill_Student;
    }

    public void Skill_Student(int studentCount)
    {
        int increaseNumber = SkillLevelSelector(50, studentCount);
        Debug.Log("学生人数：" + studentCount);
        foreach (var guest in GetFieldGuest(1))
        {           
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber);           
        }
        Debug.Log("触发了学生党技能，房租增加：" + increaseNumber);
    }

    public void Skill_Financial(int financialCount)
    {
        int increaseNumber = SkillLevelSelector(51, financialCount);
        Debug.Log("金融哥人数：" + financialCount);
        foreach (var guest in GetFieldGuest(2))
        {
            skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), increaseNumber*guestSoldCount);
        }
        Debug.Log("触发了金融哥技能，房租增加：" + increaseNumber);
    }
}
