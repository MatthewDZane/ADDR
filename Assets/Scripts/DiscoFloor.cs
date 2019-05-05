using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    public GameObject[] tiles = new GameObject[64];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeColor());
    }

    // Update is called once per frame
    void Update()
    {


            
    }

    public IEnumerator changeColor()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.92f / 4);
            foreach (GameObject tile in tiles)
            {

                Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                tile.GetComponent<Renderer>().material.color = newColor;
                tile.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
            }

        }
    }
}
