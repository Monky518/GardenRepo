using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Sprite plantGrowing;
    public Sprite plantBloom;
    public Sprite plantWilting;

    public enum Stage
    {
        Growing,
        Bloom,
        Wilting,
        Dead
    }

    Stage currentStage = Stage.Growing;
    Stage previousStage = Stage.Growing;

    public int growingDuration = 3;
    private int growingTimer = 0;

    public int bloomDuration = 1;
    public int bloomTimer = 0;

    public int unwateredDuration = 2;
    private int unwateredTimer = 2;

    public bool watered = false;

    void Update()
    {
        Debug.Log(currentStage);
    }
    
    public void NewDay()
    {
        if (currentStage == Stage.Growing)
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
        if (currentStage == Stage.Bloom)
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
}
