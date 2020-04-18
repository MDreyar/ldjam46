using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBarManager : MonoBehaviour
{
    private Rigidbody rBody;
    private BoxCollider collin;

    void Start()
    {
        GameManager.Instance.BalanceBarManager = this;
        rBody = GetComponent<Rigidbody>();
        collin = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.death)
        {
            rBody.isKinematic = false;
            rBody.useGravity = true;
            collin.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Environment")
        {
            GameManager.Instance.currentState = GameManager.GameState.death;
            
        }
    }

    public void Undie()
    {
        transform.localPosition = new Vector3(0, 1, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        collin.isTrigger = true;
        rBody.useGravity = false;
        rBody.isKinematic = true;
    }
}
