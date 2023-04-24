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
        XY,
        YY
    }
    public Genes[] flowerGenes;

    void Start()
    {
        if (flowerType == Species.Rose)
        {
            flowerGenes = new Genes[2];
        }
    }

    public void SendType()
    {

    }

    public void SendGenes(int genesArray, string newArray)
    {
        //picks up info and then bounces back to the script
        string info = "nothing";
        if (flowerGenes[genesArray] == Genes.XX)
        {
            info = "XX";
        }
        else if (flowerGenes[genesArray] == Genes.XY)
        {
            info = "XY";
        }
        else if (flowerGenes[genesArray] == Genes.YY)
        {
            info = "YY";
        }
        GameObject.Find("GameManager").GetComponent<Pollination>().SettingGenes(newArray, genesArray, info);
    }
}
