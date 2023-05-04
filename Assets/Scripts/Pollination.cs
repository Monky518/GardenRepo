using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollination : MonoBehaviour
{
    public enum Genes
    {
        XX,
        XY,
        YY
    }
    public Genes[] pollenGenes;
    public Genes[] flowerGenes;
    public Genes[] seedGenes;

    public enum Species
    {
        Rose,
        Unknown
    }
    public Species pollenType = Species.Unknown;
    public Species flowerType = Species.Unknown;
    public Species seedType = Species.Unknown;

    public int roseArrayLength = 3;

    public enum Steps
    {
        nothing,
        pollenStep,
        flowerStep,
        seedStep
    }
    public Steps currentStep = Steps.nothing;

    public GameObject jar;
    public GameObject pollenJar;
    public GameObject seed;

    private Vector3 holdingBoxPosition = new Vector3 (-10, 0, 0);

    void Update()
    {
        //any gameobject following the mouse movement
        FollowMouse();
    }
    
    void PollenButton()
    {
        if (currentStep == Steps.nothing)
        {
            //get a jar and start
            FindPollen();
        }
        else if (currentStep == Steps.pollenStep)
        {
            //put back the jar
            currentStep = Steps.nothing;
        }
    } 
    
    void FindPollen()
    {
        if (currentStep == Steps.nothing)
        {
            //resets pollen and flower to prep future seed
            pollenGenes = new Genes[0];
            flowerGenes = new Genes[0];
            seedGenes = new Genes[0];
            pollenType = Species.Unknown;
            flowerType = Species.Unknown;
            seedType = Species.Unknown;
            
            //automatically starts following mouse
            currentStep = Steps.pollenStep;
        }
        else if (currentStep == Steps.flowerStep || currentStep == Steps.pollenStep)
        {
            //sets moust rect by finding position, setting it into camera bounds, and setting the rect accordingly
            Vector3 nastyMousePos = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(nastyMousePos.x, nastyMousePos.y, 0));
            Rect mouse = new Rect(mousePos.x, mousePos.y, 0.5f, 0.5f);

            //find all flower gameobjects
            GameObject[] flower = GameObject.FindGameObjectsWithTag("Flower");

            //sets all flower rects
            Rect test = new Rect(0, 0, 1, 1);

            //check for overlap
            for (int i = 0; i < flower.Length; i++)
            {
                //sets new test rect
                test = new Rect(flower[i].transform.position, flower[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);

                //checks where player selected
                if (test.Overlaps(mouse))
                {
                    if (currentStep == Steps.pollenStep)
                    {
                        //finds and sets pollenType
                        GameObject.Find("GameManager").GetComponent<Flower>().SendType("Pollen");

                        //finds and sets pollenGenes
                        for (int j = 0; j < roseArrayLength; j++)
                        {
                            flower[i].GetComponent<Flower>().SendGenes(j, "Pollen");
                        }

                        //changes mode
                        currentStep = Steps.flowerStep;
                    }
                    else if (currentStep == Steps.flowerStep)
                    {
                        //finds, sets, and tests flowerType
                        GameObject.Find("GameManager").GetComponent<Flower>().SendType("Flower");
                        if (pollenType == flowerType && pollenType != Species.Unknown)
                        {
                            //finds and sets flowerGenes
                            for (int j = 0; j < roseArrayLength; j++)
                            {
                                flower[i].GetComponent<Flower>().SendGenes(j, "Flower");
                            }

                            //sets new seed things
                            NewGenetics();
                            seedType = pollenType;

                            //change mode
                            currentStep = Steps.seedStep;
                        }
                    }

                    //stops the search for the flower
                    break;
                }
            }
        }
        else if (currentStep == Steps.seedStep)
        {
            //gives seed to either flowerPot or storage for later
        }
    }

    public void NewGenetics()
    {
        //finds every single posibility for the genes
        for (int i = 0; i < pollenGenes.Length; i++)
        {
            if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.XX || pollenGenes[i] == Genes.YY && flowerGenes[i] == Genes.YY)
            {
                //if both XX or YY, same genes
                seedGenes[i] = pollenGenes[i];
                Debug.Log("Only XX or only YY");
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.YY || pollenGenes[i] == Genes.YY && flowerGenes[i] == Genes.XX)
            {
                //if both XX and YY, all XY
                seedGenes[i] = Genes.XY;
                Debug.Log("XY");
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.XY || pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.XX)
            {
                //if one XX and XY, half XX and half XY
                Debug.Log("XX or XY");
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    seedGenes[i] = Genes.XX;
                }
                else
                {
                    seedGenes[i] = Genes.XY;
                }
            }
            else if (pollenGenes[i] == Genes.YY && flowerGenes[i] == Genes.XY || pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.YY)
            {
                //if one YY and XY, half YY and half XY
                Debug.Log("XY or YY");
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    seedGenes[i] = Genes.YY;
                }
                else
                {
                    seedGenes[i] = Genes.XY;
                }
            }
            else if (pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.XY)
            {
                //if both XY, half XY, fourth YY, and fourth XX
                Debug.Log("XX XY or YY");
                float chance = Random.value;
                if (chance < 0.75f)
                {
                    seedGenes[i] = Genes.XX;
                }
                else if (chance < 0.25f)
                {
                    seedGenes[i] = Genes.XY;
                }
                else
                {
                    seedGenes[i] = Genes.YY;
                }
            }
        }
    }

    void NewFlowerSprite()
    {
        //sets new plant color (sprite and animation) depending on species
        //for rose, show all things
        //for other flower, show all
        //continue for all species

        if (seedType == Species.Rose)
        {
            //Red = seedGenes[0], notYellow = seedGenes[1], and White = seedGenes[2]

            if (seedGenes[1] == Genes.YY)
            {
                //check all yellow combinations
                Debug.Log("Checking yellow options");
                if (seedGenes[0] == Genes.XX)
                {
                    //coral rose
                }
                else if (seedGenes[0] == Genes.XY)
                {
                    //peach rose
                }
                else
                {
                    //yellow rose
                }
            }
            else if (seedGenes[0] != Genes.YY)
            {
                //check all red combinations
                Debug.Log("Checking red options");
                if (seedGenes[2] == Genes.XX)
                {
                    //pink rose
                }
                else if (seedGenes[2] == Genes.XY)
                {
                    //red rose
                }
                else
                {
                    //dark red rose
                }
            }
            else
            {
                //check all white combinations
                Debug.Log("Checking white options");
                if (seedGenes[2] == Genes.XX)
                {
                    //white rose
                }
                else if (seedGenes[2] == Genes.XY)
                {
                    //lavender rose
                }
                else
                {
                    //black rose
                }
            }
        }
    }

    void FollowMouse()
    {
        if (currentStep == Steps.pollenStep)
        {
            //follow mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            jar.transform.position = mousePosition;

            //restart position
            pollenJar.transform.position = holdingBoxPosition;
            seed.transform.position = holdingBoxPosition;
        }
        else if (currentStep == Steps.flowerStep)
        {
            //follow mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            pollenJar.transform.position = mousePosition;

            //restart position
            jar.transform.position = holdingBoxPosition;
            seed.transform.position = holdingBoxPosition;
        }
        else if (currentStep == Steps.seedStep)
        {
            //follow mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            seed.transform.position = mousePosition;

            //restart position
            jar.transform.position = holdingBoxPosition;
            pollenJar.transform.position = holdingBoxPosition;
        }
        else
        {
            //restart position
            jar.transform.position = holdingBoxPosition;
            pollenJar.transform.position = holdingBoxPosition;
            seed.transform.position = holdingBoxPosition;
        }
    }

    public void SetArrayLength (string arrayName, int length)
    {
        if (arrayName == "Pollen")
        {
            pollenGenes = new Genes[length];
        }
        else if (arrayName == "Flower")
        {
            flowerGenes = new Genes[length];
        }
        else if (arrayName == "Seed")
        {
            seedGenes = new Genes[length];
        }
    }
    
    public void SettingGenes (string arrayName, int arrayNumber, string geneType)
    {
        if (arrayName == "Pollen")
        {
            if (geneType == "XX")
            {
                pollenGenes[arrayNumber - 1] = Genes.XX;
            }
            else if (geneType == "XY")
            {
                pollenGenes[arrayNumber - 1] = Genes.XY;
            }
            else if (geneType == "YY")
            {
                pollenGenes[arrayNumber - 1] = Genes.YY;
            }
        }
        else if (arrayName == "Flower")
        {
            if (geneType == "XX")
            {
                flowerGenes[arrayNumber - 1] = Genes.XX;
            }
            else if (geneType == "XY")
            {
                flowerGenes[arrayNumber - 1] = Genes.XY;
            }
            else if (geneType == "YY")
            {
                flowerGenes[arrayNumber - 1] = Genes.YY;
            }
        }
    }

    public void SettingType (string objectName, string typeName)
    {
        if (typeName == "Rose")
        {
            if (objectName == "Flower")
            {
                flowerType = Species.Rose;
                flowerGenes = new Genes[roseArrayLength];
            }
            else if (objectName == "Pollen")
            {
                pollenType = Species.Rose;
                pollenGenes = new Genes[roseArrayLength];
            }
        }
    }
}
