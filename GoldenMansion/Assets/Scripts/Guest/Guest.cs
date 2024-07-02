using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExcelData;
using TMPro;
using Unity.VisualScripting;

public class Guest : MonoBehaviour
{
    [SerializeField] GameObject guestCardNameTextPrefab;
    [SerializeField] GameObject guestCardDescTextPrefab;
    [SerializeField] GameObject guestCardBudgetTextPrefab;
    private string guestName;
    private string guestDesc;
    public static int guestBudget;
    private TextMeshProUGUI guestNameText;
    private TextMeshProUGUI guestDescText;
    private TextMeshProUGUI guestBudgetText;
    private int key;


    private void Awake()
    {
        key = keyRandom();
        guestName = CharacterData.GetItem(key).name;
        guestDesc = CharacterData.GetItem(key).effectDesc;
        guestBudget = Random.Range(CharacterData.GetItem(key).Budget[0], CharacterData.GetItem(key).Budget[1]);
        
        guestNameText = guestCardNameTextPrefab.GetComponent<TextMeshProUGUI>();
        guestDescText = guestCardDescTextPrefab.GetComponent<TextMeshProUGUI>();
        guestBudgetText = guestCardBudgetTextPrefab.GetComponent<TextMeshProUGUI>();

        guestNameText.text = guestName;
        guestDescText.text = guestDesc;
        guestBudgetText.text = guestBudget.ToString();


    }

    public void addToStorage()
    {

        GuestController.Instance.GuestStorage.Add(key);
        Debug.Log(GuestController.Instance.GuestStorage[GuestController.Instance.GuestStorage.Count - 1]);
    }

    int keyRandom()
    {
        int randomKey = 0;
        int keyMin = 1;
        int keyMax = 1;
        var characterDataDict = CharacterData.GetDict();
        foreach (var kvp in characterDataDict)
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
        randomKey = Random.Range(keyMin, keyMax);
        return randomKey;
    }
}
