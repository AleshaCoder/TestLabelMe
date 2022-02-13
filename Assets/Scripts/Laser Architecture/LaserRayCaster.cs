using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayCaster : MonoBehaviour
{
    public static bool TryGetHit(Vector3 startPoint, Vector3 rayDirection, ref RaycastHit hit)
    {
        Ray ray = new Ray(startPoint, rayDirection);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            return true;
        }
        return false;
    }

    public static bool TryGetHitFromCameraToMouse(Camera camera, ref RaycastHit hit)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            return true;
        }
        return false;
    }
}
