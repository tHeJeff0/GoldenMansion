using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInButton : MonoBehaviour
{
    public void MoveIn()
    {
        UIEventSystem.Instance.Execute();
    }
}
