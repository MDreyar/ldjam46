using System.Collections;
using System.Collections.Generic;
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

    public delegate void GameStateChanged(GameState state);
    public event GameStateChanged OnGameStateChanged;

    public GameObject TitleScreen;
    public GameObject GameOver;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.pregame)
        {
            //if (debugText != null) debugText.text = "Click to start!";
            WorldManager.isSpinning = false;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
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
            if (Mathf.Abs(BalanceManager.currentBalance) >= BalanceManager.balanceMax)
            {
                Debug.Log("You lost!");
                currentState = GameState.death;
            }
        }
        else if (currentState == GameState.death)
        {
            //if (debugText != null)  debugText.text = "You died!\nPress space to reset.";
            GameOver.SetActive(true);
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
        Camera.main.transform.rotation = defaultCameraPosition;
    }

    public enum GameState
    {
        pregame, playing, death
    }
}
