using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private ButtonInput _leftButton, _rightButton;
    [SerializeField] private float _speed;
    private int _horizontal;
    private int _touchesCount;

    private void OnRightUp()
    {
        _touchesCount--;
        _horizontal = 0;
    }

    private void OnRightDown()
    {
        if (_touchesCount == 0)
            _horizontal = 1;
        _touchesCount++;
        
    }

    private void OnLeftUp()
    {
        _touchesCount--;
        _horizontal = 0;
    }

    private void OnLeftDown()
    {
        if (_touchesCount == 0)
            _horizontal = -1;
        _touchesCount++;        
    }

    private void Awake()
    {
        _horizontal = 0;
        _touchesCount = 0;
        _leftButton.OnButtonDown += OnLeftDown;
        _leftButton.OnButtonUp += OnLeftUp;
        _rightButton.OnButtonDown += OnRightDown;
        _rightButton.OnButtonUp += OnRightUp;
    }

    private void Update()
    {
        if (_horizontal == 0)
        {
            return;
        }
        transform.Rotate(Vector3.up, _speed * _horizontal * Time.deltaTime);
    }
}
