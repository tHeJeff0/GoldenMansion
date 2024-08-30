using ExcelData;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GuestInApartment : MonoBehaviour
{
    public int key { get; set; }
    public int field { get; set; }
    public int fieldSkillID { get; set; }
    public int mbti { get; set; }
    public int mbtiSkill { get; set; }
    public int guestBasicPrice { get; set; }
    public int guestExtraPrice { get; set; }
    public int guestBasicCost { get; set; }
    public int guestExtraCost { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }
    public int guestExtraBudget { get; set; }
    public int guestEffectID { get; set; }
    public bool isMoveIn { get; set; } = false;
    public bool isDestroyable { get; set; } = true;
    public int tourDays { get; set; } = 0;

    [SerializeField] GameObject personaSlot;
    [SerializeField] GameObject personaPic;
    [SerializeField] TextMeshPro nameText;

    public List<int> persona = new List<int>();
    public List<int> temporPersona = new List<int>();

    public Action<GuestInApartment> SkillMethod_OnGoingEffect;
    public Action<GuestInApartment> SkillMethod_Normal;
    public Action<GuestInApartment> SkillMethod_WhenMoveIn;
    public Action<GuestInApartment> SkillMethod_WhenGuestSold;
    public Action<GuestInApartment> SkillMethod_WhenOtherGuestSold;
    public Action<GuestInApartment, int> SkillMethod_WhenGetPersona;
    public Action<GuestInApartment> SkillMethod_WhenDaysChanged;
    public Action<GuestInApartment, int> SkillMethod_EachDays;
    public Action<GuestInApartment,int> SkillWithIntMethod;


    void Awake()
    {
        key = GuestController.Instance.temporKey;
        this.field = CharacterData.GetItem(key).field;
        this.fieldSkillID = FieldData.GetItem(field).skillID;
        this.guestDays = GameManager.Instance.gameDays;
        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = CharacterData.GetItem(key).budget;
        this.guestBasicCost = CharacterData.GetItem(key).basicCost;
        this.guestBasicPrice = CharacterData.GetItem(key).basicPrice;
        this.nameText.text = CharacterData.GetItem(key).name;

        this.gameObject.SetActive(true);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        this.nameText.enabled = false;

    }

    private void OnEnable()
    {
        SkillTrigger_OnGoingEffect();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (persona.Count == 4)
        {
            persona.Sort();
            int mbtiID = persona[0] * 1000 + persona[1] * 100 + persona[2] * 10 + persona[3];
            mbti = mbtiID;
            GetMBTISkill(mbti);
            persona.Clear();
        }

        if (guestDays - GameManager.Instance.gameDays == -1)
        {
            SkillTrigger_WhenDaysChanged();
            guestDays += 1;
        }

        if (GameManager.Instance.isChooseCardFinish || GameManager.Instance.isRoundEnd)
        {
            Reset();
        }
    }

    //public void GuestEffect()
    //{
    //    switch (this.guestEffectID)
    //    {
    //        case 1:
    //            GuestController.Instance.GuestEffect_ChangeJob(this);
    //            break;
    //        case 2:
    //            GuestController.Instance.GuestEffect_IgnoreRoomRentLimit(this);
    //            break;
    //        case 3:
    //            GuestController.Instance.GuestEffect_PayByNeighbour(this);
    //            break;
    //        case 4:
    //            GuestController.Instance.GuestEffect_RateMoveAway(this);
    //            break;
    //        case 5:
    //            GuestController.Instance.GuestEffect_RentIncrease(this);
    //            break;
    //        case 6:
    //            GuestController.Instance.GuestEffect_RandomBudget();
    //            break;
    //        case 7:
    //            GuestController.Instance.GuestEffect_MoveInNextBy();
    //            break;
    //        case 8:
    //            GuestController.Instance.GuestEffect_RentIncreaseByNeighbour(this);
    //            break;
    //        case 9:
    //            GuestController.Instance.GuestEffect_GenerateGuest();
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void SkillTrigger()
    {
        SkillMethod_Normal?.Invoke(this);
    }

    public void SkillTrigger_WhenMoveIn()
    {
        SkillMethod_WhenMoveIn?.Invoke(this);
    }

    public void SkillTrigger_WhenDaysChanged()
    {
        Debug.Log("该触发改变天数的技能了");
        SkillMethod_WhenDaysChanged?.Invoke(this);
    }



    public void SkillTrigger_WhenSold()
    {
        SkillMethod_WhenGuestSold?.Invoke(this);
    }

    public void SkillTrigger_WhenOtherGuestSold()
    {
        SkillMethod_WhenOtherGuestSold?.Invoke(this);
    }
    public void GetMBTISkill(int mbti)
    {
        Skill singleMBTISkill = new Skill();
        mbtiSkill = GuestMBTIData.GetItem(mbti).skillID;
        switch (mbtiSkill)
        {
            case 20:SkillMethod_WhenOtherGuestSold += singleMBTISkill.Skill_INTJ;
                break;
            case 21:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_INTP;
                break;
            case 23:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ENTP;
                break;
            case 24:SkillMethod_WhenDaysChanged += singleMBTISkill.Skill_INFJ;
                break;
            case 27:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ENFP;
                break;
            case 28:SkillMethod_WhenDaysChanged += singleMBTISkill.Skill_ISTJ;
                break;
            case 29:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ISFJ;
                break;
            case 30:SkillMethod_WhenDaysChanged += singleMBTISkill.Skill_ESTJ;
                break;
            case 31:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ESFJ;
                break;
            case 32:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ISTP;
                break;
            case 33:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ISFP;
                break;
            case 34:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ESTP;
                break;
            case 35:SkillMethod_WhenMoveIn += singleMBTISkill.Skill_ESFP;
                break;
        }
    }

    public void SkillTrigger_OnGoingEffect()
    {
        SkillMethod_OnGoingEffect?.Invoke(this);
    }

    public void GetPersonaSkill(int personaKey )
    {
        Skill singlePersonaSkill = new Skill();
       
            switch (personaKey)
            {
                case 1:
                    SkillMethod_WhenMoveIn += singlePersonaSkill.Skill_Inner;
                    break;

                case 2:
                    SkillMethod_WhenGuestSold += singlePersonaSkill.Skill_Outer;
                    break;
                case 3:
                    SkillMethod_WhenMoveIn += singlePersonaSkill.Skill_Intuition;
                    break;
                case 4:
                    SkillMethod_WhenMoveIn += singlePersonaSkill.Skill_Sensing;
                    break;
                case 5:
                    SkillMethod_WhenOtherGuestSold += singlePersonaSkill.Skill_Feeling;
                    break;
                case 6:
                    singlePersonaSkill.Skill_Thinking(this);
                    break;
                case 7:
                    SkillMethod_WhenDaysChanged += singlePersonaSkill.Skill_Judging;
                    break;
                case 8:
                    singlePersonaSkill.Skill_Perceiving();
                    break;
            }
       
        
    }

    public void GetFieldSkill(int fieldSkillID)
    {

    }

    public void Reset()
    {
        temporPersona.Clear();
        guestExtraBudget = 0;
        this.transform.SetParent(null);
        this.transform.localPosition = Vector3.zero;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        this.nameText.enabled = false;
    }

    public void ShowPersonaIcon(int personaKey)
    {
        GameObject personaIcon = Instantiate(personaPic, personaSlot.transform);
        personaIcon.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(GuestPersonalData.GetItem(personaKey).iconRoute);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireCube(this.transform.position, this.transform.parent.GetComponent<BoxCollider>().size);
    //}



}
