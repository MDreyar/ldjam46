using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxRollAngleLeft;
    public int maxRollAngleRight;

    public float rollValue;

    public bool hasFallen;

    public BalanceManager balanceManager;
    
    void Start()
    {
        hasFallen = false;
    }
    
    void Update()
    {
        rollValue = balanceManager.currentBalance;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Lerp(maxRollAngleLeft, maxRollAngleRight, Mathf.InverseLerp(-50, 50, rollValue)));

        if (Mathf.Abs(rollValue) == Mathf.Abs(balanceManager.balanceMax))
        {
            hasFallen = true;
            Debug.Log("You didn't keep it alive!");
        }
    }
}

