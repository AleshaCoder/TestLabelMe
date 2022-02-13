using UnityEngine;

[RequireComponent(typeof(MeshRenderer))] 
[RequireComponent(typeof(SphereCollider))] 
public class Intersection : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private SphereCollider _collider;

    private void SwitchOn()
    {
        _meshRenderer.enabled = true;
    }

    private void SwitchOff()
    {
        _meshRenderer.enabled = false;
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _meshRenderer.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }    
}
