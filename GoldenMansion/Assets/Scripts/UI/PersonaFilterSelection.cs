using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PersonaFilterSelection : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public int key { get; set; }
    public bool isSelected { get; set; } = false;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        int languageID = GuestPersonalData.GetItem(key).nameID;
        GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).CHN;
    }

    // Update is called once per frame
    void Update()
    {
        if (StorageController.Instance.isFilterMode == false)
        {
            isSelected = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FilterSelect();
    }

    void FilterSelect()
    {
        if (!isSelected)
        {
            isSelected = true;
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            FiltThisGuest();
        }
        else
        {
            isSelected = false;
            StopFiltThisGuest();
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }

    void FiltThisGuest()
    {
        var filtedGuest = StorageController.Instance.guestStorage.Where(
            obj => obj.GetComponent<GuestInApartment>().persona.Contains(key)
            ).ToList();
        StorageController.Instance.guestFilteredStorage.AddRange(filtedGuest);
        StorageController.Instance.filterWaitingUpdate = true;
        StorageController.Instance.isFilterMode = true;
        StorageController.Instance.filterSelectedCount += 1;
    }

    void StopFiltThisGuest()
    {
        for (int i = StorageController.Instance.guestFilteredStorage.Count; i > 0; i--)
        {
            List<int> personaKey = StorageController.Instance.guestFilteredStorage[i - 1].GetComponent<GuestInApartment>().persona;
            if (personaKey.Contains(key))
            {
                StorageController.Instance.guestFilteredStorage.RemoveAt(i - 1);
            }
        }
        StorageController.Instance.filterWaitingUpdate = true;
        StorageController.Instance.filterSelectedCount -= 1;
    }

}
