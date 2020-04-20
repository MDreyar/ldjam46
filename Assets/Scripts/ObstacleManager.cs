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
            if (GetComponent<AudioSource>().isPlaying == false) GetComponent<AudioSource>().Play();
            if (gameObject.name.Contains("Balloon"))
            {
                transform.Find("Icosphere").GetComponent<MeshRenderer>().enabled = false;
                if (GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();
                if (GetComponentInChildren<ParticleSystem>().transform.parent != null) GetComponentInChildren<ParticleSystem>().transform.parent = gameObject.transform.parent;
                GameManager.Instance.Score++;
            }
            else if (gameObject.name.Contains("Zazo"))
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(0, -3, 0, ForceMode.VelocityChange);
                if(GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();
                if (GetComponentInChildren<ParticleSystem>().transform.parent != null) GetComponentInChildren<ParticleSystem>().transform.parent = gameObject.transform.parent;
            }
        }
        else if (other.transform.tag == "Delete")
        {
            Debug.Log("DESU!");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
