using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject hitCube;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnSingle()
    {

    }

    public void spawnCombo(GameObject preCombo)
    {
        Instantiate(hitCube, this.transform);
    }
}
