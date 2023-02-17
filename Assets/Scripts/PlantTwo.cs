using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTwo : MonoBehaviour
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
    public int growingDuration = 7;
    public int bloomDuration = 10;
    public int unwateredDuration = 2;

    public int plantTimer = 0;
    public int unwateredTimer = 0;

    public bool watered = false;
    public RuntimeAnimatorController wateringAnimation;
    public float wateringAnimationTimer;

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
            GameObject.Find("gameObject.name/Bottom").GetComponent<SpriteRenderer>().sprite = plantSprout;
        }
        else if (currentStage == Stage.Growing)
        {
            GameObject.Find("gameObject.name/Bottom").GetComponent<SpriteRenderer>().sprite = plantGrowing;
        }
        else if (currentStage == Stage.Bloom)
        {
            GameObject.Find("gameObject.name/Bottom").GetComponent<SpriteRenderer>().sprite = plantBloomBottom;
            if (plantBloomTop != null)
            {
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = plantBloomTop;
            }
        }
        else if (currentStage == Stage.Wilting)
        {
            GameObject.Find("gameObject.name/Bottom").GetComponent<SpriteRenderer>().sprite = plantWilting;
        }
        else if (currentStage == Stage.Dead)
        {
            Destroy(gameObject);
        }
    }

    public Rect PlantRectUpdate()
    {
        if (gameObject.tag == "plantTwo")
        {
            Rect plantRect = new Rect(transform.position, GameObject.Find("gameObject.name/Bottom").transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
            return plantRect;
        }
        else
        {
            Rect plantRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
            return plantRect;
        }
    }
}
