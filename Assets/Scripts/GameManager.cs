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
}
