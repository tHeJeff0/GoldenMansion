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
            Destroy(guest.gameObject);
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
}
