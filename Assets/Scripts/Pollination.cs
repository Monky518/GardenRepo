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
    public Genes[] newSeedGenes;

    public enum Species
    {
        Rose,
        Unknown
    }
    public Species pollenType = Species.Unknown;
    public Species flowerType = Species.Unknown;
    public Species newSeedType = Species.Unknown;

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

    private Vector3 jarStartPosition;
    private Vector3 pollenJarStartPosition;
    private Vector3 seedStartPosition;

    void Start()
    {
        //finds starting position for each
        jarStartPosition = jar.transform.position;
        pollenJarStartPosition = pollenJar.transform.position;
        seedStartPosition = seed.transform.position;
    }
    
    void Update()
    {
        FollowMouse();
    }
    
    void FindPollen()
    {
        //run when what?
        //finds selected object
        //grabs species then genes (setting array amount first)
        //change jar gameobject to be full



        if (currentStep == Steps.nothing)
        {
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

            //sets all flower pot rects
            Rect test = new Rect(0, 0, 1, 1);

            //check for overlap
            for (int i = 0; i < flower.Length; i++)
            {
                //sets new test rect
                test = new Rect(flower[i].transform.position, flower[i].transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);

                //checks where player selected
                if (test.Overlaps(mouse))
                {
                    ///find flowerType with array length values and then a "for" statement for each of the genes
                    ///place all next things in the "for" statement

                    if (currentStep == Steps.pollenStep)
                    {
                        flower[i].GetComponent<Flower>().SendGenes(i, "Pollen");
                    }
                    else if (currentStep == Steps.flowerStep)
                    {
                        flower[i].GetComponent<Flower>().SendGenes(i, "Flower");
                    }

                    //stops the search for the flower
                    break;
                }
            }
        }



        ///Select button which spawns jar
        ///Select flower and replace jar with pollen jar
        ///Select flower and replace pollen jar with seed
        ///Select pot or storage and despawn seed
    }

    void FindPlant()
    {
        //run when what?
        //finds selected object
        //grabs species then genes (setting array amount first)
        //if species is the same as pollen
        //finds all plant genes
        //remove jar gameobject
        //else player error message

        //sets array length so it doesn't scream at me later
        if (pollenType == Species.Rose)
        {
            newSeedGenes = new Genes[pollenGenes.Length - 1];
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
                newSeedGenes[i] = pollenGenes[i];
                Debug.Log("Only XX or only YY");
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.YY || pollenGenes[i] == Genes.YY && flowerGenes[i] == Genes.XX)
            {
                //if both XX and YY, all XY
                newSeedGenes[i] = Genes.XY;
                Debug.Log("XY");
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.XY || pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.XX)
            {
                //if one XX and XY, half XX and half XY
                Debug.Log("XX or XY");
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    newSeedGenes[i] = Genes.XX;
                }
                else
                {
                    newSeedGenes[i] = Genes.XY;
                }
            }
            else if (pollenGenes[i] == Genes.YY && flowerGenes[i] == Genes.XY || pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.YY)
            {
                //if one YY and XY, half YY and half XY
                Debug.Log("XY or YY");
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    newSeedGenes[i] = Genes.YY;
                }
                else
                {
                    newSeedGenes[i] = Genes.XY;
                }
            }
            else if (pollenGenes[i] == Genes.XY && flowerGenes[i] == Genes.XY)
            {
                //if both XY, half XY, fourth YY, and fourth XX
                Debug.Log("XX XY or YY");
                float chance = Random.value;
                if (chance < 0.75f)
                {
                    newSeedGenes[i] = Genes.XX;
                }
                else if (chance < 0.25f)
                {
                    newSeedGenes[i] = Genes.XY;
                }
                else
                {
                    newSeedGenes[i] = Genes.YY;
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

        if (newSeedType == Species.Rose)
        {
            //Red = newSeedGenes[0], notYellow = newSeedGenes[1], and White = newSeedGenes[2]

            if (newSeedGenes[1] == Genes.YY)
            {
                //check all yellow combinations
                Debug.Log("Checking yellow options");
                if (newSeedGenes[0] == Genes.XX)
                {
                    //coral rose
                }
                else if (newSeedGenes[0] == Genes.XY)
                {
                    //peach rose
                }
                else
                {
                    //yellow rose
                }
            }
            else if (newSeedGenes[0] != Genes.YY)
            {
                //check all red combinations
                Debug.Log("Checking red options");
                if (newSeedGenes[2] == Genes.XX)
                {
                    //pink rose
                }
                else if (newSeedGenes[2] == Genes.XY)
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
                if (newSeedGenes[2] == Genes.XX)
                {
                    //white rose
                }
                else if (newSeedGenes[2] == Genes.XY)
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
            pollenJar.transform.position = pollenJarStartPosition;
            seed.transform.position = seedStartPosition;
        }
        else if (currentStep == Steps.flowerStep)
        {
            //follow mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            pollenJar.transform.position = mousePosition;

            //restart position
            jar.transform.position = jarStartPosition;
            seed.transform.position = seedStartPosition;
        }
        else if (currentStep == Steps.seedStep)
        {
            //follow mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            seed.transform.position = mousePosition;

            //restart position
            jar.transform.position = jarStartPosition;
            pollenJar.transform.position = pollenJarStartPosition;
        }
        else
        {
            //restart position
            jar.transform.position = jarStartPosition;
            pollenJar.transform.position = pollenJarStartPosition;
            seed.transform.position = seedStartPosition;
        }
    }

    public void SetArrayLength(string arrayName, int length)
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
            newSeedGenes = new Genes[length];
        }
    }
    
    public void SettingGenes(string arrayName, int arrayNumber, string geneType)
    {
        if (arrayName == "Pollen")
        {
            if (geneType == "XX")
            {
                pollenGenes[arrayNumber] = Genes.XX;
            }
            else if (geneType == "XY")
            {
                pollenGenes[arrayNumber] = Genes.XY;
            }
            else if (geneType == "YY")
            {
                pollenGenes[arrayNumber] = Genes.YY;
            }
        }
        else if (arrayName == "Flower")
        {
            if (geneType == "XX")
            {
                flowerGenes[arrayNumber] = Genes.XX;
            }
            else if (geneType == "XY")
            {
                flowerGenes[arrayNumber] = Genes.XY;
            }
            else if (geneType == "YY")
            {
                flowerGenes[arrayNumber] = Genes.YY;
            }
        }
    }
}
