using UnityEngine;

public class Box : MonoBehaviour
{
   private bool _isMarked;
     private bool _isOnGround;

    public bool IsMarked => _isMarked;
    public bool IsOnGround => _isOnGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
            _isOnGround = true;
    }

    public void Mark() => _isMarked = true;

    public Box Load(Transform target)
    {
        transform.position = target.position;
        transform.SetParent(target);

        return this;
    }

    public void Unload()
    {
        Destroy(gameObject);
    }
}