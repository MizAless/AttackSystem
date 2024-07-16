using UnityEngine;

[ExecuteInEditMode]
public class TimeZoneChanger : MonoBehaviour
{
    [SerializeField] private RectTransform _timeZonesCotainer;
    [SerializeField] private RectTransform _disapproveZone;
    [SerializeField] private RectTransform _approveZone;
    [SerializeField] private RectTransform _postpauseApproveZone;

    [SerializeField] private Attack _attack;

    private void OnEnable()
    {
        _attack.AttackOptionsChanged += ChangeTimeZones;
    }

    private void OnDisable()
    {
        _attack.AttackOptionsChanged -= ChangeTimeZones;
    }

    public void SetAttack(Attack attack)
    {
        _attack = attack;
    }

    public void ChangeTimeZones()
    {
        float zonesWidth = _timeZonesCotainer.rect.width;

        _disapproveZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _attack.DisapproveAttackTime / _attack.AttackTime * zonesWidth);
        _approveZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _attack.ApproveAttackTime / _attack.AttackTime * zonesWidth);
        _postpauseApproveZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _attack.ApprovePostpauseAttackTime / _attack.AttackTime * zonesWidth);
    }
}
