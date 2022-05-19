using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string _gameId = "4695320";
#elif UNITY_ANDROID
        private string _gameId = "4695321";
#endif


    public static AdsManager instance;

    [HideInInspector]
    public bool rewarded = false;

    private GameManager gameManager;

    public delegate void RewardDelegate();

    public RewardDelegate rewardMethod;

    private int countForAds = 0;

    void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy (gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        InitializeAds();
    }

    public void InitializeAds()
    {
        gameManager = GameManager.instance;
        Advertisement.Initialize (_gameId);
        Advertisement.AddListener(this);
        ShowBanner();
    }

    public void ShowBanner()
    {
#if UNITY_IOS
        if (Advertisement.IsReady("Banner_IOS"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_IOS");
        }
        else
        {
            StartCoroutine(TryShowBanner());
        }
#elif UNITY_ANDROID
            if(Advertisement.IsReady("Banner_Android")){
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                Advertisement.Banner.Show("Banner_Android");
            }else{
                StartCoroutine(TryShowBanner());
            }
#endif
    }

    public void ShowInterstitial()
    {
#if UNITY_IOS
        if (Advertisement.IsReady("Interstitial_IOS"))
        {
            Advertisement.Show("Interstitial_IOS");
        }
        else
        {
            Time.timeScale = 1;
        }
#elif UNITY_ANDROID
            if(Advertisement.IsReady("Interstitial_Android")){
                Advertisement.Show("Interstitial_Android");
            }else{
                Time.timeScale = 1;
            }
#endif
    }

    public void ShowRewarded(RewardDelegate method)
    {
        rewardMethod = method;


#if UNITY_IOS
        if (Advertisement.IsReady("Rewarded_IOS"))
        {
            Advertisement.Show("Rewarded_IOS");
        }
        else
        {
            Time.timeScale = 1;
        }
#elif UNITY_ANDROID
            if(Advertisement.IsReady("Rewarded_Android")){
                Advertisement.Show("Rewarded_Android");
            }else{
                Time.timeScale = 1;
            }
#endif
    }

    public bool ReadyRewarded()
    {
        if (
            Advertisement.IsReady("Rewarded_IOS") ||
            Advertisement.IsReady("Rewarded_Android")
        )
        {
            return true;
        }
        return false;
    }

    IEnumerator TryShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBanner();
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementID)
    {
    }

    public void OnUnityAdsDidError(string placementID)
    {
        Time.timeScale = 1;
    }

    public void OnUnityAdsDidStart(string placementID)
    {
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (
            (
            placementID == "Rewarded_IOS" || placementID == "Rewarded_Android"
            ) &&
            showResult == ShowResult.Finished
        )
        {
            rewardMethod();
            rewardMethod = null;
        }
        else if (
            (
            placementID == "Interstitial_IOS" ||
            placementID == "Interstitial_Android"
            )
        )
        {
            Time.timeScale = 1;
        }
    }

    public void countAds()
    {
        countForAds++;
        if (countForAds >= 4)
        {
            countForAds = 0;
            Time.timeScale = 0;
            ShowInterstitial();
        }
    }

    // bool CheckConnection()
    // {
    //     string html = string.Empty;
    //     HttpWebRequest req =
    //         (HttpWebRequest) WebRequest.Create("http://google.com");
    //     using (HttpWebResponse resp = (HttpWebResponse) req.GetResponse())
    //     {
    //         bool isSuccess =
    //             (int) resp.StatusCode < 299 && (int) resp.StatusCode >= 200;
    //         if (isSuccess)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}
