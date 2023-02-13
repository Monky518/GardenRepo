using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void NewDay()
    {
        //one tall plants
        GameObject[] plantsOne = GameObject.FindGameObjectsWithTag("PlantOne");
        foreach (GameObject o in plantsOne)
        {
            o.GetComponent<PlantOne>().NewDay();
        }

        //two tall plants
        GameObject[] plantsTwo = GameObject.FindGameObjectsWithTag("PlantTwo");
        foreach (GameObject o in plantsTwo)
        {
            o.GetComponent<PlantTwo>().NewDay();
        }
    }

    public void Watering()
    {
        //sets rect to be smaller to allow some overlapping of pacMan and the ghosts
        rect playerRect = GameObject.Find("Player").transform.GetComponent<Player>().PlayerRect();
        GameObject[] plantsOne = GameObject.FindGameObjectWithTag("PlantOne");
        GameObject[] plantsTwo = GameObject.FindGameObjectWithTag("PlantTwo");

        rect[] plantRect;
        int counter = 0;
        foreach (GameObject po in plantsOne)
        {
            plantRect[counter] = po.transform.GetComponent<PlantOne>().plantRect;
            counter++;
        }
        foreach (GameObject pt in plantsTwo)
        {
            plantRect[counter] = pt.transform.GetComponent<PlantTwo>().plantRect;
            counter++;
        }

        counter = 0;
        foreach (GameObject pr in plantRect)
        {
            if (pr.Overlaps(playerRect))
            {
                //water plant
                if (plantsOne[counter] != null)
                {
                    plantsOne[counter].transform.GetComponent<PlantOne>().watering;
                }
                else if (plantsTwo[counter] != null)
                {
                    plantsTwo[counter].transform.GetComponent<PlantTwo>().watering;
                }
                else
                {
                    Debug.Log("Could not retrace steps for watering plant");
                }
            }

            //increase new counter
            counter++;
        }
    }
}
