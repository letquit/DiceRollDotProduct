using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Die : MonoBehaviour
{
    public Action<int> OnDieStopped;
    
    [SerializeField] private Transform[] _diceSides;
    [SerializeField] private float _force = 5f;
    [SerializeField] private float _torque = 5f;
    
    private Rigidbody _rigidbody;
    private bool _isRolling = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping() && _isRolling)
        {
            int result = GetSideFacingUp();
            OnDieStopped.Invoke(result);
        }
    }

    public void RollDice()
    {
        Vector3 force = new Vector3(0f, _force, 0f);
        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _torque;
        
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _rigidbody.AddTorque(torque, ForceMode.Impulse);
        
        _isRolling = true;
    }

    private int GetSideFacingUp()
    {
        Transform upSide = null;
        float maxDot = -1;

        foreach (Transform side in _diceSides)
        {
            float dot = Vector3.Dot(side.up, Vector3.up);
            
            if (!(dot > maxDot)) continue;
            maxDot = dot;
            upSide = side;
        }
        
        _isRolling = false;
        
        if (upSide != null) return int.Parse(upSide.name);
        return 0;
    }
}
