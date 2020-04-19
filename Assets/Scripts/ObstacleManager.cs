using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Delete")
        {
            Debug.Log("DESU!");
            Destroy(gameObject);
        }
    }
}
