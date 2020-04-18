using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BalanceManager BalanceManager { get; private set; }
    public PlayerManager PlayerManager { get; set; }

    public bool isPlaying = false;

    public Text debugText;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            debugText.text = "Click to start!";
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                isPlaying = true;
                debugText.text = "";
            }
        }
        else
        {
            if (Mathf.Abs(BalanceManager.currentBalance) >= BalanceManager.balanceMax)
            {
                Debug.Log("You lost!");
                ResetGame();
            }
        }
    }

    private void ResetGame()
    {
        isPlaying = false;
        BalanceManager.Reset();
    }
}
