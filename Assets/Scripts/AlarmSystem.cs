using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSource;

    WaitForSeconds _waitSondsForChangingVolume;

    private float _minAlarmVolume = 0f;
    private float _maxAlarmVolume = 1.0f;
    private float _alarmVolumeDelta = 0.01f;
    private float _alarmVolumeDelay = 0.1f;

    private bool _isAlarmEnabled;

    private void Start()
    {
        _isAlarmEnabled = false;
        _alarmSource = GetComponent<AudioSource>();
        _alarmSource.Stop();
        _alarmSource.volume = _minAlarmVolume;
        _waitSondsForChangingVolume = new WaitForSeconds(_alarmVolumeDelay);
    }

    public void PlayAlarm()
    {
        _alarmSource.Play();
        _isAlarmEnabled = true;
        StartCoroutine(ChangeAlarmVolume(_maxAlarmVolume));
    }

    public void StopAlarm()
    {
        _isAlarmEnabled = false;
        StartCoroutine(ChangeAlarmVolume(_minAlarmVolume));
    }

    private IEnumerator ChangeAlarmVolume(float targetValue)
    {
        bool isCurrentAlarmState = _isAlarmEnabled;

            while (_alarmSource.volume != targetValue && isCurrentAlarmState == _isAlarmEnabled)
            {
                _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, targetValue, _alarmVolumeDelta);

                yield return _waitSondsForChangingVolume;
            }

        if (isCurrentAlarmState == false)
        {
            _alarmSource.Stop();
        }
    }
}