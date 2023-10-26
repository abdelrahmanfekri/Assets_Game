using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstecleScript : MonoBehaviour
{
    public GameObject obstaclePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createObstacle(Vector3 position)
    {
        Instantiate(obstaclePrefab, position, obstaclePrefab.transform.rotation);
    }
}
