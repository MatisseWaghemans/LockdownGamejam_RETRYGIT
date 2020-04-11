using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Agent : MonoBehaviour
{
    Collider2D agentCollider;

    private bool _isFilpped = false;
    public Collider2D AgentCollider { get { return agentCollider; } }
    private bool _hasSquashed = true;


    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agentCollider = GetComponent<Collider2D>();
        // agentCollider = GetComponent<Collider2D>();
    }



    public void Move(Vector2 velocity)
    {




        transform.forward = velocity;
        // transform.position += (Vector3)velocity * Time.deltaTime;

        // transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)velocity * Time.deltaTime, 0.5f);

        rb.MovePosition(transform.position + (Vector3)velocity * Time.deltaTime);



        //  if (_isFilpped && velocity.x > 0)
        //  {
        //      GetComponentInChildren<SpriteRenderer>().flipX = false;
        //      _isFilpped = false;
        //
        //  }
        //  if (!_isFilpped && velocity.x < 0)
        //  {
        //      GetComponentInChildren<SpriteRenderer>().flipX = true;
        //      _isFilpped = true;
        //  }
    }




}
