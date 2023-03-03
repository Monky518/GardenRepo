using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject newPlantSelected;
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Watering();
            Debug.Log("You pressed space!");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PlantSeedPlacement();
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
        foreach (Rect pr in plantRect)
        {
            if (counter < plantsOne.Length)
            {
                plantRect[counter] = plantsOne[counter].GetComponent<Plant>().PlantRectUpdate();
            }
            else
            {
                plantRect[counter] = plantsTwo[counter - plantsOne.Length].GetComponent<Plant>().PlantRectUpdate();
            }
            counter++;
        }

        Debug.Log("All plantRects: " + plantRect);

        //sees if any plant rects overlap with player
        foreach (Rect pr in plantRect)
        {
            //checks if it overlaps
            if (pr.Overlaps(playerRect))
            {
                //checks all plantsOne
                foreach (GameObject po in plantsOne)
                {
                    Rect test = po.GetComponent<Plant>().PlantRectUpdate();
                    if (pr == test)
                    {
                        po.GetComponent<Plant>().Watering();
                        Debug.Log("Watering time!");
                        break;
                    }
                }

                //checks all plantsTwo
                foreach (GameObject pt in plantsTwo)
                {
                    Rect test = pt.GetComponent<Plant>().PlantRectUpdate();
                    if (pr == test)
                    {
                        pt.GetComponent<Plant>().Watering();
                        Debug.Log("Watering time!");
                        break;
                    }
                }

            }
            else
            {
                Debug.Log("Not this plant");
            }
        }
    }

    void PlantSeedPlacement()
    {
        if (newPlantSelected != null)
        {
            //sets moust rect
            Vector3 mousePos = Input.mousePosition;
            Rect mouse = new Rect(mousePos.x, mousePos.y, 0.5f, 0.5f);

            //find all flower pot gameobjects
            GameObject[] flowerPot = GameObject.FindGameObjectsWithTag("FlowerPot");

            //sets all flower pot rects
            Rect test = new Rect(0, 0, 1, 1);

            //check for overlap
            for (int i = 0; i < flowerPot.Length; i++)
            {
                //sets new test rect
                test = new Rect(flowerPot[i].transform.position, flowerPot[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
                
                //checks where player selected
                if (test.Overlaps(mouse))
                {
                    Instantiate(newPlantSelected, new Vector2(flowerPot[i].transform.position.x, flowerPot[i].transform.position.y + 0.75f), Quaternion.identity);
                    newPlantSelected = null;
                    break;
                }
            }
        }
    }
}
