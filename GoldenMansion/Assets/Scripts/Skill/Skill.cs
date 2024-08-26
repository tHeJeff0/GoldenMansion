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
        int randomNumber = Random.Range(0, 9);
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count == randomNumber)
        {
            guestInApartment.guestExtraBudget += guestInApartment.guestBudget;
        }

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

    public void Skill_Thinking(GuestInApartment guestInApartment)
    {
        guestInApartment.isDestroyable = false;
    }

    public void Skill_Judging(GuestInApartment guestInApartment)
    {       
        skillEffect.IncreaseBasicBudget(guestInApartment, 1);
    }

    public void Skill_Perceiving()
    {
        GameManager.Instance.extraRerollTime += 1;
    }

    public void Skill_INTJ(GuestInApartment guestInApartment)
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            guest.GetComponent<GuestInApartment>().guestBudget += 1;
        }
    }

    public void Skill_INTP(GuestInApartment guestInApartment)
    {
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count == 0)
        {
            int randomExtraBudget = Random.Range(1, 11);
            guestInApartment.guestExtraBudget += randomExtraBudget;
        }
    }

    public void Skill_ENTJ()
    {

    }

    public void Skill_ENTP(GuestInApartment guestInApartment)
    {
        int increaseBudget = 0;
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count != 0)
        {
            foreach (var adjancentGuest in GuestController.Instance.GetAdjancentGuest(guestInApartment))
            {
                int budget = adjancentGuest.GetComponent<GuestInApartment>().guestBudget + adjancentGuest.GetComponent<GuestInApartment>().guestExtraBudget;
                if (budget > increaseBudget)
                {
                    increaseBudget = budget;
                }
            }
        }
        guestInApartment.guestExtraBudget += increaseBudget - guestInApartment.guestBudget;
        
    }

    public void Skill_INFJ(GuestInApartment guestInApartment)
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            skillEffect.IncreaseBasicBudget(guest.GetComponent<GuestInApartment>(), 1);
        }
    }

    public void Skill_INFP()
    {
        //已在Guest实现相关功能
    }

    public void Skill_ENFJ()
    {

    }

    public void Skill_ENFP(GuestInApartment guestInApartment)
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().persona.Contains(2) && guest.GetComponent<GuestInApartment>().mbti.ToString().Contains("2"))
            {
                skillEffect.IncreaseTemporBudget(guestInApartment, 1);
            }
        }
    }

    public void Skill_ISTJ(GuestInApartment guestInApartment)
    {
        if (ApartmentController.Instance.vaultMoney >= 5)
        {
            float vaultMoney = ApartmentController.Instance.vaultMoney;
            int plusTimes = (int) (vaultMoney / 5.0f);
            for (int i = 0; i < plusTimes; i++)
            {
                ApartmentController.Instance.vaultMoney += 1;
            }
        }
    }

    public void Skill_ISFJ(GuestInApartment guestInApartment)
    {
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject newGuest = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
                newGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
                GuestController.Instance.GuestInApartmentPrefabStorage.Add(newGuest);
            }
            skillEffect.RemoveGuest(guestInApartment);
        }
    }

    public void Skill_ESTJ(GuestInApartment guestInApartment)
    {
        int[] internID = { 8, 9, 23, 27, 31, 35, 41, 48, 51, 54, 58, 61 };
        int newGuestID = internID[Random.Range(0, internID.Length)];
        GuestController.Instance.temporKey = newGuestID;
        GameObject newGuest = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
        
        newGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GuestController.Instance.GuestInApartmentPrefabStorage.Add(newGuest);
    }

    public void Skill_ESFJ()
    {

    }

    public void Skill_ISTP()
    {

    }

    public void Skill_ISFP()
    {

    }

    public void Skill_ESTP()
    {

    }

    public void Skill_ESFP()
    {

    }
}
