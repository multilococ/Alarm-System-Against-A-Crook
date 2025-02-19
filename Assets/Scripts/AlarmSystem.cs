using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSource;

    private WaitForSeconds _waitSondsForChangingVolume;

    private float _minAlarmVolume = 0f;
    private float _maxAlarmVolume = 1.0f;
    private float _alarmVolumeDelta = 0.01f;
    private float _alarmVolumeDelay = 0.1f;

    private Coroutine _alarmCurotine;

    private void Awake()
    {
        _alarmSource = GetComponent<AudioSource>();
        _alarmSource.Stop();
        _alarmSource.volume = _minAlarmVolume;
        _waitSondsForChangingVolume = new WaitForSeconds(_alarmVolumeDelay);
    }

    public void PlayAlarm()
    {
        if (_alarmCurotine != null)
            StopCoroutine(_alarmCurotine);

        _alarmSource.Play();
        _alarmCurotine = StartCoroutine(ChangeAlarmVolume(_maxAlarmVolume));
    }

    public void StopAlarm()
    {
        if (_alarmCurotine != null)
            StopCoroutine(_alarmCurotine);

        _alarmCurotine = StartCoroutine(ChangeAlarmVolume(_minAlarmVolume));
    }

    private IEnumerator ChangeAlarmVolume(float targetValue)
    {
        while (_alarmSource.volume != targetValue)
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, targetValue, _alarmVolumeDelta);

            yield return _waitSondsForChangingVolume;
        }

        if (_alarmSource.volume == _minAlarmVolume)
        {
            _alarmSource.Stop();
        }
    }
}