using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private bool _isMarked;
    [SerializeField] private bool _isOnGround;
    
    public bool IsMarked => _isMarked;
    public bool IsOnGround => _isOnGround;
    
    public void Mark() => _isMarked = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Ground>())
           _isOnGround = true;
    }
}