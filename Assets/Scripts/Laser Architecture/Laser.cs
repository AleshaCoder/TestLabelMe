using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class Laser : MonoBehaviour
{
    [SerializeField] private VolumetricLineBehavior _volumetricLine;
    [SerializeField] private LaserMovement _laserMovement;
    [SerializeField] private Intersection _intersection;
    [SerializeField] private int _reflectionsCount;

    private Camera _mainCamera;
    private List<VolumetricLineBehavior> _reflesiveVolumetricLines;
    private List<Intersection> _extraIntersections;

    private Vector3 _lastMousePosition;
    private RaycastHit _hitFromCamera;

    private void ReflectRay(RaycastHit hit, Vector3 direction, int count)
    {
        if (count <= 0)
        {
            return;
        }

        while (count != 0)
        {
            Vector3 startPosition = hit.point;
            direction = Vector3.Reflect(direction, hit.normal);
            Ray ray = new Ray(startPosition, direction);
            int index = _reflectionsCount - count;

            if (Physics.Raycast(ray, out hit))
            {
                _extraIntersections[index].gameObject.SetActive(true);
                _extraIntersections[index].transform.position = hit.point;
                _extraIntersections[index].transform.rotation = Quaternion.identity;
                _extraIntersections[index].transform.localRotation = Quaternion.Euler(90, 0, 0);

                _reflesiveVolumetricLines[index].gameObject.SetActive(true);
                _reflesiveVolumetricLines[index].transform.position = Vector3.zero;
                _reflesiveVolumetricLines[index].transform.rotation = Quaternion.identity;
                _reflesiveVolumetricLines[index].transform.localRotation = Quaternion.Euler(0, 0, 0);
                _reflesiveVolumetricLines[index].transform.localPosition = Vector3.zero;

                if (index == 0)
                {
                    _reflesiveVolumetricLines[index].StartPos = _intersection.transform.localPosition;
                }
                else
                {
                    _reflesiveVolumetricLines[index].StartPos = _extraIntersections[index - 1].transform.localPosition;
                }
                _reflesiveVolumetricLines[index].EndPos = _extraIntersections[index].transform.localPosition;
                count--;
                continue;
            }
            else
            {
                for (int i = index; i < _reflectionsCount; i++)
                {
                    _extraIntersections[index].gameObject.SetActive(false);
                    _reflesiveVolumetricLines[index].gameObject.SetActive(false);
                }
                break;
            }            
        }
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _reflesiveVolumetricLines = new List<VolumetricLineBehavior>();
        _extraIntersections = new List<Intersection>();
        for (int i = 0; i < _reflectionsCount; i++)
        {
            var newTarget = Instantiate(_intersection, Vector3.zero, Quaternion.identity, transform);
            newTarget.transform.localRotation = Quaternion.Euler(90, 0, 0);
            var newRay = Instantiate(_volumetricLine, Vector3.zero, Quaternion.identity, transform);
            newRay.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newRay.transform.localPosition = new Vector3(0, 0, 0);
            newRay.StartPos = new Vector3(0, 0, 0);
            newRay.EndPos = new Vector3(0, 0, 0);
            _reflesiveVolumetricLines.Add(newRay);
            _extraIntersections.Add(newTarget);
        }
    }

    private void Update()
    {
        if (_lastMousePosition != Input.mousePosition)
        {
            if (LaserRayCaster.TryGetHitFromCameraToMouse(_mainCamera, ref _hitFromCamera) == true)
            {
                _laserMovement.RotateTo(_hitFromCamera.point);
                _intersection.transform.position = _hitFromCamera.point;
                _volumetricLine.EndPos = _intersection.transform.localPosition;

                ReflectRay(_hitFromCamera, _hitFromCamera.point, _reflectionsCount);
            }
        }
    }
}
