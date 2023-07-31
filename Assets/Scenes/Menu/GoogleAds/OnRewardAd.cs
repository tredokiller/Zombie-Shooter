using UnityEngine;
using GoogleMobileAds.Api;

public class OnRewardAd : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private int rewardCoins;
    
    private string RewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    private RewardedAd _rewardedAd;
    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(RewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }
    
    private void HandleUserEarnedReward(object sender, Reward reward)
    {
        int playerCoins = menuManager.PlayerCoins;
        playerCoins += rewardCoins;
        MenuManager.OnPlayerCoinsChange.Invoke(playerCoins);
    }

    public void ShowAd()
    {
        if(_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }
}
