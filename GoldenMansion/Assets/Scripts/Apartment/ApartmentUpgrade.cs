using ExcelData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ApartmentUpgrade : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private int roomKey;
    public bool roomKeyIsSend { get; set; }


    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(RoomData.GetItem(roomKey).roomPicRoute);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.gameObject == this.gameObject)
        //        {
        //            Debug.Log("点击了" + RoomData.GetItem(roomKey).name);
        //            ApartmentController.Instance.apartmentUpgradeKey = this.roomKey;
        //            this.roomKeyIsSend = true;
        //        }
        //    }
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("点击了" + RoomData.GetItem(roomKey).name);
        ApartmentController.Instance.apartmentUpgradeKey = this.roomKey;
        this.roomKeyIsSend = true;
    }
}
