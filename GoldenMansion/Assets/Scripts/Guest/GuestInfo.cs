using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuestInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
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

    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] GameObject personaSlot;
    [SerializeField] GameObject personaPic;
    [SerializeField] GameObject mbtiPic;

    private TextMeshProUGUI budgetText;
    private TextMeshProUGUI priceText;
    private Image guestPortrait;
    private bool isSelected = false;

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
        nameText.text = CharacterData.GetItem(key).name;
        budgetText.text = budgetShown.ToString();

        foreach (var persona in personaID)
        {
            ShowPersonaIcon(persona);
        }

        if (mbtiID != 0)
        {
            ShowMBTIIcon(mbtiID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeIntoHighlighted();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected)
        {
            isSelected = false;
            StopHighlighted();
            StorageController.Instance.guestSelected.Remove(gameObject);
        }
        else
        {
            isSelected = true;
            ChangeIntoHighlighted();
            StorageController.Instance.guestSelected.Add(gameObject);
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

    public void OnSell()
    {

        if (GameManager.Instance.isAllowSell)
        {
            List<GameObject> temporGuestList = new List<GameObject>();
            temporGuestList.AddRange(GuestController.Instance.GuestInApartmentPrefabStorage);
            foreach (var temporGuest in temporGuestList)
            {
                if (temporGuest.GetComponent<GuestInApartment>().guestElementID == elementID)
                {
                    GuestInApartment guestBeenSoldData = temporGuest.GetComponent<GuestInApartment>();
                    guestBeenSoldData.SkillTrigger_WhenSold();
                    ApartmentController.Instance.vaultMoney += guestBeenSoldData.guestBasicPrice + guestBeenSoldData.guestExtraPrice;
                    foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
                    {
                        if (guest.GetComponent<GuestInApartment>().guestElementID == elementID)
                        {                           
                            GuestController.Instance.GuestInApartmentPrefabStorage.Remove(guest);
                            Destroy(guest);
                            return;
                        }
                    }                   
                    SkillController.Instance.guestSoldCount += 1;
                    SkillController.Instance.SkillTrigger_EShop("sell");
                    List<GameObject> temporList = new List<GameObject>();
                    foreach (var guestToTriggerSkill in temporList)
                    {
                        guestToTriggerSkill.GetComponent<GuestInApartment>().SkillTrigger_WhenOtherGuestSold();
                    }
                    StorageController.Instance.guestStorage.Remove(gameObject);
                    StorageController.Instance.guestSelected.Remove(gameObject);
                    StorageController.Instance.guestFilteredStorage.Remove(gameObject);
                    Destroy(gameObject);
                }
            }            
        }
        else
        {
            Debug.Log("还不允许出售");
        }

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
}
