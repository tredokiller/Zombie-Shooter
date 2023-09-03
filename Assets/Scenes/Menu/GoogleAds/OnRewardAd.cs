using System;
using GoogleMobileAds.Api;
using Managers;
using UnityEngine;
using Zenject;

namespace Scenes.Menu.GoogleAds
{
    public class OnRewardAd : MonoBehaviour
    {
        private IGameManager _gameManager;
        
        [SerializeField] private int rewardCoins;
    
        private const string RewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
    
        private RewardedAd _rewardedAd;
        

        [Inject]
        private void Constructor(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        
       
        private void LoadRewardedAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            // send the request to load the ad.
            RewardedAd.Load(RewardedUnitId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad; 
                });
        }
        
        public void ShowRewardedAd()
        {
            LoadRewardedAd();
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    HandleUserEarnedReward();
                });
            }
        }
        
        
        private void HandleUserEarnedReward()
    {
        _gameManager.AddSubtractMoney(rewardCoins);
        _gameManager.SaveGame();
    }
        
    }
}
