using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Score")]
    public int BalloonScoreIncrease = 5;
    public int ZazoScoreDecrease = 1;
    [Header("Score popups")]
    public GameObject plusFive;
    public GameObject minusOne;

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
                GameManager.Instance.Score += BalloonScoreIncrease;
                GameManager.Instance.increaseDifficulty();
                Instantiate(plusFive, transform.position, transform.rotation);
            }
            else if (gameObject.name.Contains("Zazo"))
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(0, -3, 0, ForceMode.VelocityChange);
                if(GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();
                GameManager.Instance.Score -= ZazoScoreDecrease;
                Instantiate(minusOne, transform.position, transform.rotation);
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
