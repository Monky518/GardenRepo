using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum Genes
    {
        XX,
        XY,
        YY
    }
    public Genes pollenGenes;
    public Genes flowerGenes;

    public void BeginTest()
    {
        StartCoroutine(FindNewGens());
    }
    
    IEnumerator FindNewGens()
    {
        //makes it easier for me later
        GameObject gm = GameObject.Find("GameManager");
        string Pollen = "Pollen";
        string Flower = "Flower";

        //SET ARRAYS TO LENGTH ONE INSTEAD OF ZERO
        gm.GetComponent<Pollination>().SetArrayLength("Pollen", 1);
        gm.GetComponent<Pollination>().SetArrayLength("Flower", 1);
        gm.GetComponent<Pollination>().SetArrayLength("Seed", 1);

        //sets the new genes for flower and pollen
        if (pollenGenes == Genes.XX)
        {
            //zero x, pollen, array[0]
            gm.GetComponent<Pollination>().SettingGenes(0, Pollen, 0);
        }
        else if (pollenGenes == Genes.XY)
        {
            //one x, pollen, array[0]
            gm.GetComponent<Pollination>().SettingGenes(1, Pollen, 0);
        }
        else
        {
            //two x, pollen, array[0]
            gm.GetComponent<Pollination>().SettingGenes(2, Pollen, 0);
        }

        if (flowerGenes == Genes.XX)
        {
            //zero x, flower, array[0]
            gm.GetComponent<Pollination>().SettingGenes(0, Flower, 0);
        }
        else if (flowerGenes == Genes.XY)
        {
            //one x, flower, array[0]
            gm.GetComponent<Pollination>().SettingGenes(1, Flower, 0);
        }
        else
        {
            //two x, flower, array[0]
            gm.GetComponent<Pollination>().SettingGenes(2, Flower, 0);
        }

        //waits a bit and then runs for new seed genes
        yield return new WaitForSeconds(1);
        gm.GetComponent<Pollination>().NewGenetics();
    }
}
