using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollination : MonoBehaviour
{
    public enum Genes
    {
        Heterozygous,
        homozygousDominant,
        homozygousRecessive
    }
    public Genes[] pollenGenes;
    public Genes[] plantGenes;
    public Genes[] newPlantGenes;

    public enum FlowerType
    {
        Rose
    }
    public FlowerType pollen;
    public FlowerType plant;
    public FlowerType newPlantSeed;

    void FindPollen()
    {
        //finds all pollen genes and flower type
        //change jar gameobject to be full
    }

    void FindPlant()
    {
        //if flower type is the same as pollen
            //finds all plant genes
            //remove jar gameobject
    }

    void NewGenetics()
    {
        //sets new genetics for all genes depending on flower type
        //for gene array.length
            //if both homo and same, only homo of the same
            //if both homo and different, all hetero
            //if one homoDom and hetero, half homoRec and half hetero
            //if one homoRec and hetero, half homoRec and half hetero
            //if both hetero, half hetero, fourth homoRec, and fourth homoDom
            //set specific gene to newSeed genes
    }

    void NewFlowerSprite()
    {
        //sets new plant color (sprite and animation) depending on flower type
        //for rose, show all things
        //for other flower, show all
        //continue for all flowerTypes


        if (newPlantSeed = FlowerType.Rose)
        {
            //Red = 0, notYellow = 1, and White = 2

            if (newPlantGenes[1] == Genes.homozygousRecessive)
            {
                //check all yellow combinations
                Debug.Log("Checking yellow options");
                if (newPlantGenes[0] == Genes.homozygousDominant)
                {
                    //coral rose
                }
                else if (newPlantGenes[0] == Genes.Heterozygous)
                {
                    //peach rose
                }
                else
                {
                    //yellow rose
                }
            }
            else if (newPlantGenes[0] != Genes.homozygousRecessive)
            {
                //check all red combinations
                Debug.Log("Checking red options");
                if (newPlantGenes[2] == Genes.homozygousDominant)
                {
                    //pink rose
                }
                else if (newPlantGenes[2] == Genes.Heterozygous)
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
                if (newPlantGenes[2] == Genes.homozygousDominant)
                {
                    //white rose
                }
                else if (newPlantGenes[2] == Genes.Heterozygous)
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
