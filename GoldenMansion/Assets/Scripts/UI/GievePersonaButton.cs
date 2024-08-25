using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GievePersonaButton : MonoBehaviour
{
    [SerializeField] private GameObject personaButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    public int personaKey;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //personaKey = 8;
        personaKey = RandomKey();
        buttonText.text = LanguageData.GetItem(GuestPersonalData.GetItem(personaKey).nameID).CHN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RandomKey()
    {
        int randomKey = 1;
        int keyMin = 1;
        int keyMax = 1;
        var personaDataDict = GuestPersonalData.GetDict();
        foreach (var kvp in personaDataDict)
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
        randomKey = Random.Range(keyMin, keyMax + 1);
        return randomKey;
    }

    public void DelieverTemporKey()
    {
        SkillController.Instance.temporPersonaKey = personaKey;
    }
}
