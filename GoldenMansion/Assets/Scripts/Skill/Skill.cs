using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        int newGuestID = GuestController.Instance.internID[Random.Range(0, GuestController.Instance.internID.Length)];
        GuestController.Instance.temporKey = newGuestID;
        GameObject newGuest = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);       
        newGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GuestController.Instance.GuestInApartmentPrefabStorage.Add(newGuest);
    }

    public void Skill_ESFJ(GuestInApartment guestInApartment)
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (GuestController.Instance.internID.Contains(guest.GetComponent<GuestInApartment>().key))
            {
                skillEffect.IncreaseTemporBudget(guest.GetComponent<GuestInApartment>(), 1);
            }
        }
    }

    public void Skill_ISTP(GuestInApartment guestInApartment)
    {
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count < 8)
        {
            int increaseTimes = 8 - GuestController.Instance.GetAdjancentGuest(guestInApartment).Count;
            skillEffect.IncreaseTemporBudget(guestInApartment, increaseTimes * 10);
        }
    }

    public void Skill_ISFP(GuestInApartment guestInApartment)
    {
        Apartment[] apartmentList = FindObjectsOfType<Apartment>();
        foreach (var apartment in apartmentList)
        {
            if (apartment.transform.GetComponentInChildren<GuestInApartment>() == null)
            {
                skillEffect.IncreaseTemporBudget(guestInApartment, 5);
            }
        }
    }

    public void Skill_ESTP(GuestInApartment guestInApartment)
    {
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count != 0)
        {
            bool isBigger = false;
            foreach (var guest in GuestController.Instance.GetAdjancentGuest(guestInApartment))
            {
                if (guest.GetComponent<GuestInApartment>().guestBudget > guestInApartment.guestBudget)
                {
                    isBigger = true;
                    break;
                }
            }
            if (!isBigger)
            {
                foreach (var guest in GuestController.Instance.GetAdjancentGuest(guestInApartment))
                {
                    guest.GetComponent<GuestInApartment>().guestBudget = guestInApartment.guestBudget;
                }
            }
        }
        
    }

    public void Skill_ESFP(GuestInApartment guestInApartment)
    {
        int esfpNum = 0;
        if (GuestController.Instance.GetAdjancentGuest(guestInApartment).Count != 0)
        {
            foreach (var guest in GuestController.Instance.GetAdjancentGuest(guestInApartment))
            {
                if (guest.GetComponent<GuestInApartment>().mbti == 2358)
                {
                    Debug.Log("对面的人名是"+guest.GetComponent<GuestInApartment>().guestName);
                    esfpNum += 1;
                }
                else
                {
                    break;
                }
            }
        }
        Debug.Log(GuestController.Instance.GetAdjancentGuest(guestInApartment).Count);
        if (esfpNum == 8)
        {
            Debug.Log("是你赢了");
        }
        
    }
}
