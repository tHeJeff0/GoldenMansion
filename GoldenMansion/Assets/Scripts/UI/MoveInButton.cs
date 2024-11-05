using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInButton : MonoBehaviour
{
    public void MoveIn()
    {
        UIEventSystem.Instance.Execute();
    }

    public void GetVolume()
    {
        transform.Find("NextDayButtonVoice").GetComponent<AudioSource>().volume = GameManager.Instance.SFVolume;
    }
}
