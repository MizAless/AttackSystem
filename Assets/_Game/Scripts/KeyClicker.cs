using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class KeyClicker : MonoBehaviour
{
    private const string IsAttacked = nameof(IsAttacked);

    [SerializeField] private Animator _animator;

    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.Attacked += Click;
    }

    private void OnDisable()
    {
        _inputReader.Attacked -= Click;
    }

    private void Click()
    {
        _animator.SetTrigger(IsAttacked);
    }
}
