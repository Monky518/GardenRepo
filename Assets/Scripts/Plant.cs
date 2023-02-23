using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
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
        if (plantBloomTop == null)
        {
            //watering animation for one tall
            transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
            transform.GetComponent<Animator>().enabled = true;
        }
        else if (currentStage == Stage.Bloom)
        {
            //bottom part
            GameObject.Find("Bottom").transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
            GameObject.Find("Bottom").transform.GetComponent<Animator>().enabled = true;

            //top part
            GameObject.Find("Top").transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
            GameObject.Find("Top").transform.GetComponent<Animator>().enabled = true;
        }
        else
        {
            //bottom part
            GameObject.Find("Bottom").transform.GetComponent<Animator>().runtimeAnimatorController = wateringAnimation;
            GameObject.Find("Bottom").transform.GetComponent<Animator>().enabled = true;
        }

        //waiting for the animation to end
        yield return new WaitForSeconds(wateringAnimationTimer);

        if (plantBloomTop == null)
        {
            //watering animation is done for one tall
            transform.GetComponent<Animator>().enabled = false;
        }
        else if (currentStage == Stage.Bloom)
        {
            //watering animation is done
            GameObject.Find("Bottom").transform.GetComponent<Animator>().enabled = false;
            GameObject.Find("Top").transform.GetComponent<Animator>().enabled = false;
        }
        else
        {
            //watering animation is done
            GameObject.Find("Bottom").transform.GetComponent<Animator>().enabled = false;
        }
    }

    void UpdateSprite()
    {
        if (plantBloomTop != null)
        {
            if (currentStage == Stage.Sprout)
            {
                GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantSprout;
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (currentStage == Stage.Growing)
            {
                GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantGrowing;
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (currentStage == Stage.Bloom)
            {
                GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantBloomBottom;
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = plantBloomTop;
            }
            else if (currentStage == Stage.Wilting)
            {
                GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite = plantWilting;
                GameObject.Find("Top").GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (currentStage == Stage.Dead)
            {
                Destroy(gameObject);
            }
        }
        else
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
                transform.GetComponent<SpriteRenderer>().sprite = plantBloomBottom;
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

    public Rect PlantRectUpdate()
    {
        if (plantBloomTop != null)
        {
            Rect plantRect = new Rect(transform.position, GameObject.Find("Bottom").GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
            Debug.Log("PlantTwo rect: " + plantRect);
            return plantRect;
        }
        else
        {
            Rect plantRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
            Debug.Log("PlantOne rect: " + plantRect);
            return plantRect;
        }
    }
}
