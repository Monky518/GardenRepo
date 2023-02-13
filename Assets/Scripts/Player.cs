using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xRange;
    public float speed;

    void Start()
    {
        playerRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 4);
    }

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

    void PlayerRect()
    {
        rect playerRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 4);
        return playerRect;
    }
}
