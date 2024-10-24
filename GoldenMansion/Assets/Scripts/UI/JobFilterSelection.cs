using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JobFilterSelection : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public int key { get; set; }
    public bool isSelected { get; set; } = false;
    private void Awake()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        int languageID = FieldData.GetItem(key).nameID;
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
            FiltThisGuest();
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        List<string> excistedFiltedID = new List<string>();
        foreach (var guest in StorageController.Instance.guestFilteredStorage)
        {
            excistedFiltedID.Add(guest.GetComponent<GuestInApartment>().guestElementID);
            
        }
        var filtedGuest = StorageController.Instance.guestStorage.Where(
            obj => key == obj.GetComponent<GuestInApartment>().field && !excistedFiltedID.Contains(obj.GetComponent<GuestInApartment>().guestElementID)
            ).ToList();
        StorageController.Instance.guestFilteredStorage.AddRange(filtedGuest);
        StorageController.Instance.filterWaitingUpdate = true;
        StorageController.Instance.isFilterMode = true;
        StorageController.Instance.filterSelectedCount += 1;
        StorageController.Instance.jobFilterSelected.Add(key);
    }

    void StopFiltThisGuest()
    {
        for (int i = StorageController.Instance.guestFilteredStorage.Count; i > 0; i--)
        {
            int fieldKey = CharacterData.GetItem(StorageController.Instance.guestFilteredStorage[i-1].GetComponent<GuestInApartment>().key).field;
            if (fieldKey == key)
            {
                List<int> guestPersona = new List<int>();
                guestPersona.AddRange(StorageController.Instance.guestFilteredStorage[i - 1].GetComponent<GuestInApartment>().persona);
                bool isInPersonaFilter = guestPersona.Any(item => StorageController.Instance.personaFilterSelected.Contains(item));
                if (!isInPersonaFilter)
                {
                    StorageController.Instance.guestFilteredStorage.RemoveAt(i - 1);
                }

            }
        }
        StorageController.Instance.filterWaitingUpdate = true;
        StorageController.Instance.filterSelectedCount -= 1;
        StorageController.Instance.jobFilterSelected.Remove(key);
    }
}
