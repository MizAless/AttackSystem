using System;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private string _name;

    [SerializeField, Min(0)] private float _attackTime;
    [SerializeField, Min(0)] private float _disapproveAttackTime;
    [SerializeField, Min(0)] private float _approveAttackTime;

    [SerializeField] private bool _isPauseAttack;
    [SerializeField, Min(0)] private float _approvePostpauseAttackTime;

    [SerializeField] private Attack _nextAttack;
    [SerializeField] private Attack _nextPostpauseAttack;

    public string Name => _name;

    public Attack NextAttack => _nextAttack;
    public Attack NextPostpauseAttack => _nextPostpauseAttack;

    private float _currentAttackTime = 0;
    public float AttackTime => _attackTime;
    public float DisapproveAttackTime => _disapproveAttackTime;
    public float ApproveAttackTime => _approveAttackTime;
    public float ApprovePostpauseAttackTime => _approvePostpauseAttackTime;
    public float CurrentAttackTime => _currentAttackTime;

    public bool IsApproveAttackTime => _currentAttackTime > _disapproveAttackTime && _currentAttackTime <= _disapproveAttackTime + _approveAttackTime;
    public bool IsApprovePostpauseAttackTime => _currentAttackTime > _disapproveAttackTime + _approveAttackTime;

    public event Action<float> CurrentAttackTimeChanged;
    public event Action Complited;
    public event Action Started;
    public event Action AttackOptionsChanged;

    private void OnValidate()
    {
        if (_isPauseAttack == false)
        {
            _approvePostpauseAttackTime = 0;
            _nextPostpauseAttack = null;
        }

        if (_disapproveAttackTime > _attackTime)
        {
            _disapproveAttackTime = _attackTime;
        }

        if (_disapproveAttackTime + _approveAttackTime > _attackTime)
        {
            _approveAttackTime = _attackTime - _disapproveAttackTime;
        }
        
        if (_disapproveAttackTime + _approveAttackTime + _approvePostpauseAttackTime > _attackTime)
        {
            _approvePostpauseAttackTime = _attackTime - _disapproveAttackTime - _approveAttackTime;
        }

        AttackOptionsChanged?.Invoke();
    }

    public void StartAttack()
    {
        StartCoroutine(Attacking());
        Started?.Invoke();
    }

    public void SetNextPostpauseAttack(Attack attack)
    {
        _nextPostpauseAttack = attack;
    }

    private IEnumerator Attacking()
    {
        var frameDelay = new WaitForFixedUpdate();

        _currentAttackTime = 0;

        while (_currentAttackTime < _attackTime)
        {
            _currentAttackTime += Time.fixedDeltaTime;

            if (_currentAttackTime > _attackTime)
                _currentAttackTime = _attackTime;

            CurrentAttackTimeChanged?.Invoke(_currentAttackTime);

            yield return frameDelay;
        }

        _currentAttackTime = 0;
        CurrentAttackTimeChanged?.Invoke(_currentAttackTime);
        Complited?.Invoke();
    }
}
