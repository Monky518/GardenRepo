using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollination : MonoBehaviour
{
    public enum Genes
    {
        Xx,
        XX,
        xx
    }
    public Genes[] pollenGenes;
    public Genes[] plantGenes;
    public Genes[] newSeedGenes;

    public enum FlowerType
    {
        Rose,
        None
    }
    public FlowerType pollenType = FlowerType.None;
    public FlowerType plantType = FlowerType.None;
    public FlowerType newSeedType = FlowerType.None;

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


        if (newSeedType == FlowerType.Rose)
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
