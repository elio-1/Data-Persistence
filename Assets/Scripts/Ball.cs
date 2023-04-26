using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float maxBallVelocity = 4.5f;
    private float m_ballAcceleration = 0.02f;
    private float m_ballAccelerationRaise = 0.004f;
    [SerializeField] private float m_defaultBallAcceleration = 0.02f;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        //Raise the score multiplier and speed ball after each collisions with bricks
        if (other.gameObject.tag == "Brick"){
            //after a collision we accelerate a bit
            velocity += velocity.normalized * m_ballAcceleration;
            m_ballAcceleration += m_ballAccelerationRaise;
            MainManager.ScoreMultiplier += .5f;
        }
        // if the ball hit the Paddle
        if (other.gameObject.tag == "Paddle"){
            m_ballAcceleration = m_defaultBallAcceleration;
            MainManager.ScoreMultiplier = MainManager.DefaultScoreMultiplier;
            velocity = new Vector3(velocity.x, 2, 0);
        }
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > maxBallVelocity)
        {
            velocity = velocity.normalized * maxBallVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }


}
