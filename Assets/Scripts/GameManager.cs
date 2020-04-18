using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BalanceManager BalanceManager { get; private set; }
    public PlayerManager PlayerManager { get; set; }

    public GameState currentState = GameState.pregame;

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
        defaultCameraPosition = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.pregame)
        {
            debugText.text = "Click to start!";
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                currentState = GameState.playing;
                debugText.text = "";
            }
        }
        else if (currentState == GameState.playing)
        {
            if (Mathf.Abs(BalanceManager.currentBalance) >= BalanceManager.balanceMax)
            {
                Debug.Log("You lost!");
                currentState = GameState.death;
            }
        }
        else if (currentState == GameState.death)
        {
            debugText.text = "You died!\nPress space to reset.";
            Camera.main.transform.LookAt(PlayerManager.transform.GetChild(0));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
        }
    }

    private void ResetGame()
    {
        currentState = GameState.pregame;
        BalanceManager.Undie();
        PlayerManager.Undie();
        Camera.main.transform.rotation = defaultCameraPosition;

    }

    public enum GameState
    {
        pregame, playing, death
    }
}
