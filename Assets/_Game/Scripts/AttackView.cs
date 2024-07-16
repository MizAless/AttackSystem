using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(TimeZoneChanger))]
public class AttackView : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _attackName;

    private char _timeSeparator = ':';
    private Attack _attack;
    private TimeZoneChanger _timeZoneChanger;

    private void Awake()
    {
        _attack = GetComponent<Attack>();
        _timeZoneChanger = GetComponent<TimeZoneChanger>();
        _timeZoneChanger.SetAttack(_attack);
    }

    private void Start()
    {
        UpdateAttackName();
    }

    public void ChangeAttack(Attack attack)
    {
        _attack.CurrentAttackTimeChanged -= UpdateAttackView;
        _attack = attack;
        UpdateAttackName();
        _timeZoneChanger.SetAttack(_attack);
        _timeZoneChanger.ChangeTimeZones();
        _attack.CurrentAttackTimeChanged += UpdateAttackView;
    }

    private void OnEnable()
    {
        _attack.CurrentAttackTimeChanged += UpdateAttackView;
    }

    private void OnDisable()
    {
        _attack.CurrentAttackTimeChanged -= UpdateAttackView;
    }

    private void UpdateAttackName()
    {
        _attackName.text = _attack.Name;
    }

    private void UpdateAttackView(float currentAttackTime)
    {
        int secondsToMilliseconds = 100;
        int millisecondsCountForFromat = 10;
        string additionalFormat = "0";

        _timeSlider.value = currentAttackTime / _attack.AttackTime;

        int seconds = (int)currentAttackTime;
        int milliseconds = (int)((currentAttackTime - seconds) * secondsToMilliseconds);

        string millisecondsString = milliseconds.ToString();

        if (milliseconds < millisecondsCountForFromat)
            millisecondsString = additionalFormat + millisecondsString;

        _timeText.text = seconds.ToString() + _timeSeparator.ToString() + millisecondsString;
    }
}
