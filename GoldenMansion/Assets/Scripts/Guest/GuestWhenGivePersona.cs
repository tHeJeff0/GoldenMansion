using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestWhenGivePersona : MonoBehaviour
{
    public int key { get; set; }
    public int priceShown { get; set; }
    public int basicPrice { get; set; }
    public int extraPrice { get; set; }
    public string portraitRoute { get; set; }
    public int mbtiID { get; set; }
    public int elementCount { get; set; }

    private TextMeshProUGUI priceText;
    private Image guestPortrait;
    public GameObject guestInStoragePrefab;
    public RectTransform parentRectTransform;

    public List<int> personaID = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        priceShown = basicPrice + extraPrice;

        guestPortrait = this.GetComponentsInChildren<Image>()[0];
        priceText = this.GetComponentInChildren<TextMeshProUGUI>();

        portraitRoute = CharacterData.GetItem(key).portraitRoute;
        guestPortrait.sprite = Resources.Load<Sprite>(portraitRoute);
        priceText.text = priceShown.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    public void GetPersona()
    {
        if (SkillController.Instance.temporPersonaKey != 0)
        {
            GuestController.Instance.GuestInApartmentPrefabStorage[elementCount].GetComponent<GuestInApartment>().persona.Add(SkillController.Instance.temporPersonaKey);
            GuestController.Instance.GuestInApartmentPrefabStorage[elementCount].GetComponent<GuestInApartment>().ShowPersonaIcon(SkillController.Instance.temporPersonaKey);
            SkillController.Instance.temporPersonaKey = 0;
        }

    }


    void OnClick()
    {

        // 仅在按下鼠标左键时检测点击  
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标在屏幕上的位置  
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, Input.mousePosition, null, out Vector2 localPoint);
            // 使用射线投射来检测碰撞  
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(localPoint.x,localPoint.y,1.0f), Vector3.forward);

            // 检查是否击中了我们的预设  
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                
                // 在这里处理点击事件  
                GetPersona();
            }
        }
    }
}
