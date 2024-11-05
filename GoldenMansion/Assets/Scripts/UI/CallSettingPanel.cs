using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSettingPanel : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CallByESC();
    }

    public void CallByESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingPanel.SetActive(true);
        }
    }
}
