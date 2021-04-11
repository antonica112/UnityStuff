using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject endGameScreen;

    public TextMeshProUGUI scoreText;

    public void EndGame(int score)
    {
        //AdsManager.Instance.DisplayInterstitial();

        endGameScreen.SetActive(true);

        if (scoreText != null)
            scoreText.text = $"Score: {score.ToString()}";
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(0);

        SoundManager.Instance.PlayMenuMusic();
    }

    public void RetryGame()
    {
        GameController.SetGameStarted(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
