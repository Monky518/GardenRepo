using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPackets : MonoBehaviour
{
    public GameObject plantSeed;

    public void NewPlant()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().newPlantSelected = plantSeed;
        Debug.Log("Pushed button");
    }
}
