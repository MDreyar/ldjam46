using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hit the player!");
            if (gameObject.name.Contains("Balloon"))
            {
                transform.Find("Icosphere").GetComponent<MeshRenderer>().enabled = false;
                GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        else if (other.transform.tag == "Delete")
        {
            Debug.Log("DESU!");
            Destroy(gameObject);
        }
    }
}
