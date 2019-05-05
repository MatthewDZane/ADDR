using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public const float timeToDisplayPotentialPoints = 1f;

    public const string label = "Score: ";
    private string points = "";
    private string tempPoints = "";

    public UnityEngine.UI.Text textObject;

    public static float potentialTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        points = "" + GameManager.points;
        tempPoints = "" + GameManager.tempPoints;


        if (potentialTimer > 0)
        {
            potentialTimer -= Time.deltaTime;
            if (GameManager.tempPoints < 0)
            {
                textObject.text = label + points + " " + tempPoints;
            }
            else
            {
                textObject.text = label + points + " +" + tempPoints;
            }

        }
        else
        {
            potentialTimer = 0;
            textObject.text = label + points;
        }


    }
    public static void resetPotentialTimer()
    {
        potentialTimer = 3f;
    }

    private IEnumerator RemovePotentialPoints()
    {
        yield return new WaitForSeconds(timeToDisplayPotentialPoints);
        textObject.text = label + points;
    }

    
}
