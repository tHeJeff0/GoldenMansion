using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MBTIInGuestInfoDesc : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] GameObject mbtiEffectDesc;
    private GameObject mbtiEffectDescCopy;
    public int mbtiKey;

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
        mbtiEffectDesc.SetActive(true);
        mbtiEffectDescCopy = Instantiate(mbtiEffectDesc, transform);
        int mbtiEffectDescID = GuestMBTIData.GetItem(mbtiKey).descID;
        string skillDescText = LanguageData.GetItem(mbtiEffectDescID).CHN;
        switch (GameManager.Instance.Language)
        {
            case 1: 
                skillDescText = LanguageData.GetItem(mbtiEffectDescID).CHN;
                break;
            case 2:
                skillDescText = LanguageData.GetItem(mbtiEffectDescID).ENG;
                break;
            case 3:
                skillDescText = LanguageData.GetItem(mbtiEffectDescID).TCHN;
                break;
            default:
                skillDescText = LanguageData.GetItem(mbtiEffectDescID).CHN;
                break;
        }
        mbtiEffectDescCopy.GetComponentsInChildren<TextMeshProUGUI>()[0].text = skillDescText;

        mbtiEffectDescCopy.transform.SetParent(GameObject.Find("Canvas").transform);
        mbtiEffectDesc.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(mbtiEffectDescCopy);
    }
}
