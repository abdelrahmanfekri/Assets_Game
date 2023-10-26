using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardThePlayer : MonoBehaviour
{
    int Speed = 20;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }
    void moveForward()
    {
        transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * Speed);
    }
}
