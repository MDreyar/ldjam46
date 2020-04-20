using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BalanceManager BalanceManager { get; private set; }
    public PlayerManager PlayerManager { get; set; }
    public WorldManager WorldManager { get; set; }
    public BalanceBarManager BalanceBarManager { get; set; }
    public ObstacleSpawner ObstacleSpawner { get; set; }

    
    public int Score { get; set; }
    [Header("Difficulty Increaser Options")]
    public int increaseDificultyLimit = 50;
    private int nextDificultyincreaselimit = 0;
    public float pushoveDifficultyIncreaser = 0f;

    public delegate void GameStateChanged(GameState state);
    public event GameStateChanged OnGameStateChanged;

    [Header("Game UI")]
    public GameObject TitleScreen;
    public GameObject InGameUI;
    public TextMeshProUGUI ScoreBoard;
    public GameObject GameOver;
    public TextMeshProUGUI EndScore;

    private GameState pCurrentState;

    public GameState currentState {
        get
        {
            return pCurrentState;
        }
        set
        {
            pCurrentState = value;
            if (OnGameStateChanged != null) OnGameStateChanged(value);
        }}

    public Text debugText;

    private Quaternion defaultCameraPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        BalanceManager = gameObject.GetComponent<BalanceManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.pregame;
        defaultCameraPosition = Camera.main.transform.rotation;
        Score = 0;
        nextDificultyincreaselimit = increaseDificultyLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.pregame)
        {
            //if (debugText != null) debugText.text = "Click to start!";
            WorldManager.isSpinning = false;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {
                currentState = GameState.playing;
                if (debugText != null) debugText.text = "";
                WorldManager.isSpinning = true;
            }
        }
        else if (currentState == GameState.playing)
        {
            TitleScreen.SetActive(false);
            WorldManager.isSpinning = true;
            InGameUI.SetActive(true);
            ScoreBoard.SetText("Score: " + Score.ToString());

            if (Mathf.Abs(BalanceManager.currentBalance) >= BalanceManager.balanceMax)
            {
                InGameUI.SetActive(false);
                Debug.Log("You lost!");
                currentState = GameState.death;
            }
        }
        else if (currentState == GameState.death)
        {
            //if (debugText != null)  debugText.text = "You died!\nPress space to reset.";
            GameOver.SetActive(true);
            EndScore.SetText("score: " + Score);
            Camera.main.transform.LookAt(PlayerManager.transform.GetChild(0));
            WorldManager.isSpinning = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameOver.SetActive(false);
                TitleScreen.SetActive(true);
                ResetGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Game Quit");
                Application.Quit();
            }
        }
    }

    private void ResetGame()
    {
        currentState = GameState.pregame;
        BalanceManager.Undie();
        PlayerManager.Undie();
        BalanceBarManager.Undie();
        ObstacleSpawner.Cleanup();
        ObstacleSpawner.difficultyModifier = 1;

        Score = 0;
        Camera.main.transform.rotation = defaultCameraPosition;
    }

    public void increaseDifficulty()
    {
        if (Score >= nextDificultyincreaselimit)
        {
            ObstacleSpawner.difficultyModifier++;
            //dit zou er voor moeten zorgen dat de pushoverammount niet hoger gaat dan 40 maar dat is niet zo en ik weet niet waarom -iggy
            if (BalanceManager.pushoverAmount <= 40)
            {
                BalanceManager.pushoverAmount += pushoveDifficultyIncreaser;
            }
            nextDificultyincreaselimit += increaseDificultyLimit;
        }
    }

    public enum GameState
    {
        pregame, playing, death
    }
}
