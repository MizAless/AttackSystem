using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode AttackKey = KeyCode.Space;

    public event Action Attacked;

    private void Update()
    {
        if (Input.GetKeyDown(AttackKey))
            Attacked?.Invoke();
    }
}
