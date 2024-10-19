using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JobTextDesc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject jobEffectDesc;
    private GameObject jobEffectDescCopy;
    public void OnPointerEnter(PointerEventData eventData)
    {
        jobEffectDesc.SetActive(true);
        jobEffectDescCopy = Instantiate(jobEffectDesc,transform);
        int characterKey;
        if (GetComponentInParent<GuestInfo>() != null)
        {
            characterKey = GetComponentInParent<GuestInfo>().key;
        }
        else
        {
            characterKey = GetComponentInParent<GuestInfoWhenGivePersona>().key;
        }

        int jobKey = CharacterData.GetItem(characterKey).field;
        int skillKey = FieldData.GetItem(jobKey).skillID;
        int skillDescKey = SkillData.GetItem(skillKey).descID;
        string skillDescText = LanguageData.GetItem(skillDescKey).CHN;
        jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].text = skillDescText;      
        
        jobEffectDescCopy.transform.SetParent(GameObject.Find("Canvas").transform);
        jobEffectDesc.SetActive(false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(jobEffectDescCopy);
    }

}
