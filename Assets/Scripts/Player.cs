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
        Vector2 movement = new Vector2(moveHorizontal, 0f);
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

    public Rect PlayerRectUpdate()
    {
        Rect playerRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 2);
        Debug.Log("Player rect: " + playerRect);
        return playerRect;
    }
}
