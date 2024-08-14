using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillController : MonoBehaviour
{
    private static SkillController instance;

    //public delegate void SkillSpace();

    //public event SkillSpace onGet;
    //public event SkillSpace onDaysChanged;
    //public event SkillSpace onGuestMoveIn;
    //public event SkillSpace onPayRent;
    //public event SkillSpace afterPayRent;
    //public event SkillSpace onRoundEnd;
    //public event SkillSpace onBuyGuest;
    //public event SkillSpace afterBuyGuest;
    //public event SkillSpace onSellGuest;
    //public event SkillSpace afterSellGuest;


    public static SkillController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<SkillController>();
                if (instance == null)
                {
                    GameObject skillController = new GameObject("SkillController");
                    instance = skillController.AddComponent<SkillController>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillTrigger()
    {

    }

    //public void OnGet()
    //{
    //    onGet?.Invoke();
    //}

    //public void OnDaysChanged()
    //{
    //    onDaysChanged?.Invoke();
    //}

    //public void OnGuestMoveIn()
    //{
    //    onGuestMoveIn?.Invoke();
    //}

    //public void OnPayRent()
    //{
    //    onPayRent?.Invoke();
    //}

    //public void AfterPayRent()
    //{
    //    afterPayRent?.Invoke();
    //}

    //public void OnRoundEnd()
    //{
    //    onRoundEnd?.Invoke();
    //}

    //public void OnBuyGuest()
    //{
    //    onBuyGuest?.Invoke();
    //}

    //public void AfterBuyGuest()
    //{
    //    afterBuyGuest?.Invoke();
    //}

    //public void OnSellGuest()
    //{
    //    onSellGuest?.Invoke();
    //}

    //public void AfterSellGuest()
    //{
    //    afterSellGuest?.Invoke();
    //}
}
