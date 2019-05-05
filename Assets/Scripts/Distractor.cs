using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractor : MonoBehaviour
{
    public GameObject disRing;

    public const float speed = .01f;

    public const float minDistanceToPoint = .5f;

    public const float minMovement = 3;

    public Vector3[] triangle = new Vector3[3];

    private bool[] movementPhase = { true, false, false };

    public const float rotateSpeed = 200;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        disRing.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);

        //disRing.transform.LookAt(new Vector3(0, 1, 0));

        if (movementPhase[0])
        {
            MoveToPoint(triangle[1]);
            if (Vector3.Distance(transform.position, triangle[1]) < minDistanceToPoint )
            {
                movementPhase[0] = false;
                movementPhase[1] = true;
            }
        }
        else if (movementPhase[1])
        {
            MoveToPoint(triangle[2]);
            if (Vector3.Distance(transform.position, triangle[2]) < minDistanceToPoint)
            {
                movementPhase[1] = false;
                movementPhase[2] = true;
            }
        }
        else if (movementPhase[2])
        {
            MoveToPoint(triangle[0]);
            if (Vector3.Distance(transform.position, triangle[0]) < minDistanceToPoint)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveToPoint(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        transform.Translate(direction * speed);
    }
}
