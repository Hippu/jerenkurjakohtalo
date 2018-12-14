using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public AudioClip explosionSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var rb = GetComponent<Rigidbody2D>();
        rb.position += new Vector2(horizontal * speed * Time.deltaTime, 0);

        var touches = Input.touches;
        if (touches.Count() == 1)
        {
            var touch = touches.First();
            var screenCenterX = Screen.width / 2f;
            var touchHorizontal = Mathf.Sign(touch.position.x - screenCenterX);
            rb.position += new Vector2(touchHorizontal * speed * Time.deltaTime, 0);
        }

        var renderer = GetComponent<SpriteRenderer>();
        if (horizontal > 0)
        {
            renderer.flipX = true;
        }
        if (horizontal < 0)
        {
            renderer.flipX = false;
        }
    }

    public void GetHit()
    {
        var source = GetComponent<AudioSource>();
        source.clip = explosionSound;
        source.Play();
        transform.localScale *= 0.8f;
    }


}
