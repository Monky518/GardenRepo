using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject newPlantSelected;
    public bool wateringTime;
    
    void Update()
    {
        //I do not know how to make UI better buttons, so program them instead
        if (Input.GetButtonDown("Fire1"))
        {
            if (newPlantSelected != null)
            {
                PlantSeedPlacement();
            }
            else if (wateringTime)
            {
                Watering();
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

    void Watering()
    {
        //sets player rect
        Rect playerRect = GameObject.Find("Player").transform.GetComponent<Player>().PlayerRectUpdate();

        //finds all plants
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");

        //sets test rect
        Rect test = new Rect(0, 0, 1, 1);

        //check for overlap
        for (int i = 0; i < plants.Length; i++)
        {
            //sets new test rect
            test = new Rect(plants[i].transform.position, plants[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size);

            //checks where player selected
            if (test.Overlaps(playerRect))
            {
                //sets plant as watered
                plants[i].transform.GetComponent<Plant>().Watering();

                //breaks wateringTime
                wateringTime = false;
                break;
            }
        }
    }

    void PlantSeedPlacement()
    {
        //sets moust rect by finding position, setting it into camera bounds, and setting the rect accordingly
        Vector3 nastyMousePos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(nastyMousePos.x, nastyMousePos.y, 0));
        Rect mouse = new Rect(mousePos.x, mousePos.y, 0.5f, 0.5f);

        //find all flower pot gameobjects
        GameObject[] flowerPot = GameObject.FindGameObjectsWithTag("FlowerPot");

        //sets all flower pot rects
        Rect test = new Rect(0, 0, 1, 1);

        //temp placement
        bool anotherPlant = false;

        //check for overlap
        for (int i = 0; i < flowerPot.Length; i++)
        {
            //sets new test rect
            test = new Rect(flowerPot[i].transform.position, flowerPot[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);

            //test for another plant already in the pot
            //bool anotherPlant = false;
            GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Plant");
            foreach (GameObject ap in allPlants)
            {
                if (ap.transform.position == new Vector3(flowerPot[i].transform.position.x, flowerPot[i].transform.position.y + 0.75f, 0))
                {
                    anotherPlant = true;
                    Debug.Log("Another plant is here");
                    break;
                }
            }

            //checks where player selected
            if (test.Overlaps(mouse) && !anotherPlant)
            {
                Instantiate(newPlantSelected, new Vector2(flowerPot[i].transform.position.x, flowerPot[i].transform.position.y + 0.75f), Quaternion.identity);
                newPlantSelected = null;
                break;
            }
        }

        if (newPlantSelected != null && !anotherPlant)
        {
            Debug.Log("Misclicked");
        }
    }
}
