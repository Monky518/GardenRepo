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
    public Genes newSeed;

    void NewSeedGenes(Genes Plant, Genes Pollen)
    {
        //checks if any are the same and automatically moving on
        //else, see if both are homozygous
        //else, 1:2:1 ratio time
    }
}
