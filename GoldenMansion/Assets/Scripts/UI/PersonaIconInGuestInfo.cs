using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersonaIconInGuestInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject personaEffectDesc;
    private GameObject personaEffectDescCopy;
    public int personaKey;

    // Start is called before the first frame update
    void Start()
    {        
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        personaEffectDesc.SetActive(true);
        personaEffectDescCopy = Instantiate(personaEffectDesc, transform);
        int personaEffectDescID = GuestPersonalData.GetItem(personaKey).descID;
        string skillDescText = LanguageData.GetItem(personaEffectDescID).CHN;
        personaEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].text = skillDescText;

        personaEffectDescCopy.transform.SetParent(GameObject.Find("Canvas").transform);
        personaEffectDesc.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(personaEffectDescCopy);
    }
}
