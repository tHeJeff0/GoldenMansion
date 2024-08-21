using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    SkillEffect skillEffect = new SkillEffect();
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillRegister()
    {

    }

    public void Skill_Inner(GuestInApartment guestInApartment)
    {
        List<GameObject> adjancentGuest = GuestController.Instance.GetAdjancentGuest(guestInApartment);
        foreach (GameObject guest in adjancentGuest)
        {
            Debug.Log(guest.GetComponent<GuestInApartment>().key);
            if (guest.GetComponent<GuestInApartment>().isDestroyable)
            {
                skillEffect.RemoveGuest(guest.GetComponent<GuestInApartment>());
            }
        }
    }

    public void Skill_Outer(GuestInApartment guestInApartment)
    {
        skillEffect.IncreaseTemporPrice(guestInApartment, guestInApartment.guestBasicPrice);
    }

    public void Skill_Intuition(GuestInApartment guestInApartment)
    {
        int randomID = Random.Range(1, 8);
        skillEffect.GetTemporPersona(guestInApartment, randomID );
    }

    public void Skill_Sensing(GuestInApartment guestInApartment)
    {
        int randomNumber = Random.Range(0, 100);
        int rate = 50;
        if (randomNumber >= rate)
        {
            skillEffect.IncreaseTemporBudget(guestInApartment, guestInApartment.guestBudget);
        }
        else
        {
            skillEffect.IncreaseTemporBudget(guestInApartment, -guestInApartment.guestBudget);
        }
    }

    public void Skill_Feeling(GuestInApartment guestInApartment)
    {
        int randomNumber = Random.Range(0, 100);
        int rate = 50;
        if (randomNumber >= rate)
        {
            skillEffect.RemoveGuest(guestInApartment);

        }
    }

    //public void Skill_Thinking(GuestInApartment guestInApartment,int skillID)
    //{
    //    guestInApartment.isDestroyable = false;
    //}

    public void Skill_Judging(GuestInApartment guestInApartment)
    {       
        skillEffect.IncreaseBasicBudget(guestInApartment, 1);
    }

    public void Skill_Perceiving(GuestInApartment guestInApartment,int daysRequire)
    {
        if (GameManager.Instance.gameDays - guestInApartment.guestDays == daysRequire)
        {
            guestInApartment.key = GuestController.Instance.RandomKey();
        }
    }
}
