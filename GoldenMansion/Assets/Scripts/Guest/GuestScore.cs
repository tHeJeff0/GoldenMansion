using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestScore : MonoBehaviour
{

    public string guestImageRouteStats;
    public string guestNameStats;
    public int guestKeyStats;
    public int guestScoreStats;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GuestScore(int guestKey, string guestImageRoute, string guestName, int guestScore)
    {
        guestKeyStats = guestKey;
        guestImageRouteStats = guestImageRoute;
        guestNameStats = guestName;
        guestScoreStats = guestScore;
    }
}
