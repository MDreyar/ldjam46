﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.ComponentModel;

public class BalanceManager : MonoBehaviour
{
    [Header("Debug slider for debug reasons")]
    public Slider testSliderCurrent;
    public Slider testSliderTarget;

    [Header("Balancing option")]
    [Tooltip("How far can you go before you die")]
    public float balanceMax = 50;
    public float minRandomBalanceAjustmentAmount = 3f;
    public float MaxRandomBalanceAjustmentAmount = 15f;
    [Header("Leaning options")]
    [Tooltip("This multiplyer is attached to the natural leaning, based on how far the player is already leaning")]
    public float falloverMultiplyer = 1;
    [Tooltip("How quickly should the player move towards the target. A lower number is quicker!")]
    public float falloverSpeed = 1;
    [Tooltip("How far should the player be pushed when it hits something")]
    public float pushoverAmount = 15;

    public float currentBalance { get; private set; } = 0;
    public float targetBalance { get; set; } = 0;
    private float smoothMovementVelocity = 0;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.playing)
            return;

        // Change balance based on input
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetBalance += DontGoOverMax(Random.Range(minRandomBalanceAjustmentAmount, MaxRandomBalanceAjustmentAmount));
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetBalance += DontGoOverMax(Random.Range(-MaxRandomBalanceAjustmentAmount, -minRandomBalanceAjustmentAmount));
        }

        // Natural fallover
        targetBalance += DontGoOverMax(falloverMultiplyer * targetBalance * Time.deltaTime);

        // Make it moar difficult!
        //falloverMultiplyer += 0.05f * Time.deltaTime;

        currentBalance = Mathf.SmoothDamp(currentBalance, targetBalance, ref smoothMovementVelocity, falloverSpeed);

        if (testSliderCurrent != null) testSliderCurrent.value = currentBalance;
        if (testSliderTarget != null)  testSliderTarget.value = targetBalance;
    }

    public void Undie()
    {
        currentBalance = 0;
        targetBalance = 0;
        falloverMultiplyer = 1;
    }

    public void Impact()
    {
        if (targetBalance >= 0)
        {
            //currentBalance += pushoverAmount;
            targetBalance += pushoverAmount;
        }
        else if (targetBalance < 0)
        {
            //currentBalance += -pushoverAmount;
            targetBalance += -pushoverAmount;
        }
    }

    private float DontGoOverMax(float valueToClamp)
    {
        float output = Mathf.Clamp(valueToClamp, (-balanceMax - 10) - targetBalance, (balanceMax + 10) - targetBalance);
        Debug.Assert(Mathf.Abs(output) < balanceMax);
        return output;
    }
}
