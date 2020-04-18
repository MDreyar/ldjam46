using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxRollAngleLeft;
    public int maxRollAngleRight;

    public float rollValue;

    private BalanceManager balanceManager;
    
    void Start()
    {
        balanceManager = GameManager.Instance.BalanceManager;
        GameManager.Instance.PlayerManager = this;
    }
    
    void Update()
    {
        rollValue = balanceManager.currentBalance;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Lerp(maxRollAngleLeft, maxRollAngleRight, Mathf.InverseLerp(-50, 50, rollValue)));
    }
}

