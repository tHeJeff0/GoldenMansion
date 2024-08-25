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

    public void Skill_ENTP()
    {

    }

    public void Skill_INFJ()
    {

    }

    public void Skill_INFP()
    {

    }

    public void Skill_ENFJ()
    {

    }

    public void Skill_ENFP()
    {

    }

    public void Skill_ISTJ()
    {

    }

    public void Skill_ISFJ()
    {

    }

    public void Skill_ESTJ()
    {

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
