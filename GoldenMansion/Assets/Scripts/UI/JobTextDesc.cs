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
        int skillDescKey1 = SkillData.GetItem(skillKey).descID;
        int skillDescKey2 = SkillData.GetItem(skillKey).descID + 100;
        int skillDescKey3 = SkillData.GetItem(skillKey).descID + 200;
        string skillDescText1 = LanguageData.GetItem(skillDescKey1).CHN;
        string skillDescText2 = LanguageData.GetItem(skillDescKey2).CHN;
        string skillDescText3 = LanguageData.GetItem(skillDescKey3).CHN;
        switch (GameManager.Instance.Language)
        {
            case 1:
                skillDescText1 = LanguageData.GetItem(skillDescKey1).CHN;
                skillDescText2 = LanguageData.GetItem(skillDescKey2).CHN;
                skillDescText3 = LanguageData.GetItem(skillDescKey3).CHN;
                break;
            case 2:
                skillDescText1 = LanguageData.GetItem(skillDescKey1).ENG;
                skillDescText2 = LanguageData.GetItem(skillDescKey2).ENG;
                skillDescText3 = LanguageData.GetItem(skillDescKey3).ENG;
                break;
            case 3:
                skillDescText1 = LanguageData.GetItem(skillDescKey1).TCHN;
                skillDescText2 = LanguageData.GetItem(skillDescKey2).TCHN;
                skillDescText3 = LanguageData.GetItem(skillDescKey3).TCHN;
                break;
            default:
                skillDescText1 = LanguageData.GetItem(skillDescKey1).CHN;
                skillDescText2 = LanguageData.GetItem(skillDescKey2).CHN;
                skillDescText3 = LanguageData.GetItem(skillDescKey3).CHN;
                break;
        }
        jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].text = skillDescText1;
        jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].text = skillDescText2;
        jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].text = skillDescText3;

        switch (GetSkillDescIndex(skillKey,SkillController.Instance.GetFieldCount(jobKey)))
        {
            case 0:
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].color = new Color(1, 1, 1, 0.3f);
                break;
            case 1:
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1, 1.0f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].color = new Color(1, 1, 1, 0.3f);
                break;
            case 2:
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1, 1.0f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].color = new Color(1, 1, 1, 0.3f);
                break;
            case 3:
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].color = new Color(1, 1, 1, 1.0f);
                break;
            default:
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1, 0.3f);
                jobEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[2].color = new Color(1, 1, 1, 1.0f);
                break;
        }

        jobEffectDescCopy.transform.SetParent(GameObject.Find("Canvas").transform);
        jobEffectDesc.SetActive(false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(jobEffectDescCopy);
    }

    public int GetSkillDescIndex(int skillID,int guestNumber)
    {
        int index = 0;
        if (guestNumber == 0 || guestNumber == 1)
        {
            return 0;
        }
        else
        {
            for (int i = 0; i < SkillData.GetItem(skillID).conditionValue.Length; i++)
            {
                if (guestNumber >= SkillData.GetItem(skillID).conditionValue[i])
                {
                    index = i + 1;
                }
            }
            
        }
        return index;
    }
}
