using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject LoadingScreen;

    public TextMeshProUGUI versionText;

    public Slider slider;

    public GameObject squarePlayButton;
    public GameObject squareUnlockButton;
    public ButtonsScale squareButtonScript;

    public GameObject trianglePlayButton;
    public GameObject triangleUnlockButton;
    public ButtonsScale triangleButtonScript;

    public Image SoundButtonImage;
    public Image MusicButtonImage;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public GameObject GameMenuPanel;
    public GameObject CreditsMenuPanel;

    public TextMeshProUGUI highestScore;

    void Start()
    {
        versionText.text = $"Version: {Application.version}";

        ResetMenu();
    }

    public void ShowCreditsButton()
    {
        if (CreditsMenuPanel.activeSelf)
        {
            CreditsMenuPanel.SetActive(false);
            GameMenuPanel.SetActive(true);
        }
        else
        {
            CreditsMenuPanel.SetActive(true);
            GameMenuPanel.SetActive(false);
        }
    }

    public void ToggleSound()
    {
        if(SoundManager.Instance.SoundOn)
        {
            SoundManager.Instance.ToggleGameSounds();
            SoundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            SoundManager.Instance.ToggleGameSounds();
            SoundButtonImage.sprite = soundOnSprite;
        }
    }

    public void ToggleMusic()
    {
        if (SoundManager.Instance.MusicOn)
        {
            SoundManager.Instance.ToggleGameMusic();
            MusicButtonImage.sprite = musicOffSprite;
        }
        else
        {
            SoundManager.Instance.ToggleGameMusic();
            MusicButtonImage.sprite = musicOnSprite;
        }
    }

    void DisableCharacterButtons()
    {
        squarePlayButton.SetActive(false);
        squareUnlockButton.SetActive(false);

        trianglePlayButton.SetActive(false);
        triangleUnlockButton.SetActive(false);
    }

    public void UnlockCharacters()
    {
        PlayerPrefs.SetInt("TriangleCharacter", 1);
        PlayerPrefs.SetInt("SquareCharacter", 1);

        ResetMenu();
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey("SquareCharacter");
        PlayerPrefs.DeleteKey("TriangleCharacter");
        PlayerPrefs.DeleteKey("HighestScore");

        ResetMenu();
    }

    public void ResetMenu()
    {
        int squareUnlocked = 0;
        int triangleUnlocked = 0;
        int highScore = 0;

        if (PlayerPrefs.HasKey("SquareCharacter"))
            squareUnlocked = PlayerPrefs.GetInt("SquareCharacter");

        if (PlayerPrefs.HasKey("TriangleCharacter"))
            triangleUnlocked = PlayerPrefs.GetInt("TriangleCharacter");

        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetInt("HighScore");

        DisableCharacterButtons();

        if (squareUnlocked == 1)
            squarePlayButton.SetActive(true);
        else squareUnlockButton.SetActive(true);

        if (triangleUnlocked == 1)
            trianglePlayButton.SetActive(true);
        else triangleUnlockButton.SetActive(true);

        if (SoundManager.Instance.MusicOn)
            MusicButtonImage.sprite = musicOnSprite;
        else MusicButtonImage.sprite = musicOffSprite;

        if (SoundManager.Instance.SoundOn)
            SoundButtonImage.sprite = soundOnSprite;
        else SoundButtonImage.sprite = soundOffSprite;

        if (highestScore != null)
            highestScore.text = highScore.ToString();
    }

    public void PlayButton(int character)
    {
        PlayerPrefs.SetInt("SelectedCharacter", character);

        LoadLevel(1);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}