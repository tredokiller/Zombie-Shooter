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
    
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        [SerializeField] private int rewardCoins;

        private const string RewardedUnitId = "ca-app-pub-3940256099942544/5224354917";

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
            _gameManager.AddSubtractMoney(rewardCoins);
        }

        public void ShowAd()
        {
            if(_rewardedAd.IsLoaded())
                _rewardedAd.Show();
        }
    }
}
