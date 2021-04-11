using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float distanceTraveled = 0f;

    public GameObject[] GameCharacters;

    public GameObject GameCanvasPrefab;

    public static int Score { get; private set; }

    public static bool GameStarted { get; private set; }

    private static TextMeshProUGUI scoreText;

    public static bool GameEnded { get; private set; }

    private static GameObject gameCanvas = null;

    public static GameObject currentCharacter;

    public static void IncreaseScore(int amount)
    {
        Score += amount;

        if(scoreText != null)
        {
            scoreText.text = Score.ToString();
        }
    }

    public static void SetEndGame(bool ended)
    {
        GameEnded = ended;

        if (ended)
        {
            GameMenuManager menuScript = FindObjectOfType<GameMenuManager>();

            int highScore = 0;
            if (PlayerPrefs.HasKey("HighScore"))
                highScore = PlayerPrefs.GetInt("HighScore");

            if(Score > highScore)
            {
                // New high score
                PlayerPrefs.SetInt("HighScore", Score);
            }

            if (menuScript != null)
                menuScript.EndGame(Score);
        }
    }

    public static void SetGameStarted(bool started)
    {
        GameStarted = started;
    }

    // Start is called before the first frame update
    void Start()
    {
        int character = 0;
        if (PlayerPrefs.HasKey("SelectedCharacter"))
            character = PlayerPrefs.GetInt("SelectedCharacter");

        currentCharacter = Instantiate(GameCharacters[character], new Vector3(0f, 4f, 0f), Quaternion.identity) ;

        GameEnded = false;

        // Instantiate game canvas, where we'll display player's score
        if (GameCanvasPrefab != null)
        {
            gameCanvas = Instantiate(GameCanvasPrefab);
            //scoreText = gameCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Score = 0;
            //scoreText.text = Score.ToString();
        }
    }
}
