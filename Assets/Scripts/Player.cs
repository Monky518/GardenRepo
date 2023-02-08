using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xRange;
    public float yRange;
    public float speed;

    void Update()
    {
        //inputs for movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //setting both as a vector 2
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //calculating the change
        transform.Translate(movement * speed * Time.deltaTime);

        //change so y does not change
        ///float moveHorizontal = Input.GetAxisRaw("Horizontal");
        ///transform.position = new Vector2(moveHorizontal * speed * Time.deltaTime, yRange);
        ///Debug.Log(transform.position);
    }

    void LateUpdate()
    {
        if (transform.position.x > 8.4)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }
        else if (transform.position.x < -8.4)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }
    }
}
