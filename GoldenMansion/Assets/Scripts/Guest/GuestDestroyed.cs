using ExcelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestDestroyed : MonoBehaviour
{
    public int key { get; set; }
    public int field { get; set; }
    public int mbti { get; set; }

    [SerializeField] GameObject personaSlot;
    [SerializeField] GameObject personaPic;

    public List<int> persona = new List<int>();
    public List<int> temporPersona = new List<int>();
    // Start is called before the first frame update
    public void OnInstantiate()
    {
        InitialGuest();
    }

    public void InitialGuest()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute + "inapartment");
        gameObject.SetActive(true);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        foreach (var personaid in persona)
        {
            ShowPersonaIcon(personaid);
        }
    }

    public void ShowPersonaIcon(int personaKey)
    {
        GameObject personaIcon = Instantiate(personaPic, personaSlot.transform);
        personaIcon.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(GuestPersonalData.GetItem(personaKey).iconRoute);
    }
}
