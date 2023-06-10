using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerController : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = rbody.position;
        rbody.position += Vector2.left * speed * Time.fixedDeltaTime;
        rbody.MovePosition(pos);
        
    }
}
