using UnityEngine;

public class LaserMovement : MonoBehaviour
{  
    public void RotateTo(Vector3 point)
    {
        var direction = point - transform.position;
        gameObject.transform.up = direction;
    }
}
