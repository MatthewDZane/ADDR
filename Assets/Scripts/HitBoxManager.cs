using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCubeManager : MonoBehaviour
{
    private int pointValue;

    public GameObject preCube;
    public GameObject postCube;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool isHit()
    {
        //if the precube was hit already
        if (preCube == null)
        {
            // first set postcube.precube to null
            postCube.GetComponent<HitCubeManager>().preCube = null;

            // then destroy this cube
            Destroy(this);
        }
        return false;
    }


}
