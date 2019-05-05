using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    public const float maxScale = 0.2f;
    public const float maxGrowth = 0.1f;
    //public const float growth = .0005f;
    //public const float opacity = 0.005f;
    private int pointValue = 1;

    public bool canBeHit = false;

    public const float beatSec = 1.92f / 2f;
    public const float beatTime = 1.92f;

    public void stackPoints(int points) { GameManager.tempPoints += points;  }

    public GameObject preCube = null;
    public GameObject postCube = null;

    public GameObject spawner = null;

    static AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void playSound()
    {
        Debug.Log("play sound");
        source.Play(0);
        //yield return new WaitForSeconds(0);
    }   

    // Update is called once per frame
    void Update()
    {
        if (!canBeHit)
        {
            Move();
            //Grow(Time.deltaTime);
            //Opaquify(Time.deltaTime);
        }
    }

    IEnumerator Disintegrate()
    {
        yield return new WaitForSeconds(beatSec);
        //maybe add something else for when yarn in destroyed
        Destroy(gameObject);
    }
    private void Move( )
    {
        if ( transform.position.z > spawner.transform.position.z)
        {
            float movement = Time.deltaTime * 10 / 4;
            transform.Translate(new Vector3(0, 0, -movement));
            
        }
        else
        {
            canBeHit = true;
            StartCoroutine(Disintegrate());
        }
        
    }
    private void Grow(float deltaTime)
    {
        if (transform.localScale.x < maxScale)
        {
            float growth = deltaTime * maxGrowth / beatTime * 4;
            transform.localScale = new Vector3(growth + transform.localScale.x, growth + transform.localScale.y, growth + transform.localScale.z);

        }
        if (transform.localScale.x > maxScale)
        {
            
            transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        }
    }

    private void Opaquify(float deltaTime)
    {
        Color color = this.GetComponent<MeshRenderer>().sharedMaterial.color;
        if (color.a < 1)
        {
            float opacity = deltaTime / beatTime *4;
            this.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(color.r, color.g, color.b, Mathf.Min(1, color.a + opacity));
        }
        if (this.GetComponent<MeshRenderer>().sharedMaterial.color.a >= 1)
        {
            this.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(color.r, color.g, color.b, 1);
        }
    }

    public bool IsHit()
    {
        Debug.Log("over here");
        //if the precube was hit already
        if (preCube == null)
        {
            Debug.Log("in here");
            playSound();
            //StartCoroutine(playSound());
            
            if (postCube != null)
            {
                // first set postcube.precube to null
                postCube.GetComponent<HitBoxManager>().preCube = null;
                // add points from this box to the next
                postCube.GetComponent<HitBoxManager>().stackPoints(pointValue);
                Debug.Log(pointValue);
                PointsScript.resetPotentialTimer();
            }
            else
            {
                // add points
                GameManager.AddPoints(pointValue);
            }

            // then destroy this cube
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

}
