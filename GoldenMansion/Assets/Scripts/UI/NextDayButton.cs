using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlusGameDays()
    {
        GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(GetComponent<ButtonSound>().clickSound);
        GameManager.Instance.gameDays += 1;
        GameManager.Instance.isChooseCardFinish = true;
        if (GameManager.Instance.mediaDays > 0)
        {
            GameManager.Instance.mediaDays -= 1;
        }
        GameManager.Instance.rerollTime = GameManager.Instance.basicRerollTime + GameManager.Instance.extraRerollTime;
    }
}
