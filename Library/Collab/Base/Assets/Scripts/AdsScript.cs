using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class AdsScript : MonoBehaviour {

	public static AdsScript Instance{ set; get; }

	private RewardBasedVideoAd rewardBasedVideo;

	public void Start()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);

		#if UNITY_ANDROID
			string appId = "ca-app-pub-2631128054673594~6288751344";
		#else
			string appId = "unexpected_platform";
		#endif

		MobileAds.Initialize(appId);
	
		AdsScript.Instance.rewardBasedVideo = RewardBasedVideoAd.Instance;

		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;

		AdsScript.Instance.RequestRewardedVideo();


	}

	private void RequestRewardedVideo()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-2631128054673594/5518595157";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		AdRequest request = new AdRequest.Builder ().Build ();

		AdsScript.Instance.rewardBasedVideo.LoadAd (request, adUnitId);
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		GameScript.Instance.rewardPoints = 50;
	}

	public void GameOver()
	{
		if (AdsScript.Instance.rewardBasedVideo.IsLoaded()) {
			AdsScript.Instance.rewardBasedVideo.Show();
		}
	}
}
