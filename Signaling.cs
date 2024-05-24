using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private float _volumeIncreasingSpeed;
    [SerializeField] private EnterDetector _detector;

    private readonly float _maxVolume = 1.0f;
    private readonly float _minVolume = 0.0f;
    private readonly float _increasingTimeoutDuration = 0.1f;

    private AudioSource _audioSource;
    private WaitForSeconds _timeBetweenIncreasingDuration;
    private Coroutine _changeCoroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _timeBetweenIncreasingDuration = new WaitForSeconds(_increasingTimeoutDuration);
    }

    private void StartChanging(float finishValue)
    {
        if (_changeCoroutine != null)
            StopCoroutine(ChangeVolume(finishValue));

        _changeCoroutine = StartCoroutine(ChangeVolume(finishValue));
    }

    private IEnumerator ChangeVolume(float finishValue)
    {
        Debug.Log("Start changing");

        if (!_audioSource.isPlaying)
            _audioSource.Play();

        while (_audioSource.volume != finishValue)
        {
            _audioSource.volume = 
            Mathf.MoveTowards(_audioSource.volume, finishValue, _volumeIncreasingSpeed * Time.deltaTime);
            Debug.Log(_audioSource.volume);
            yield return _timeBetweenIncreasingDuration;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();

        Debug.Log("Finish changing");
    }

    public void ActivateSignaling(bool isThiefInside)
    {
        if (isThiefInside == true)
            StartChanging(_maxVolume);
        if (isThiefInside == false)
            StartChanging(_minVolume);
    }
}
