using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPackets : MonoBehaviour
{
    public GameObject plantSeed;

    public void NewPlant()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().newPlantSelected == plantSeed)
        {
            //deselect plant
            GameObject.Find("GameManager").GetComponent<GameManager>().newPlantSelected = null;
            Debug.Log("No more plant");
        }
        else
        {
            //sets plant type
            GameObject.Find("GameManager").GetComponent<GameManager>().newPlantSelected = plantSeed;
            Debug.Log("Selected plant: " + plantSeed);

            //no water
            GameObject.Find("GameObject").GetComponent<GameManager>().water = false;
        }
    }
}
