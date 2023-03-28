using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public enum FlowerType
    {
        Rose,
        None
    }
    public FlowerType plantType;

    public enum Genes
    {
        Xx,
        XX,
        xx
    }
    public Genes[] plantGenes;

    void Start()
    {
        if (plantType == FlowerType.Rose)
        {
            plantGenes = new Genes[2];
        }
    }
}
