using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedOrb"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("GreenOrb"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BlueOrb"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Obst"))
        {
            Destroy(other.gameObject);
        }
    }
}
