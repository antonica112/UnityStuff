using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour//, IUnityAdsListener
{
    /*public int DesiredCharacter { get; private set; }

    //public string gameId = "c3a751af-5622-4c7b-9932-107152ee324a";
    public string gameId = "3300618";
    public string bannerId = "bannerShow";
    public string interstitialId = "interstitialShow";
    public string rewardedId = "rewardedVideo";
    public bool testMode = true;

    public static AdsManager Instance = null;

    public int displayWait = 0;

    public void DisplayInterstitial()
    {
        displayWait++;

        if (displayWait >= 3)
        {
            Advertisement.Show();
            displayWait = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedId)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                Debug.Log("Desired character is " + DesiredCharacter);

                // Reward the user for watching the ad to completion.
                if (DesiredCharacter == 1)
                {
                    PlayerPrefs.SetInt("SquareCharacter", 1);

                    MenuManager menuScript = FindObjectOfType<MenuManager>();
                    if (menuScript != null)
                        menuScript.ResetMenu();
                }
                else if (DesiredCharacter == 2)
                {
                    PlayerPrefs.SetInt("TriangleCharacter", 1);
                    MenuManager menuScript = FindObjectOfType<MenuManager>();
                    if (menuScript != null)
                        menuScript.ResetMenu();
                }
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == rewardedId)
        {
            Advertisement.Show(rewardedId);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void ShowRewardedVideo(int desiredShape)
    {
        DesiredCharacter = desiredShape;
        Advertisement.Show(rewardedId);
    }*/
}
