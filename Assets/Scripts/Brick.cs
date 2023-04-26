using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    private Animator anim;
    public int PointValue;

    void Start()
    {
        anim = GetComponent<Animator>();
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        anim.SetTrigger("death");
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        
        Destroy(gameObject, 0.5f);
    }
}
