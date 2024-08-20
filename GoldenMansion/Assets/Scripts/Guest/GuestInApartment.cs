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
    public int mbti { get; set; }
    public Skill mbtiSkill { get; set; }
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

    [SerializeField] GameObject personaSlot;
    [SerializeField] GameObject personaPic;

    public List<int> persona = new List<int>();
    public List<int> temporPersona = new List<int>();

    public Action<GuestInApartment> SkillMethod;
    public Action<GuestInApartment> SkillMethod_WhenGuestSold;
    public Action<GuestInApartment, int> SkillMethod_WhenGetPersona;
    public Action<GuestInApartment> SkillMethod_WhenDaysChanged;
    public Action<GuestInApartment, int> SkillMethod_EachDays;
    public Action<GuestInApartment,int> SkillWithIntMethod;


    void Awake()
    {
        key = GuestController.Instance.temporKey;
        this.guestDays = GameManager.Instance.gameDays;
        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = CharacterData.GetItem(key).budget;
        this.guestBasicCost = CharacterData.GetItem(key).basicCost;
        this.guestBasicPrice = CharacterData.GetItem(key).basicPrice;

        //this.guestEffectID = CharacterData.GetItem(key).effectID;
        this.gameObject.SetActive(true);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;


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
            int mbtiID = persona[0] * 1000 + persona[1] * 100 + persona[2] * 10 + persona[3];
            mbti = mbtiID;
            persona.Clear();
        }

        if (guestDays - GameManager.Instance.gameDays == -1)
        {
            SkillTrigger_WhenDaysChanged();
            guestDays += 1;
        }

        if (GameManager.Instance.isChooseCardFinish||GameManager.Instance.isRoundEnd)
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
        SkillMethod?.Invoke(this);
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
    public void GetMBTISkill(int mbti)
    {
 
    }

    public void GetPersonaSkill(int personaKey )
    {
        Skill singlePersonaSkill = new Skill();
       
            switch (personaKey)
            {
                case 1:
                    SkillMethod += singlePersonaSkill.Skill_Inner;
                    break;

                case 2:
                    SkillMethod_WhenGuestSold += singlePersonaSkill.Skill_Outer;
                    break;
                case 3:
                    SkillMethod += singlePersonaSkill.Skill_Intuition;
                    break;
                case 4:
                    SkillMethod += singlePersonaSkill.Skill_Sensing;
                    break;
                case 5:
                    SkillMethod_WhenGuestSold += singlePersonaSkill.Skill_Feeling;
                    break;
                case 6:
                    isDestroyable = false;
                    break;
                case 7:
                    SkillMethod_WhenDaysChanged += singlePersonaSkill.Skill_Judging;
                    break;
                case 8:SkillMethod_EachDays += singlePersonaSkill.Skill_Perceiving;
                    break;
            }
       
        
    }

    public void Reset()
    {
        this.transform.SetParent(null);
        this.transform.localPosition = Vector3.zero;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
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
