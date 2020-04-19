using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxRollAngleLeft;
    public int maxRollAngleRight;

    public float rollValue;

    public float panicMoment = 25;

    private BalanceManager balanceManager;
    private Rigidbody rBody;
    private Animator animator;
    private Vector3 spawnLocation;

    void Start()
    {
        balanceManager = GameManager.Instance.BalanceManager;
        GameManager.Instance.PlayerManager = this;
        rBody = gameObject.GetComponentInChildren<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        spawnLocation = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.playing)
        {
            rollValue = balanceManager.currentBalance;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Lerp(maxRollAngleLeft, maxRollAngleRight, Mathf.InverseLerp(-50, 50, rollValue)));
        }
        else if (GameManager.Instance.currentState == GameManager.GameState.death)
        {
            if (!rBody.useGravity == true)
            {
                rBody.useGravity = true;
                rBody.isKinematic = false;
                rBody.AddRelativeForce(new Vector3(0, 3, 0), ForceMode.VelocityChange);
            }
        }

        if (Mathf.Abs(balanceManager.currentBalance) > panicMoment)
        {
            animator.SetBool("isPanicing", true);
        }
        else
        {
            animator.SetBool("isPanicing", false);
        }
    }

    public void Undie()
    {
        transform.position = spawnLocation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rBody.useGravity = false;
        rBody.isKinematic = true;
    }
}

