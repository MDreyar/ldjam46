using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFloat : MonoBehaviour
{
    public float floatSpeed = 1;
    public float shrinkSpeed = 0.1f;

   // Update is called once per frame
    void Update()
    {
        transform.Translate(0, floatSpeed * Time.deltaTime, 0);
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, shrinkSpeed);
        if(transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
}
