using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageButton : MonoBehaviour
{
    public void OpenStorage()
    {
        UIController.Instance.ShowStoragePanel();
    }
}
