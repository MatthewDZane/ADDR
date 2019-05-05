using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const float distractionIncr = .015f;

    public static int points = 0;
    public static int tempPoints = 0;

    public GameObject[] horizontalSpawners;
    public GameObject[] verticalSpawners;
    public GameObject distractionSpawner;

    public static float distractionPercentage = .1f;

    public const float spawnDelay = 2;

    public static float comboSpawnDelay = 0.1f;

    public const float beatTime = 1.92f /4;

    public const float timeBetweenDistractions = 20;

    public static void distract()
    {
        distractionPercentage += distractionIncr;
        if (distractionPercentage >= 1)
        {
            points += -1;
            tempPoints = -1;
            PointsScript.resetPotentialTimer();
            distractionPercentage = .5f;  
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playSong();
    }

    // Update is called once per frame
    void Update()
    {
        //decrement distractionIncr
        distractionPercentage -= distractionIncr / 4;
        if (distractionPercentage < 0)
        {
            distractionPercentage = 0;
        }
    }


    public static void AddPoints(int newPoints)
    {
        points += newPoints;
        points += tempPoints;
        tempPoints = 0;
    }

    /**
     * Adds new points to a temp value which will hold potential points until a whole combo is finished
     * */
    public static void AddTempPoints(int newPoints)
    {
        tempPoints += newPoints;
    }

    public IEnumerator SpawnDistraction(float delay)
    {
        yield return new WaitForSeconds(beatTime * delay);
        distractionSpawner.GetComponent<SpawnerManager>().spawnDistraction();
    } 

    IEnumerator HorizontalSpawn(int start, int finish, float delay) 
    {
        yield return new WaitForSeconds(beatTime * delay - comboSpawnDelay);
        GameObject preCube = null;
        if ( start > finish)
        {
            //going right to left
            for (int i = start; i >= finish; i-- )
            {
                yield return new WaitForSeconds(comboSpawnDelay); 
                GameObject newCube = horizontalSpawners[i].GetComponent<SpawnerManager>().spawnBall(preCube);
                if (newCube == null)
                {
                    continue;
                }

                if (preCube != null)
                {
                    preCube.GetComponent<HitBoxManager>().postCube = newCube;
                }
                preCube = newCube;

            }
        }
        else
        {
            //going left to right
            for (int i = start; i <= finish; i++)
            {
                yield return new WaitForSeconds(comboSpawnDelay);
                GameObject newCube = horizontalSpawners[i].GetComponent<SpawnerManager>().spawnBall(preCube);
                if (newCube == null)
                {
                    continue;
                }

                if (preCube != null)
                {
                    preCube.GetComponent<HitBoxManager>().postCube = newCube;
                }
                preCube = newCube;
            }
        }
    }

    IEnumerator VerticalSpawn(int start, int finish, float delay)
    {
        yield return new WaitForSeconds(beatTime * delay - comboSpawnDelay);
        GameObject preCube = null;
        if (start > finish)
        {
            //going down to up
            for (int i = start; i >= finish; i--)
            {
                yield return new WaitForSeconds(comboSpawnDelay);
                GameObject newCube = verticalSpawners[i].GetComponent<SpawnerManager>().spawnBall(preCube);
                if (newCube == null)
                {
                    continue;
                }

                if (preCube != null)
                {
                    preCube.GetComponent<HitBoxManager>().postCube = newCube;
                }
                preCube = newCube;

            }
        }
        else
        {
            //going up to down
            for (int i = start; i <= finish; i++)
            {
                yield return new WaitForSeconds(comboSpawnDelay);
                GameObject newCube = verticalSpawners[i].GetComponent<SpawnerManager>().spawnBall(preCube);
                if (newCube == null)
                {
                    continue;
                }

                if (preCube != null)
                {
                    preCube.GetComponent<HitBoxManager>().postCube = newCube;
                }
                preCube = newCube;
            }
            
        }
    }
    IEnumerator MakeDistractions()
    {
        while (true)
        {
            
            StartCoroutine(SpawnDistraction(0f));
            yield return new WaitForSeconds(timeBetweenDistractions);
        }
    }

    public static void levelSwitch(int choice)
    {
        switch (choice)
        {
            case 1:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
//playFantasia();
                break;
            case 2:
                SceneManager.LoadScene("DiscoDome", LoadSceneMode.Additive);
                //playWerq();
                break;
        }
    }

    public void playFantasia()
    {
        AudioManager.setFantasia();
        StartCoroutine(AudioManager.Remote());
        StartCoroutine(HorizontalSpawn(0, 0, 1 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 2 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 3 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 4 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 5 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 5.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 6 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 6.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 9 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 10 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 11 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 12 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 13 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 13.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 14 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 17 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 18 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 19 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 20 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 21 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 21.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 22 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 22.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 25 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 26 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 27 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 28 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 29 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 29.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 30.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(4, 4, 31 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 32 - spawnDelay ));
        StartCoroutine(VerticalSpawn(4, 4, 32 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 33 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 37 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 41 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 43 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 44 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 45 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 49 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 53 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 57 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 59 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 60 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 61 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 63.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 64 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 64.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 65 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 66 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 67 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 68 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 69 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 69.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 70 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 70.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 73 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 74 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 75 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 76 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 77 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 77.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 78 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(1, 1, 81 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 82 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 83 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 84 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 85 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 85.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 86 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 86.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 89 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 4, 90 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 91 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 92 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 93 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 93.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(3, 3, 94.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 95 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 97 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 98 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 99.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 100 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 100.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 101 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 102 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 105 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 106 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 107.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 108 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 108.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 109 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 110 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 113 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 114 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 115.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 116 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 116.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 117 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 118 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 119.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 120.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 121 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 122 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 123.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 124 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 124.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 125 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 129 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 133 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 137 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 140 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 141 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 145 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 145.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 146 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 147 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 148 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 149 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 153 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 153.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 154 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 155 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 156 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 157 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 159 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 160 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 161 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 162 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 163 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 164 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 165 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 166 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 167 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 168 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 169 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 170 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 171 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 172 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 173 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 174 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 175 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 176 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 177 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 178 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 179 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 180 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 181 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 182 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 183 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 184 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 185 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 186 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 187 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 188 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 189 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 190.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 191 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 192.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 193 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 194 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 195 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 196 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 197 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 197.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 198 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 198.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 200 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 201 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 202 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 203 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 204 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 205 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 205.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 206 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 209 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 210 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 211 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 212 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 213 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 213.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 214 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 214.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 216 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 217 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 218 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 219 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 220 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 221 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 221.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 222.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 223 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 224 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 224 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 225 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 226 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 227 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 227.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 228 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 228.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 229 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 230 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 231 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 231.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 232 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 232.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 233 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 234 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 235 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 235.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 236 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 236.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 237 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 238 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 239 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 239.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 240 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 240.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 241 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 242 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 243 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 243.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 244 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 244.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 245 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 246 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 247 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 247.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 248 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 248.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 249 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 250 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 251 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 251.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 252 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 252.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 253 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 253.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 254 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 255 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 255.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 256 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 257 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 257.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 258 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 259 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 260 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 261 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 261.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 262 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 265 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 265.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 266 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 267 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 268 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 269 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 269.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 270 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 273 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 273.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 274 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 275 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 276 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 277 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 277.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 278 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 281 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 281 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 283 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 283.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 284 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 284 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 285 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 285.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 286 - spawnDelay ));

    }

    public void playWerq()
    {
        AudioManager.setWerq();
        StartCoroutine(AudioManager.Remote());
        StartCoroutine(HorizontalSpawn(4, 4, 17 - spawnDelay));
        StartCoroutine(HorizontalSpawn(0, 0, 18 - spawnDelay));
        StartCoroutine(HorizontalSpawn(4, 4, 21 - spawnDelay));
        StartCoroutine(HorizontalSpawn(0, 0, 22 - spawnDelay));
        StartCoroutine(VerticalSpawn(0, 0, 25 - spawnDelay));
        StartCoroutine(VerticalSpawn(2, 2, 26 - spawnDelay));
        StartCoroutine(VerticalSpawn(0, 0, 29 - spawnDelay));
        StartCoroutine(VerticalSpawn(2, 2, 30 - spawnDelay));
        StartCoroutine(HorizontalSpawn(4, 4, 31.5f - spawnDelay));
        StartCoroutine(HorizontalSpawn(0, 0, 32 - spawnDelay));
        StartCoroutine(VerticalSpawn(0, 0, 32.5f - spawnDelay));

        StartCoroutine(VerticalSpawn(2, 2, 33 - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 36.5f - spawnDelay));
        StartCoroutine(HorizontalSpawn(3, 3, 37 - spawnDelay));
        StartCoroutine(HorizontalSpawn(3, 3, 40.5f - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 41 - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 42 - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 43 - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 44 - spawnDelay));
        StartCoroutine(HorizontalSpawn(1, 1, 44.5f - spawnDelay));
        StartCoroutine(HorizontalSpawn(3, 3, 45 - spawnDelay));
        StartCoroutine(HorizontalSpawn(3, 3, 46 - spawnDelay));
        StartCoroutine(HorizontalSpawn(3, 3, 47 - spawnDelay));
        StartCoroutine(HorizontalSpawn(0, 0, 48 - spawnDelay));
        StartCoroutine(HorizontalSpawn(4, 4, 48 - spawnDelay));

        StartCoroutine(VerticalSpawn(0, 0, 52 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 52 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 56 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 56 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 60 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 60 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 63 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 63 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 64 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 64 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 65 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 65.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 66 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 66.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 67.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 68 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 68.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 69.5f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(3, 3, 70 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 70.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 71.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 72 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 72.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 73.2f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(4, 4, 74 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 74.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 75.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 76 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 76.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 77.7f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(2, 2, 78 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 78.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 79.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 80 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 80.5f - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 81 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 81.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 82 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 82.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 83.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 84 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 84.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 85.5f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(2, 2, 86 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 86.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 87.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 88 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 88.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 89.5f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(2, 2, 90 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 90.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 91.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 92 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 92.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 93.5f - spawnDelay ));//
        StartCoroutine(HorizontalSpawn(2, 2, 94 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 94.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 95 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 96 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 97 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 98 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 99 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 100 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 100.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 101 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 102 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 103 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 104 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 104.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 105 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 106 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 107 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 108 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 108.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 109 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 110 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 111 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 112 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 112.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 113 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 114 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 116 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 116.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 117 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 118 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 121 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 122 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 124 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 124.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 125 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 126 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 133 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 137 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 141 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 145 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 147 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 149 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 151 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 153 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 155 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 157 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 159 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 161 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 162 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 163 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 164 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 165 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 166 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 167 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 168 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 169 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 170 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 171 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 172 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 173 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 174 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 175 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 176 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 177 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 177.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 178f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 178.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 179 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 179.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 180 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 181 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 181.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 182 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 182.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 183 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 183.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 184 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 185 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 187 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 188 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 189 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 193 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 193.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 194 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 194.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 195.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 196 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 196.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 197.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 198 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 198.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 199.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 200 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 200.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 201.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 202 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 202.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 203.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 204 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 204.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 205.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 206 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 206.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 207 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 208 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 209.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 210 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 210.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 211.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 212 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 212.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 213.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 214 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 214.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 215.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 216 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 216.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 217.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 218 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 218.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 219.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 220 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 220.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 221.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 222 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 222.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 224 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 224 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 225 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 225 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 228 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 228 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 228.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 228.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 232 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 232 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 232.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 232.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 233 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 233 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 236 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 236 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 236.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 236.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 240 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 240 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 241 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 241.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 242 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 242.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 243 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 244 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 244 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 245 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 245.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 246 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 246.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 247 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 248 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 248 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 249 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 249.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 250 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 250.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 251 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 252 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 252 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 253 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 253.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 254 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 255 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 255.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 256 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 256.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 257 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 258 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 259 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 260 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 260.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 261 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 262 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 263 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 264 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 264.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 265 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 267 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 269 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 271 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 272 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 272 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 273 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 273.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 274 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 274.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 275 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 276 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 276 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 277 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 277.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 278 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 278.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 279 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 280 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 280 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 281 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 281.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 282 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 282.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 283 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 284 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 284 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 285 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 285 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 286 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 286 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 287 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 287.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 288 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 288.5f - spawnDelay ));

        StartCoroutine(HorizontalSpawn(4, 4, 289 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 290 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 290 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 291 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 291.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 292 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 293 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 294 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 294 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 295 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 295.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 296 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 297 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 298 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 298 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 299 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 299.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 300 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 301 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 302 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 302 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 303 - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 303.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 304 - spawnDelay ));

        StartCoroutine(HorizontalSpawn(0, 0, 305 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 305.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 306 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 306.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 307.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 308 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 309 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 309.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(0, 0, 310 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 310.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 311.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 312 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 313 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 313.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 314 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 314.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(3, 3, 315.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(4, 4, 316 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 317 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 317.5f - spawnDelay ));
        StartCoroutine(HorizontalSpawn(1, 1, 318 - spawnDelay ));
        StartCoroutine(HorizontalSpawn(2, 2, 318.5f - spawnDelay ));
        StartCoroutine(VerticalSpawn(0, 0, 320 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 320 - spawnDelay ));

        StartCoroutine(VerticalSpawn(0, 0, 321 - spawnDelay ));
        StartCoroutine(VerticalSpawn(2, 2, 321 - spawnDelay ));
    }

        private void playSong()
    {
        
        StartCoroutine(MakeDistractions());

        
        //StartCoroutine(HorizontalSpawn(0, 4, 5));
        //StartCoroutine(HorizontalSpawn(2, 2, 2));
        //StartCoroutine(HorizontalSpawn(0, 1, 4));
        //StartCoroutine(HorizontalSpawn(3, 4, 4));
        //StartCoroutine(VerticalSpawn(0, 0, 6));
        //StartCoroutine(VerticalSpawn(0, 2, 6));

        playWerq();
        






    }
}
