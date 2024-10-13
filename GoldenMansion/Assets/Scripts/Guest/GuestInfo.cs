using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuestInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
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

    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private TextMeshProUGUI nameText;
    
    private TextMeshProUGUI budgetText;
    private TextMeshProUGUI priceText;
    private Image guestPortrait;
    private bool isSelected;

    public List<int> personaID = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        priceShown = basicPrice + extraPrice;
        budgetShown = basicBudget + extraBudget;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 1);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 0);
        
    }

    //private void OnMouseEnter()
    //{
        
    //    if (CompareTag("GuestInfoCard"))
    //    {
    //        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    //        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 1);
    //    }
    //    else if (CompareTag("JobText"))
    //    {
    //        Debug.Log("显示职业描述");
    //    }
        
        
    //}

    //private void OnMouseExit()
    //{
        
    //    if (CompareTag("GuestInfoCard"))
    //    {
    //        transform.Find("Bottom").GetComponent<Image>().color = new Color(1, 1, 1, 0);
    //        transform.Find("Shadow").GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 0);
    //    }
    //    else if (CompareTag("JobText"))
    //    {
    //        Debug.Log("隐藏职业描述");
    //    }

    //}


}
