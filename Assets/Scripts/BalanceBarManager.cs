using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBarManager : MonoBehaviour
{
    private Rigidbody rBody;
    private BoxCollider collin;
    private BalanceManager balanceManager;
    private Vector3 resetLocation;

    void Start()
    {
        GameManager.Instance.BalanceBarManager = this;
        rBody = GetComponent<Rigidbody>();
        collin = GetComponent<BoxCollider>();
        balanceManager = GameManager.Instance.BalanceManager;
        resetLocation = transform.localPosition;
    }

    void Update()
    {

        if(GameManager.Instance.currentState == GameManager.GameState.playing)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, balanceManager.targetBalance), 1);
        }
        else if (GameManager.Instance.currentState == GameManager.GameState.death)
        {
            rBody.isKinematic = false;
            rBody.useGravity = true;
            collin.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            Debug.Log("Hit an obstacle!");
            balanceManager.Impact();
        }
    }

    public void Undie()
    {
        transform.localPosition = resetLocation;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        collin.isTrigger = true;
        rBody.useGravity = false;
        rBody.isKinematic = true;
    }
}
