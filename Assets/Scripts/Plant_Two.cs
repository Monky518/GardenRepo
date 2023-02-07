using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Two : MonoBehaviour
{
    public Sprite plantSprout;
    public Sprite plantGrowing;
    public Sprite plantBloomTop;
    public Sprite plantBloomBottom;
    public Sprite plantWilting;

    public enum Stage
    {
        Sprout,
        Growing,
        Bloom,
        Wilting,
        Dead
    }

    Stage currentStage = Stage.Sprout;
    Stage previousStage = Stage.Sprout;

    public int sproutDuration = 2;
    private int sproutTimer = 0;

    public int growingDuration = 5;
    private int growingTimer = 0;

    public int bloomDuration = 3;
    private int bloomTimer = 0;

    public int unwateredDuration = 2;
    private int unwateredTimer = 0;

    public bool watered = false;

    void Update()
    {
        Debug.Log(currentStage);
    }

    public void NewDay()
    {
        if (currentStage == Stage.Sprout)
        {
            if (watered)
            {
                //water and prevent wilting
                watered = false;
                unwateredTimer = 0;

                //sprout timer
                sproutTimer++;
                if (sproutTimer == sproutDuration)
                {
                    previousStage = Stage.Growing;
                    currentStage = Stage.Growing;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer--;
                if (unwateredTimer <= 0)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }

                //prep a recovery day for growing plant
                sproutTimer--;
            }
        }
        else if (currentStage == Stage.Growing)
        {
            if (watered)
            {
                //water and prevent wilting
                watered = false;
                unwateredTimer = unwateredDuration;

                //growing timer
                growingTimer++;
                if (growingTimer == growingDuration)
                {
                    previousStage = Stage.Bloom;
                    currentStage = Stage.Bloom;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer--;
                if (unwateredTimer <= 0)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }

                //prep a recovery day for growing plant
                growingTimer--;
            }
        }
        else if (currentStage == Stage.Bloom)
        {
            if (watered)
            {
                //water and prevent wilting
                watered = false;
                unwateredTimer = unwateredDuration;

                //blooming timer
                bloomTimer++;
                if (bloomTimer == bloomDuration)
                {
                    currentStage = Stage.Dead;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer--;
                if (unwateredTimer <= 0)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }

                //still subtract a bloom day with death chance
                bloomTimer++;
                if (bloomTimer == bloomDuration)
                {
                    currentStage = Stage.Dead;
                    UpdateSprite();
                }
            }
        }
        else if (currentStage == Stage.Wilting)
        {
            if (watered)
            {
                currentStage = previousStage;
            }
            else
            {
                //bye bye plant
                currentStage = Stage.Dead;
                UpdateSprite();
            }
        }
        else if (currentStage == Stage.Dead)
        {
            Debug.Log("Why are you still alive? " + transform.position);
            UpdateSprite();
        }
    }

    public void Watering()
    {
        watered = true;
    }

    void UpdateSprite()
    {
        if (currentStage == Stage.Sprout)
        {
            GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantSprout;
        }
        else if (currentStage == Stage.Growing)
        {
            GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantGrowing;
        }
        else if (currentStage == Stage.Bloom)
        {
            GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantBloomBottom;
            if (plantBloomTop != null)
            {
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = plantBloomTop;
            }
        }
        else if (currentStage == Stage.Wilting)
        {
            GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantWilting;
        }
        else if (currentStage == Stage.Dead)
        {
            Destroy(gameObject);
        }
    }
}
