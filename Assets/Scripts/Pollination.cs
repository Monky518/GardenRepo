using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollination : MonoBehaviour
{
    public enum Genes
    {
        XX,
        Xx,
        xx
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
        //grabs genes and species
        //change jar gameobject to be full
    }

    void FindPlant()
    {
        //run when what?
        //finds selected object
        //grabs genes and species
        //if species is the same as pollen
        //finds all plant genes
        //remove jar gameobject
    }

    void NewGenetics()
    {
        //sets array length so it doesn't scream at me later
        if (pollenType == Species.Rose)
        {
            newSeedGenes[] = new Genes[pollenGenes.Length - 1];
        }

        //finds every single posibility for the genes
        for (int i = 0; i < pollenGenes; i++)
        {
            if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.XX || pollenGenes[i] == Genes.xx && flowerGenes[i] == Genes.xx)
            {
                //if both XX or xx, same genes
                newSeedGenes[i] = pollenGenes[i];
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.xx || pollenGenes[i] == Genes.xx && pollenGenes[i] == Genes.XX) { }
            {
                //if both XX and xx, all Xx
                newSeedGenes[i] = Genes.Xx;
            }
            else if (pollenGenes[i] == Genes.XX && flowerGenes[i] == Genes.Xx || pollenGenes[i] == Genes.Xx && flowerGenes[i] == Genes.XX)
            {
                //if one XX and Xx, half XX and half Xx
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    newSeedGenes[i] == Genes.XX;
                }
                else
                {
                    newSeedGenes[i] == Genes.Xx;
                }
            }
            else if (pollenGenes[i] == Genes.xx && flowerGenes[i] == Genes.Xx || pollenGenes[i] == Genes.Xx && flowerGenes[i] == Genes.xx)
            {
                //if one xx and Xx, half xx and half Xx
                float chance = Random.value;
                if (chance < 0.5f)
                {
                    newSeedGenes[i] == Genes.xx;
                }
                else
                {
                    newSeedGenes[i] == Genes.Xx;
                }
            }
            else if (pollenGenes[i] == Genes.Xx && flowerGenes[i] == Genes.Xx)
            {
                //if both Xx, half Xx, fourth xx, and fourth XX
                float chance = Random.value;
                if (chance < 0.75f)
                {
                    newSeedGenes[i] = Genes.XX;
                }
                else if (chance < 0.25f)
                {
                    newSeedGenes[i] = Genes.Xx;
                }
                else
                {
                    newSeedGenes[i] = Genes.xx;
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

            if (newSeedGenes[1] == Genes.xx)
            {
                //check all yellow combinations
                Debug.Log("Checking yellow options");
                if (newSeedGenes[0] == Genes.XX)
                {
                    //coral rose
                }
                else if (newSeedGenes[0] == Genes.Xx)
                {
                    //peach rose
                }
                else
                {
                    //yellow rose
                }
            }
            else if (newSeedGenes[0] != Genes.xx)
            {
                //check all red combinations
                Debug.Log("Checking red options");
                if (newSeedGenes[2] == Genes.XX)
                {
                    //pink rose
                }
                else if (newSeedGenes[2] == Genes.Xx)
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
                else if (newSeedGenes[2] == Genes.Xx)
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
}
