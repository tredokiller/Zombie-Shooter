using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(status => { });
    }
}
