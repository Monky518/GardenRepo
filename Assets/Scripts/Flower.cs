using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public enum Species
    {
        Rose,
        Unknown
    }
    public Species flowerType;

    public enum Genes
    {
        XX,
        Xx,
        xx
    }
    public Genes[] flowerGenes;

    void Start()
    {
        if (flowerType == Species.Rose)
        {
            flowerGenes = new Genes[2];
        }
    }
}
