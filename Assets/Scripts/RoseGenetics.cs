using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseGenetics : MonoBehaviour
{
    public enum Genes
    {
        Heterozygous,
        homozygousDominant,
        homozygousRecessive
    }
    public Genes Red;
    public Genes notYellow;
    public Genes White;

    public GameObject blackRose;
    public GameObject darkRedRose;
    public GameObject redRose;
    public GameObject pinkRose;
    public GameObject whiteRose;
    public GameObject coralRose;
    public GameObject peachRose;
    public GameObject yellowRose;
    public GameObject lavenderRose;

    void NewRoseSeed()
    {
        //find new seed genes from pollination script for all three
        //checks "not yellow" to "red" and finally "white"
        //for now, set test plant with new colors (see if it works before growing cycle startings)

        //change this to show new red, new notYellow, and new white
        Debug.Log("New Red: " + Red);
        Debug.Log("New notYellow: " + notYellow);
        Debug.Log("New White: " + White);

        if (notYellow == Genes.homozygousRecessive)
        {
            //check all yellow combinations
            Debug.Log("Checking yellow options");
            if (Red == Genes.homozygousDominant)
            {
                //coral
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = coralRose;
            }
            else if (Red == Genes.Heterozygous)
            {
                //peach
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = peachRose;
            }
            else
            {
                //yellow
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = yellowRose;
            }
        }
        else if (Red != Genes.homozygousRecessive)
        {
            //check all red combinations
            Debug.Log("Checking red options");
            if (White == Genes.homozygousDominant)
            {
                //pink
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = pinkRose;
            }
            else if (White == Genes.Heterozygous)
            {
                //red
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = redRose;
            }
            else
            {
                //dark red
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = darkRedRose;
            }
        }
        else
        {
            //check all white combinations
            Debug.Log("Checking white options");
            if (White == Genes.homozygousDominant)
            {
                //white
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = whiteRose;
            }
            else if (White == Genes.Heterozygous)
            {
                //lavender
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = lavenderRose;
            }
            else
            {
                //black
                GameObject.Find("GameManager").GetComponent<GameManager>().newSeed = blackRose;
            }
        }
    }
}
