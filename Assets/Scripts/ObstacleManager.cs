using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Score")]
    public int BalloonScoreIncrease = 10;
    public int ZazoScoreDecrease = 1;

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
                GameManager.Instance.Score += BalloonScoreIncrease;
                GameManager.Instance.increaseDifficulty();
            }
            else if (gameObject.name.Contains("Zazo"))
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(0, -3, 0, ForceMode.VelocityChange);
                if(GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();
                if (GetComponentInChildren<ParticleSystem>().transform.parent != null) GetComponentInChildren<ParticleSystem>().transform.parent = gameObject.transform.parent;
                GameManager.Instance.Score -= ZazoScoreDecrease;
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
        Destroy(gameObject, 3f);
    }
}
