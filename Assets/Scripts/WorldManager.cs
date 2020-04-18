using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public bool isSpinning;
    public float rotationSpeed;

    void Start()
    {
        GameManager.Instance.WorldManager = this;
    }

    void Update()
    {
        if (isSpinning)
        {
            Debug.Log("Is Spinning");
            transform.Rotate(Vector3.left * (rotationSpeed * Time.deltaTime));
        } 

    }
}
