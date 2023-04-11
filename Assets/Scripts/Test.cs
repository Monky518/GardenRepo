using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum Genes
    {
        XX,
        Xx,
        xx
    }
    public Genes pollenGenes;
    public Genes flowerGenes;

    void FindNewGens()
    {
        //makes it easier for me later
        GameObject gm = GameObject.Find("GameManager");

        if (pollenGenes == Genes.XX)
        {
            gm.GetComponent<Pollination>().pollenGenes[0] = gm.GetComponent<Pollination>().Genes.XX;
        }
        else if (pollenGenes == Genes.Xx)
        {
            //gm.GetComponent<Pollination>().pollenGenes = gm.GetComponent<Pollination>().Genes.Xx;
        }
        else
        {
            //gm.GetComponent<Pollination>().pollenGenes = gm.GetComponent<Pollination>().Genes.xx;
        }

        if (flowerGenes == Genes.XX)
        {
            //gm.GetComponent<Pollination>().flowerGenes = gm.GetComponent<Pollination>().Genes.XX;
        }
        else if (flowerGenes == Genes.Xx)
        {
            //gm.GetComponent<Pollination>().flowerGenes = gm.GetComponent<Pollination>().Genes.Xx;
        }
        else
        {
            //gm.GetComponent<Pollination>().flowerGenes = gm.GetComponent<Pollination>().Genes.xx;
        }

        gm.GetComponent<Pollination>().NewGenetics();
    }
}
