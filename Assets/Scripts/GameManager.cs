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
            Debug.Log("You pressed space!");
        }
    }

    public void NewDay()
    {
        //one tall plants
        GameObject[] plantsOne = GameObject.FindGameObjectsWithTag("PlantOne");
        foreach (GameObject o in plantsOne)
        {
            o.GetComponent<Plant>().NewDay();
        }

        //two tall plants
        GameObject[] plantsTwo = GameObject.FindGameObjectsWithTag("PlantTwo");
        foreach (GameObject o in plantsTwo)
        {
            o.GetComponent<Plant>().NewDay();
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
        Debug.Log("Total plants found: " + plantRect.Length);

        //finds all plant rects
        int counter = 0;
        foreach (GameObject go in plantsOne)
        {
            plantRect[counter] = go.GetComponent<Plant>().PlantRectUpdate();
        }
        foreach (GameObject gt in plantsTwo)
        {
            plantRect[counter] = gt.GetComponent<Plant>().PlantRectUpdate();
        }
        Debug.Log("All plantRects: " + plantRect);

        //sees if any plant rects overlap with player
        counter = 0;
        foreach (Rect pr in plantRect)
        {
            //checks if it overlaps
            if (pr.Overlaps(playerRect))
            {
                //water plant
                if (counter >= plantsOne.Length)
                {
                    //plantTwo watering time
                    plantsTwo[counter - plantsOne.Length].GetComponent<Plant>().Watering();
                }
                else
                {
                    //plantOne watering time
                    plantsOne[counter].GetComponent<Plant>().Watering();
                }
                
                Debug.Log("Watering time!");
            }
            else
            {
                Debug.Log("Not this plant");
            }
            counter++;
        }
    }
}
