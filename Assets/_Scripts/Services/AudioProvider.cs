using RB.Services.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioProvider : MonoBehaviour
{
    private AudioService _audioService;

    [SerializeField] private AudioClip _levelOneSobriety;
    [SerializeField] private AudioClip _levelTwoSobriety;
    [SerializeField] private AudioClip _levelThreeSobriety;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _audioService = AudioService.Instance;
    }
    

    private void OnEnable()
    {
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            _audioService.PlayMusic(_levelOneSobriety);
        }
    }

    private void OnSobrietyChanged(SobrietyLevel sobrietyLevel)
    {
        switch (sobrietyLevel)
        {
            case SobrietyLevel.Sober:
                _audioService.PlayMusic(_levelOneSobriety);
                break;
            case SobrietyLevel.Drunk:
                _audioService.PlayMusic(_levelTwoSobriety);
                break;
            case SobrietyLevel.DrunkAsHell:
                _audioService.PlayMusic(_levelThreeSobriety);
                break;
        }
    }

    private void OnDisable()
    {
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

