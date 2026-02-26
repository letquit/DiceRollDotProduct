using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private Die[] _dice;
    
    private int _totalSum = 0;
    private bool _isRolling = false;
    private int _diceStillRolling;

    private void Start()
    {
        foreach (Die die in _dice)
        {
            die.OnDieStopped += OnDieStopped;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !_isRolling)
        {
            RollAllDice();
        }
    }

    private void RollAllDice()
    {
        _isRolling = true;
        _diceStillRolling = _dice.Length;
        _totalSum = 0;
        
        foreach (Die die in _dice)
        {
            die.RollDice();
        }
    }

    private void OnDieStopped(int result)
    {
        _totalSum += result;
        _diceStillRolling--;
        
        if (_diceStillRolling != 0) return;
        _isRolling = false;
        Debug.Log($"Sum: {_totalSum}");
    }
}
