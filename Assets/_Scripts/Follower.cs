using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;

    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="DestoryTrigger")
        {
            Debug.Log(gameObject.transform.parent.name);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
