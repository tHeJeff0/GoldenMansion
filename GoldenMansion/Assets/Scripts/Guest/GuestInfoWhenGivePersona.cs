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
        nameText.text = CharacterData.GetItem(key).name;
        budgetText.text = budgetShown.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GetPersona();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeIntoHighlighted();
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
}
