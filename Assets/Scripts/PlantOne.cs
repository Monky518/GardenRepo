using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOne : MonoBehaviour
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

    public int plantDuration = 16;
    private int plantTimer = 0;

    public int sproutDuration = 2;
    private int sproutTimer = 0;

    public int growingDuration = 5;
    private int growingTimer = 0;

    public int bloomDuration = 3;
    private int bloomTimer = 0;

    public int unwateredDuration = 2;
    private int unwateredTimer = 0;

    public bool watered = false;
    public RuntimeAnimatorController wateringAnimation;
    public float wateringAnimationTimer;


    void Update()
    {
        Debug.Log(currentStage);
    }

    public void NewDay()
    {
        plantTimer++;
        if (plantTimer >= 16)
        {
            if (currentStage != Stage.Wilting)
            {
                currentStage = Stage.Wilting;
                UpdateSprite();
            }
            else
            {
                currentStage = Stage.Dead;
                UpdateSprite();
            }
        }
        else
        {
            if (currentStage == Stage.Sprout)
            {
                //water and prevent wilting
                watered = false;
                unwateredTimer = 0;

                //sprout timer
                sproutTimer++;
                if (sproutTimer >= sproutDuration)
                {
                    previousStage = Stage.Growing;
                    currentStage = Stage.Growing;
                    UpdateSprite();
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
                    if (growingTimer >= growingDuration)
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

                    ///prep a recovery day for growing plant
                    //growingTimer--;
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
                    if (bloomTimer >= bloomDuration)
                    {
                        currentStage = Stage.Dead;
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
                    bloomTimer++;
                    if (bloomTimer >= bloomDuration)
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
    }

    public void Watering()
    {
        watered = true;
        StartCoroutine(WateringTimer());
    }

    public IEnumerator WateringTimer()
    {
        //watering animation
        transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
        transform.GetComponent<Animator>().enabled = true;

        //waiting for the animation to end
        yield return new WaitForSeconds(wateringAnimationTimer);

        //watering animation is done
        transform.GetComponent<Animator>().enabled = false;
    }

    void UpdateSprite()
    {
        if (currentStage == Stage.Sprout)
        {
            if (plantGrowing != null)
            {
                GetComponent<SpriteRenderer>().sprite = plantSprout;
            }
        }
        else if (currentStage == Stage.Growing)
        {
            if (plantGrowing != null)
            {
                GetComponent<SpriteRenderer>().sprite = plantGrowing;
            }
        }
        else if (currentStage == Stage.Bloom)
        {
            if (plantBloom != null)
            {
                GetComponent<SpriteRenderer>().sprite = plantBloom;
            }
        }
        else if (currentStage == Stage.Wilting)
        {
            if (plantWilting != null)
            {
                GetComponent<SpriteRenderer>().sprite = plantWilting;
            }
        }
        else if (currentStage == Stage.Dead)
        {
            Destroy(gameObject);
        }
    }

    public Rect PlantRectUpdate()
    {
        Rect plantRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
        return plantRect;
    }
}
