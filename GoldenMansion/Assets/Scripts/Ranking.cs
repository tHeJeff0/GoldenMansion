using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    private static Ranking instance;
    [SerializeField] GameObject guestRankingDropDown;
    [SerializeField] GameObject guestRankingItemPrefab;
    private List<GuestScore> guestScores = new List<GuestScore>();

    public static Ranking Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<Ranking>();
                if (instance == null)
                {
                    GameObject ranking = new GameObject("Ranking");
                    instance = ranking.AddComponent<Ranking>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void AddScore(int guestKey,int guestScore)
    {
        GuestScore guest = guestScores.FirstOrDefault(guest => guest.guestKeyStats == guestKey);
        if (guest != null)
        {
            guest.guestScoreStats += guestScore;
        }
        else
        {
            guestScores.Add(new GuestScore(guestKey, CharacterData.GetItem(guestKey).portraitRoute, CharacterData.GetItem(guestKey).name, guestScore));

        }
        UpdateRank();
    }

    void UpdateRank()
    {
        guestScores = guestScores.OrderByDescending(guest => guest.guestScoreStats).ToList();
        if (guestRankingDropDown.transform.childCount != 0)
        {
            foreach (Transform child in guestRankingDropDown.transform)
            {
                Destroy(child.gameObject);
            }
        }
        

        foreach (var guest in guestScores)
        {
            GameObject newItem = Instantiate(guestRankingItemPrefab, guestRankingDropDown.transform);
            newItem.transform.Find("Portrait").GetComponent<Image>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(guest.guestKeyStats).portraitRoute);
            newItem.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = CharacterData.GetItem(guest.guestKeyStats).name;
            newItem.transform.Find("StatsText").GetComponent<TextMeshProUGUI>().text = guest.guestScoreStats.ToString();
        }
    }
}
