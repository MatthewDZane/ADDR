using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public GameObject hitBoxes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameObject spawnBall(GameObject preCube )
    {
        
            GameObject newBox = Instantiate(hitBoxes, this.transform);
            newBox.transform.Translate(0, 0, transform.position.z + 10);
            //newBox.transform.localScale = new Vector3(.1f, .1f, .1f);
            //Color color = newBox.GetComponent<MeshRenderer>().sharedMaterial.color;
            //Debug.Log(color.r);
            //newBox.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(color.r, color.g, color.b, 0);
            
            newBox.GetComponent<HitBoxManager>().preCube = preCube;
            newBox.GetComponent<HitBoxManager>().spawner = this.gameObject;

            return newBox;
    }

    public GameObject spawnDistraction()
    {
        GameObject newDis = Instantiate(hitBoxes, transform);
        Distractor dis = newDis.GetComponent<Distractor>();
        do
        {
            dis.triangle[0] = new Vector3(Random.Range(-5, 5),
                                          Random.Range(1, 5),
                                          Random.Range(-1, 3));
        } while (Vector3.Distance(dis.triangle[0], dis.triangle[1]) < Distractor.minMovement);
        newDis.transform.position = dis.triangle[0];

        do
        {
            dis.triangle[1] = new Vector3(Random.Range(-5, 5),
                                          Random.Range(1, 5),
                                          Random.Range(-1, 3));
        } while (Vector3.Distance(dis.triangle[0], dis.triangle[1]) < Distractor.minMovement);
        
        do
        {
            dis.triangle[2] = new Vector3(Random.Range(-5, 5),
                                          Random.Range(1, 5),
                                          Random.Range(-1, 3));
        } while (Vector3.Distance(dis.triangle[1], dis.triangle[2]) < Distractor.minMovement ||
                 Vector3.Distance(dis.triangle[0], dis.triangle[2]) < Distractor.minMovement);
        return null;
    }
}
