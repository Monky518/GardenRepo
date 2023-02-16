using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Watering();
        }
    }

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
        Rect playerRect = GameObject.Find("Player").transform.GetComponent<Player>().PlayerRectUpdate();
        GameObject[] plantsOne = GameObject.FindGameObjectsWithTag("PlantOne");
        GameObject[] plantsTwo = GameObject.FindGameObjectsWithTag("PlantTwo");

        Rect[] plantRect = new Rect[plantsOne.Length + plantsTwo.Length];
        int counter = 0;
        foreach (GameObject po in plantsOne)
        {
            plantRect[counter] = po.transform.GetComponent<PlantOne>().PlantRectUpdate();
            counter++;
        }
        foreach (GameObject pt in plantsTwo)
        {
            plantRect[counter] = pt.transform.GetComponent<PlantTwo>().PlantRectUpdate();
            counter++;
        }

        foreach (Rect pr in plantRect)
        {
            Debug.Log(pr);
        }

        counter = 0;
        foreach (Rect pr in plantRect)
        {
            if (pr.Overlaps(playerRect))
            {
                //water plant
                Rect por = plantsOne[counter].transform.GetComponent<PlantOne>().PlantRectUpdate();
                Rect ptr = plantsTwo[counter].transform.GetComponent<PlantTwo>().PlantRectUpdate();
                if (pr == por)
                {
                    //plantOne is watered
                    plantsOne[counter].transform.GetComponent<PlantOne>().Watering();
                }
                else if (pr == ptr)
                {
                    //plantTwo is watered
                    plantsTwo[counter].transform.GetComponent<PlantTwo>().Watering();
                }
            }

            //increase new counter
            counter++;
        }
    }
}
