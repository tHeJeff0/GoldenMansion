using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GievePersonaButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private GameObject personaButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject personaDesc;
    public int personaKey;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //personaKey = 1;
        personaKey = RandomKey();
        personaButton.transform.Find("Button").Find("BottomPic").GetComponent<Image>().sprite = Resources.Load<Sprite>(GuestPersonalData.GetItem(personaKey).iconRoute+"L");
        //buttonText.text = LanguageData.GetItem(GuestPersonalData.GetItem(personaKey).nameID).CHN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RandomKey()
    {
        int randomKey = 1;
        int keyMin = 1;
        int keyMax = 1;
        var personaDataDict = GuestPersonalData.GetDict();
        foreach (var kvp in personaDataDict)
        {
            int key = kvp.Key;
            if (key > keyMax)
            {
                keyMax = key;
            }
            if (key < keyMin)
            {
                keyMin = key;
            }
        }
        randomKey = Random.Range(keyMin, keyMax + 1);
        return randomKey;
    }

    public void DelieverTemporKey()
    {
        SkillController.Instance.temporPersonaKey = personaKey;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOLocalMoveY(10, 0.05f);
        personaDesc.SetActive(true);
        int languageID = GuestPersonalData.GetItem(personaKey).descID;
        personaDesc.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).CHN;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOLocalMoveY(0, 0.05f);
        personaDesc.SetActive(false);
    }
}
