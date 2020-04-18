
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BalanceManager : MonoBehaviour
{
    public Slider testSlider;
    public float balanceMax = 50;
    public float minRandomBalanceAjustmentAmount = 3f;
    public float MaxRandomBalanceAjustmentAmount = 15f;
    public float falloverMultiplyer = 1;

    [SerializeField]
    private float currentBalance = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Change balance based on input
        if (Input.GetMouseButtonDown(0))
        {
            currentBalance += DontGoOverMax(Random.Range(minRandomBalanceAjustmentAmount, MaxRandomBalanceAjustmentAmount));
        }else if (Input.GetMouseButtonDown(1))
        {
            currentBalance += DontGoOverMax(Random.Range(-MaxRandomBalanceAjustmentAmount, -minRandomBalanceAjustmentAmount));
        }

        // Natural fallover
        currentBalance += DontGoOverMax(falloverMultiplyer * currentBalance * Time.deltaTime);


        // Make it moar difficult!
        //falloverMultiplyer += 0.05f * Time.deltaTime;

        testSlider.value = currentBalance;
    }

    private float DontGoOverMax(float valueToClamp)
    {
        return Mathf.Clamp(valueToClamp, -balanceMax - currentBalance, balanceMax - currentBalance);
    }
}
