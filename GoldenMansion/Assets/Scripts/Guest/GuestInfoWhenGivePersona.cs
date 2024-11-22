using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuestInfoWhenGivePersona : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public int key { get; set; }
    public int priceShown { get; set; }
    public int basicPrice { get; set; }
    public int extraPrice { get; set; }
    public int budgetShown { get; set; }
    public int basicBudget { get; set; }
    public int extraBudget { get; set; }
    public string portraitRoute { get; set; }
    public int mbtiID { get; set; }
    public string elementID { get; set; }
    public int fieldID { get; set; }
    public bool isSelected { get; set; }

    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] GameObject personaSlot;
    [SerializeField] GameObject personaPic;
    [SerializeField] GameObject mbtiPic;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip hoverAudio;

    private TextMeshProUGUI budgetText;
    private TextMeshProUGUI priceText;
    private Image guestPortrait;

    public List<int> personaID = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        priceShown = basicPrice + extraPrice;
        budgetShown = basicBudget + extraBudget;

        fieldID = CharacterData.GetItem(key).field;

        guestPortrait = GetComponentsInChildren<Image>()[2];
        nameText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        budgetText = GetComponentsInChildren<TextMeshProUGUI>()[4];
        priceText = GetComponentsInChildren<TextMeshProUGUI>()[6];

        portraitRoute = CharacterData.GetItem(key).portraitRoute;
        guestPortrait.sprite = Resources.Load<Sprite>(portraitRoute);
        priceText.text = priceShown.ToString();
        switch (GameManager.Instance.Language)
        {
            case 1:
                nameText.text = LanguageData.GetItem(CharacterData.GetItem(key).nameID).CHN;
                break;
            case 2:
                nameText.text = LanguageData.GetItem(CharacterData.GetItem(key).nameID).ENG;
                break;
            case 3:
                nameText.text = LanguageData.GetItem(CharacterData.GetItem(key).nameID).TCHN;
                break;
        }
        budgetText.text = budgetShown.ToString();

        GetPersonaMessage();

        foreach (var persona in personaID)
        {
            ShowPersonaIcon(persona);
        }

        if (mbtiID != 0)
        {
            ShowMBTIIcon(mbtiID);
        }

        TranslateSelfTitle("JobText", GameManager.Instance.Language);
        TranslateSelfTitle("PersonaText", GameManager.Instance.Language);
        TranslateSelfTitle("BudgetText", GameManager.Instance.Language);
        TranslateSelfTitle("PriceText", GameManager.Instance.Language);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.Find("JobEffectDesc(Clone)") != null)
        {
            Destroy(GameObject.Find("JobEffectDesc(Clone)"));
        }
        if (GameObject.Find("PersonaEffectDesc(Clone)") != null)
        {
            Destroy(GameObject.Find("PersonaEffectDesc(Clone)"));
        }
        if (personaID.Count < 4 && mbtiID == 0)
        {
            GetPersona();
            GameObject.Find("NewGivePersonaPanel").GetComponent<NewGivePersona>().CloseThisPanel();
            UIController.Instance.givePersonaButtonSelected.GetComponent<GievePersonaButton>().personaKey = 0;
            UIController.Instance.givePersonaButtonSelected.SetActive(false);
            SaveSystem.Instance.SaveData(SaveSystem.Instance.saveName);
        }
        else
        {
            Debug.Log("超出人格限制");
            SkillController.Instance.temporPersonaKey = 0;
            GameObject.Find("NewGivePersonaPanel").GetComponent<NewGivePersona>().CloseThisPanel();            
        }
        AudioSource.volume = GameManager.Instance.SFVolume;
        AudioSource.PlayOneShot(clickAudio);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeIntoHighlighted();
        if (!isSelected)
        {
            AudioSource.volume = GameManager.Instance.SFVolume;
            AudioSource.PlayOneShot(hoverAudio);
        }
    }

    public void GetPersona()
    {
        if (SkillController.Instance.temporPersonaKey != 0)
        {
            foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
            {
                if (guest.GetComponent<GuestInApartment>().guestElementID == elementID)
                {
                    guest.GetComponent<GuestInApartment>().persona.Add(SkillController.Instance.temporPersonaKey);
                    guest.GetComponent<GuestInApartment>().ShowPersonaIcon(SkillController.Instance.temporPersonaKey);
                    guest.GetComponent<GuestInApartment>().GetPersonaSkill(SkillController.Instance.temporPersonaKey);
                }
            }
            SkillController.Instance.temporPersonaKey = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            StopHighlighted();
        }
    }

    void ChangeIntoHighlighted()
    {
        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 1);
    }

    void StopHighlighted()
    {
        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 0);
    }

    public void ShowPersonaIcon(int personaKey)
    {
        GameObject personaIcon = Instantiate(personaPic, personaSlot.transform);
        personaIcon.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(GuestPersonalData.GetItem(personaKey).iconRoute);
        personaIcon.GetComponent<PersonaIconInGuestInfo>().personaKey = personaKey;
    }

    public void ShowMBTIIcon(int mbtiKey)
    {
        GameObject mbtiIcon = Instantiate(mbtiPic, personaSlot.transform);
        mbtiIcon.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(GuestMBTIData.GetItem(mbtiKey).iconRoute);

    }

    void TranslateSelfTitle(string titleName, int languageID)
    {
        switch (languageID)
        {
            case 1:
                transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text).CHN;
                break;
            case 2:
                transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text).ENG;
                break;
            case 3:
                transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(transform.Find(titleName).Find("Title").GetComponent<TextMeshProUGUI>().text).TCHN;
                break;
        }

    }

    void GetPersonaMessage()
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().guestElementID == elementID)
            {
                personaID.AddRange(guest.GetComponent<GuestInApartment>().persona);
            }
        }
    }
}
