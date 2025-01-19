using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class BaseObstacle : MonoBehaviour
{
    private Rigidbody2D _body;
    private BoxCollider2D _collider;

    // Use this for initialization
    void Start()
    {
        this._body = GetComponent<Rigidbody2D>();
        this._collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            this._body.bodyType = RigidbodyType2D.Static;
            this._collider.isTrigger = true;
            Debug.Log("Collided with player");
        }
    }
}
