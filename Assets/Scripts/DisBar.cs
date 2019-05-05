using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.localPosition;
        transform.localPosition = new Vector3(-2170f + GameManager.distractionPercentage * 2170f, position.y, position.z );
    }
}
