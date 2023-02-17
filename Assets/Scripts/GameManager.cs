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
        //sets player rect
        Rect playerRect = GameObject.Find("Player").transform.GetComponent<Player>().PlayerRectUpdate();
        //finds all plants
        GameObject[] plantsOne = GameObject.FindGameObjectsWithTag("PlantOne");
        GameObject[] plantsTwo = GameObject.FindGameObjectsWithTag("PlantTwo");

        //sets plantRect[] length
        Rect[] plantRect = new Rect[plantsOne.Length + plantsTwo.Length];
        //finds all plant rects and sees if they overlap with player
        int counter = 0;
        foreach (Rect pr in plantRect)
        {
            pr = po.transform.GetComponent<PlantOne>().PlantRectUpdate();
            //checks if it overlaps
            if (pr.Overlaps(playerRect))
            {
                //water plant
                Rect por = plantsOne[counter].GetComponent<Plant>().PlantRectUpdate();
                Rect ptr = plantsTwo[counter].GetComponent<Plant>().PlantRectUpdate();
                if (plantRect[counter] == por)
                {
                    //plantOne is watered
                    plantsOne[counter].transform.GetComponent<Plant>().Watering();
                }
                else if (plantRect[counter] == ptr)
                {
                    //plantTwo is watered
                    plantsTwo[counter].transform.GetComponent<Plant>().Watering();
                }
            }
            counter++;
        }




        foreach (GameObject pt in plantsTwo)
        {
            plantRect[counter] = pt.transform.GetComponent<PlantTwo>().PlantRectUpdate();
            counter++;
        }
        counter = 0;
        foreach (Rect pr in plantRect)
        {
            if (pr.Overlaps(playerRect))
            {
                //water plant
                Rect por = plantsOne[counter].GetComponent<PlantOne>().PlantRectUpdate();
                Rect ptr = plantsTwo[counter].GetComponent<PlantTwo>().PlantRectUpdate();
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
