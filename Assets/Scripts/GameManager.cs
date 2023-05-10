using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject newPlantSelected;
    public bool water;
    public bool pollenJar;

    public GameObject newSeed;
    
    void Update()
    {
        //I do not know how to make UI better buttons, so program them instead
        if (Input.GetButtonDown("Fire1"))
        {
            if (water)
            {
                Watering();
            }
            else if (newPlantSelected != null)
            {
                PlantSeedPlacement();
            }
        }
    }

    public void NewDay()
    {
        //plants are getting old
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (GameObject p in plants)
        {
            p.GetComponent<Plant>().NewDay();
        }
    }

    public void WateringTime()
    {
        //lever time
        if (water)
        {
            //no more watering time
            water = false;
        }
        else
        {
            //deselect plant
            newPlantSelected = null;

            //watering
            water = true;
        }
    }

    void Watering()
    {
        //sets moust rect by finding position, setting it into camera bounds, and setting the rect accordingly
        Vector3 nastyMousePos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(nastyMousePos.x, nastyMousePos.y, 0));
        Rect mouse = new Rect(mousePos.x, mousePos.y, 0.5f, 0.5f);

        //finds all plants
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        Debug.Log("Number of plants found: " + plants.Length);

        //sets test rect
        Rect test = new Rect(0, 0, 1, 1);

        //check for overlap
        for (int i = 0; i < plants.Length; i++)
        {
            //sets new test rect
            test = new Rect(plants[i].transform.position, plants[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size);
            Debug.Log("New test rect #" + i + ": " + test);

            //checks where player selected
            if (test.Overlaps(mouse))
            {
                Debug.Log("Watering Time!");
                //sets plant as watered
                plants[i].transform.GetComponent<Plant>().Watering();

                //holding shift to continue watering
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Debug.Log("Watering time continues");
                }
                else
                {
                    water = false;
                }

                //breaks
                Debug.Log("Watering time is over");
                break;
            }
        }
    }

    public void PlantSeedPlacement()
    {
        //sets moust rect by finding position, setting it into camera bounds, and setting the rect accordingly
        Vector3 nastyMousePos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(nastyMousePos.x, nastyMousePos.y, 0));
        Rect mouse = new Rect(mousePos.x, mousePos.y, 0.5f, 0.5f);

        //find all flower pot gameobjects
        GameObject[] flowerPot = GameObject.FindGameObjectsWithTag("FlowerPot");

        //sets all flower pot rects
        Rect test = new Rect(0, 0, 1, 1);

        //saving this for later
        bool anotherPlant = false;

        //check for overlap
        for (int i = 0; i < flowerPot.Length; i++)
        {
            //sets new test rect
            test = new Rect(flowerPot[i].transform.position, flowerPot[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);

            //test for another plant already in the pot
            anotherPlant = false;
            GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Plant");
            foreach (GameObject ap in allPlants)
            {
                if (ap.transform.position == new Vector3(flowerPot[i].transform.position.x, flowerPot[i].transform.position.y + 0.75f, 0))
                {
                    anotherPlant = true;
                    break;
                }
            }

            //checks where player selected
            if (test.Overlaps(mouse) && !anotherPlant)
            {
                Instantiate(newPlantSelected, new Vector2(flowerPot[i].transform.position.x, flowerPot[i].transform.position.y + 0.75f), Quaternion.identity);

                //holding shift to continue planting
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Debug.Log("Planting the most plants");
                }
                else
                {
                    newPlantSelected = null;
                }
                break;
            }
        }
    }
}
