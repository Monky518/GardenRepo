using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xRange;
    public float speed;

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime);
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
