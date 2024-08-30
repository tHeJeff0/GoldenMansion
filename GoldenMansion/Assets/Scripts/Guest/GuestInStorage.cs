using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestInStorage : MonoBehaviour
{
    public int key { get; set; }
    public int priceShown { get; set; }
    public int basicPrice { get; set; }
    public int extraPrice { get; set; }
    public string portraitRoute { get; set; }
    public int mbtiID { get; set; }
    public int elementCount { get; set; }


    [SerializeField] private GameObject sellButton;
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private TextMeshProUGUI nameText;
    private TextMeshProUGUI priceText;
    private Image guestPortrait;
    public GameObject guestInStoragePrefab;


    public List<int> personaID = new List<int>();

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        priceShown = basicPrice + extraPrice;

        guestPortrait = this.GetComponentsInChildren<Image>()[0];
        priceText = this.GetComponentInChildren<TextMeshProUGUI>();

        portraitRoute = CharacterData.GetItem(key).portraitRoute;
        guestPortrait.sprite = Resources.Load<Sprite>(portraitRoute);
        priceText.text = priceShown.ToString();
        nameText.text = CharacterData.GetItem(key).name;
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();

        
    }

    void OnClick()
    {

        // 仅在按下鼠标左键时检测点击  
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标在屏幕上的位置  
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, Input.mousePosition, null, out Vector2 localPoint);
            // 使用射线投射来检测碰撞  
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(localPoint.x, localPoint.y, 1.0f), Vector3.forward);

            // 检查是否击中了我们的预设  
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log(elementCount);
                // 在这里处理点击事件  
                sellButton.SetActive(true);
            }
        }
    }

    public void OnSell()
    {
        if (GameManager.Instance.isAllowSell)
        {
            GameObject guestBeenSold = GuestController.Instance.GuestInApartmentPrefabStorage[elementCount];
            GuestInApartment guestBeenSoldData = guestBeenSold.GetComponent<GuestInApartment>();
            guestBeenSoldData.SkillTrigger_WhenSold();
            ApartmentController.Instance.vaultMoney += guestBeenSoldData.guestBasicPrice + guestBeenSoldData.guestExtraPrice;
            Destroy(GuestController.Instance.GuestInApartmentPrefabStorage[elementCount]);
            GuestController.Instance.GuestInApartmentPrefabStorage.RemoveAt(elementCount);
            SkillController.Instance.guestSoldCount += 1;
            UIController.Instance.UpdateVaultMoneyText();
            SkillController.Instance.SkillTrigger_EShop("sell");
            List<GameObject> temporList = new List<GameObject>();
            temporList.AddRange(GuestController.Instance.GuestInApartmentPrefabStorage);
            foreach (var guest in temporList)
            {
                guest.GetComponent<GuestInApartment>().SkillTrigger_WhenOtherGuestSold();
            }
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("还不允许出售");
        }
        
    }

    

    
}
