
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
    public float currentBalance { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.playing)
            return;

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
        falloverMultiplyer += 0.05f * Time.deltaTime;

        testSlider.value = currentBalance;
    }

    public void Reset()
    {
        currentBalance = 0;
        falloverMultiplyer = 1;
    }

    private float DontGoOverMax(float valueToClamp)
    {
        float output = Mathf.Clamp(valueToClamp, -balanceMax - currentBalance, balanceMax - currentBalance);
        Debug.Assert(Mathf.Abs(output) < balanceMax);
        return output;
    }
}
