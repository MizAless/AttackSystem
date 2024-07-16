using System;

public class AttackContainer
{
    private Attack _attack;

    public AttackContainer()
    {
        _attack = null;
    }

    public bool IsLast => _attack.NextAttack == null && _attack.NextPostpauseAttack == null;
    public bool IsEmpty => _attack == null;

    public Attack Attack => _attack;

    public event Action Cleared;

    public void SetAttack(Attack attack)
    {
        _attack = attack;
        _attack.Complited += ClearAttack;
    }

    public void ClearAttack()
    {
        _attack.Complited -= ClearAttack;
        _attack = null;
        Cleared?.Invoke();
    }
}
