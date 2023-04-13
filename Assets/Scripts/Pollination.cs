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

    void FindPollen()
    {
        //run when what?
        //finds selected object
        //grabs species then genes (setting array amount first)
        //change jar gameobject to be full

        //sets array length so it doesn't scream at me later
        if (pollenType == Species.Rose)
        {
            newSeedGenes = new Genes[pollenGenes.Length - 1];
        }
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
            //Red = 0, notYellow = 1, and White = 2

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
    
    public void SettingGenes(int x, string arrayName, int arrayNumber)
    {
        if (arrayName == "Pollen")
        {
            if (x == 0)
            {
                pollenGenes[arrayNumber] = Genes.XX;
            }
            else if (x == 1)
            {
                pollenGenes[arrayNumber] = Genes.XY;
            }
            else if (x == 2)
            {
                pollenGenes[arrayNumber] = Genes.YY;
            }
        }
        else if (arrayName == "Flower")
        {
            if (x == 0)
            {
                flowerGenes[arrayNumber] = Genes.XX;
            }
            else if (x == 1)
            {
                flowerGenes[arrayNumber] = Genes.XY;
            }
            else if (x == 2)
            {
                flowerGenes[arrayNumber] = Genes.YY;
            }
        }
    }
}
