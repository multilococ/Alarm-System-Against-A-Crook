using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Croock croock))
        {
            _alarmSystem.PlayAlarm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Croock croock))
        {
            _alarmSystem.StopAlarm();
        }
    }
}
