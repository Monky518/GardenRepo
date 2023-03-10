using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Sprite plantSprout;
    public Sprite plantGrowing;
    public Sprite plantBloom;
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
    public int growingDuration = 7;
    public int bloomDuration = 10;
    public int unwateredDuration = 2;

    public int plantTimer = 0;
    public int unwateredTimer = 0;

    public bool watered = false;
    public RuntimeAnimatorController wateringAnimation;
    public float wateringAnimationTimer;

    public void NewDay()
    {
        //add water timer for overwatering and underwatering (fix later)
        
        //very long growing cycle (fix later)
        if (currentStage == Stage.Sprout)
        {
            if (watered)
            {
                //water and prevent wilting
                watered = false;
                unwateredTimer = 0;

                //sprout timer check
                plantTimer++;
                if (plantTimer >= sproutDuration)
                {
                    previousStage = Stage.Growing;
                    currentStage = Stage.Growing;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer++;
                if (unwateredTimer >= unwateredDuration)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }
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
                plantTimer++;
                if (plantTimer >= growingDuration)
                {
                    previousStage = Stage.Bloom;
                    currentStage = Stage.Bloom;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer++;
                if (unwateredTimer >= unwateredDuration)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }
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
                plantTimer++;
                if (plantTimer >= bloomDuration)
                {
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }
            }
            else
            {
                //wilting
                unwateredTimer++;
                if (unwateredTimer >= unwateredDuration)
                {
                    //no previous stage when wilting
                    currentStage = Stage.Wilting;
                    UpdateSprite();
                }

                //still subtract a bloom day with death chance
                plantTimer++;
                if (plantTimer >= bloomDuration)
                {
                    currentStage = Stage.Dead;
                    UpdateSprite();
                }
            }
        }
        else if (currentStage == Stage.Wilting)
        {
            if (plantTimer >= bloomDuration)
            {
                //bye bye plant
                currentStage = Stage.Dead;
                UpdateSprite();
            }
            else
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
        }
        else if (currentStage == Stage.Dead)
        {
            Debug.Log("Why are you still alive?: " + transform.position);
            UpdateSprite();
        }
    }

    public void Watering()
    {
        watered = true;
        StartCoroutine(WateringTimer());
    }

    public IEnumerator WateringTimer()
    {
        //animation time begins
        transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
        transform.GetComponent<Animator>().enabled = true;

        //waiting for the animation to end
        yield return new WaitForSeconds(wateringAnimationTimer);

        //animation time is over
        transform.GetComponent<Animator>().enabled = false;
    }

    void UpdateSprite()
    {
        if (currentStage == Stage.Sprout)
        {
            transform.GetComponent<SpriteRenderer>().sprite = plantSprout;
        }
        else if (currentStage == Stage.Growing)
        {
            transform.GetComponent<SpriteRenderer>().sprite = plantGrowing;
        }
        else if (currentStage == Stage.Bloom)
        {
            transform.GetComponent<SpriteRenderer>().sprite = plantBloom;
        }
        else if (currentStage == Stage.Wilting)
        {
            transform.GetComponent<SpriteRenderer>().sprite = plantWilting;
        }
        else if (currentStage == Stage.Dead)
        {
            Destroy(gameObject);
        }
    }
}
