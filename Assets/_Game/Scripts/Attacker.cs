using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private Attack _startAttack;
    [SerializeField] private AttackView _currentAttackView;

    private InputReader _inputReader;

    private AttackContainer _currentAttack;
    private AttackContainer _nextAttack;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _currentAttack = new AttackContainer();
        _nextAttack = new AttackContainer();
    }

    private void OnEnable()
    {
        _inputReader.Attacked += Attack;
        _currentAttack.Cleared += TrySetNextAttack;
    }

    private void OnDisable()
    {
        _inputReader.Attacked -= Attack;
        _currentAttack.Cleared -= TrySetNextAttack;
    }

    private void Attack()
    {
        if (_currentAttack.IsEmpty)
        {
            _currentAttack.SetAttack(_startAttack);
            _currentAttack.Attack.StartAttack();
            _currentAttackView.ChangeAttack(_currentAttack.Attack);
        }
        else if (_nextAttack.IsEmpty)
        {
            if (_currentAttack.IsLast)
            {
                if (_currentAttack.Attack.IsApproveAttackTime)
                {
                    _nextAttack.SetAttack(_startAttack);
                }
            }
            else if (_currentAttack.Attack.IsApproveAttackTime)
            {
                _nextAttack.SetAttack(_currentAttack.Attack.NextAttack);
            }
            else if (_currentAttack.Attack.IsApprovePostpauseAttackTime)
            {
                _nextAttack.SetAttack(_currentAttack.Attack.NextPostpauseAttack);
            }
        }

        //if (_nextAttack.IsEmpty == false)
        //    print($"Next attack name: {_nextAttack.Attack.Name}");
    }

    private void TrySetNextAttack()
    {
        if (_nextAttack.IsEmpty)
            return;

        _currentAttack.SetAttack(_nextAttack.Attack);
        _currentAttackView.ChangeAttack(_currentAttack.Attack);
        _nextAttack.ClearAttack();
        _currentAttack.Attack.StartAttack();
    }
}
