using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
       
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
          

            GameManager.distract();
            RaycastableObject raycastObject = hitInfo.collider.gameObject.GetComponent<RaycastableObject>();

            if(raycastObject != null)
            {
                GameManager.distract();
            }
        }
    }
}
