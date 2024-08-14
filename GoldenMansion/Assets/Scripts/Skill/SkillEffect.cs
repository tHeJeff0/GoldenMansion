using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeJob(GuestInApartment guestInApartment,int jobID)
    {
        guestInApartment.key = jobID;
    }

    public void GetTemporPersona(GuestInApartment guestInApartment,int personaID)
    {
        guestInApartment.temporPersona.Add(personaID);
    }

    public void RemovePersona(GuestInApartment guestInApartment,int personaID)
    {
        guestInApartment.persona.Remove(personaID);
    }

    public void IncreaseTemporBudget(GuestInApartment guestInApartment,int temporBudget)
    {
        guestInApartment.guestExtraBudget += temporBudget;
    }

    public void IncreaseBasicBudget(GuestInApartment guestInApartment,int budget)
    {
        guestInApartment.guestBudget += budget;
    }

    public void IncreaseTemporPrice(GuestInApartment guestInApartment, int temporPrice)
    {
        guestInApartment.guestExtraPrice += temporPrice;
    }

    public void IncreaseBasicPrice(GuestInApartment guestInApartment,int price)
    {
        guestInApartment.guestBasicPrice += price;
    }

    public void SellGuest(GuestInApartment guestInApartment)
    {
        ApartmentController.Instance.vaultMoney += guestInApartment.guestBasicPrice + guestInApartment.guestExtraPrice;
        GuestController.Instance.GuestInApartmentPrefabStorage.Remove(guestInApartment.gameObject);
        Destroy(guestInApartment.gameObject);
        foreach (GameObject guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().persona.Contains(5))
            {
                guest.GetComponent<GuestInApartment>().SkillMethod_WhenGuestSold?.Invoke(guest.GetComponent<GuestInApartment>());
            }
        }
    }

    public void RemoveGuest(GuestInApartment guestInApartment)
    {
        GuestController.Instance.GuestInApartmentPrefabStorage.Remove(guestInApartment.gameObject);
        Destroy(guestInApartment.gameObject);
    }

    public void BanSellGuestFunction(GameObject sellGuestButton)
    {
        sellGuestButton.SetActive(false);
    }
}
