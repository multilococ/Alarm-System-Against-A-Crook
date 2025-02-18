using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSource;

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
        _isAlarmEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Croock croock))
        {
            _isAlarmEnabled = true;
            StartCoroutine(IncreaseAlarmVolume(_alarmVolumeDelay));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Croock croock))
        {
            _isAlarmEnabled = false;
            StartCoroutine(DecreaseAlarmVolume(_alarmVolumeDelay));
        }
    }
    
    private IEnumerator IncreaseAlarmVolume(float alarmTimeDelay)
    {
        _alarmSource?.Play();

        WaitForSeconds waitForSeconds = new WaitForSeconds(alarmTimeDelay);

        while (_isAlarmEnabled == true && _alarmSource.volume < _maxAlarmVolume)
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, _maxAlarmVolume,_alarmVolumeDelta);

            yield return waitForSeconds;
        }
    }

    private IEnumerator DecreaseAlarmVolume(float alarmTimeDelay) 
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(alarmTimeDelay);

        while (_alarmSource.volume > _minAlarmVolume)
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, _minAlarmVolume, _alarmVolumeDelta);

            yield return waitForSeconds;
        }

        _alarmSource?.Stop();
    }
}
