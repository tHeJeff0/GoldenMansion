using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentController : MonoBehaviour
{
    [SerializeField] private TextAsset roomMessage;
    void Awake()
    {
        GameManager.Instance.TextLoader(roomMessage);
    }

    


}
