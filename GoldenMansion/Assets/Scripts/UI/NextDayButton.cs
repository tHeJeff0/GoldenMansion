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
        GameManager.Instance.gameDays += 1;
        GameManager.Instance.isChooseCardFinish = true;
        GameManager.Instance.extraRerollTime = 0;
        if (GameManager.Instance.mediaDays > 0)
        {
            GameManager.Instance.mediaDays -= 1;
        }

    }
}
