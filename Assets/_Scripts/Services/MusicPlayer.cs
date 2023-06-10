using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace RB.Services.Audio
{
    public class MusicPlayer
	{
        private readonly string _volumeTag;
        private readonly string _generalVolumeTag;
        private readonly AudioService _audioService;
        private readonly AudioSource _audioSource;
        private readonly AudioSource _audioSource2;
        private bool _isaudiosourse1Playing = false;
        private AudioSource ActiveSource =>  _isaudiosourse1Playing ? _audioSource : _audioSource2;
        private AudioSource InactiveSource => _isaudiosourse1Playing ? _audioSource2 : _audioSource;

        public MusicPlayer (string volumeTag, string generalVolumeTag, AudioService audioService, AudioSource audioSource,AudioSource audioSource2)
        {
            _volumeTag = volumeTag;
            _generalVolumeTag = generalVolumeTag;
            _audioService = audioService;
            _audioSource = audioSource;
            _audioSource2 = audioSource2;
            UpdateVolumeLevel();
        }
        public void PlayMusic(AudioClip clip, bool playWithoutFade = false)
        {

            if (ActiveSource.clip == clip) return;
            float currentvolume = PlayerPrefs.GetFloat(_volumeTag, 1f) * MuteModifier();
            var time = ActiveSource.timeSamples;
            ActiveSource.DOFade(0f, _audioService.FadeSwitchingDuration);
            _isaudiosourse1Playing = !_isaudiosourse1Playing;
            ActiveSource.clip = clip;
            ActiveSource.Play();
            ActiveSource.timeSamples = time;
            ActiveSource.DOFade(currentvolume, _audioService.FadeSwitchingDuration);


            //if (_audioService.FadeOnSwitchMusic && playWithoutFade == false)
            //{
            //    _audioService.StartCoroutine(FadingSwitchMusic(_audioService.FadeSwitchingDuration, clip));
            //}
            //else
            //{
            //    PlayWithOneTime(clip);
            //}
        }
        public void PauseMusic() => _audioSource.Pause();
        public void UnpauseMusic() => _audioSource.UnPause();


        private IEnumerator FadingSwitchMusic(float duration, AudioClip clip)
        {



            float currentTime = 0;
            float start = PlayerPrefs.GetFloat(_volumeTag, 1f) * MuteModifier();
            while (currentTime < duration / 2)
            {
                currentTime += Time.unscaledDeltaTime;
                _audioSource.volume = Mathf.Lerp(start, 0f, currentTime / (duration / 2));
                yield return null;
            }
            PlayWithOneTime(clip);
            currentTime = 0;
            while (currentTime < duration / 2)
            {
                currentTime += Time.unscaledDeltaTime;
                _audioSource.volume = Mathf.Lerp(0f, start, currentTime / (duration / 2));
                yield return null;
            }
        }

        public void SetVolumeLevel(float value)
        {
            PlayerPrefs.SetFloat(_volumeTag, value);
            ActiveSource.volume = value * PlayerPrefs.GetFloat(_generalVolumeTag, 1f) * MuteModifier();
            InactiveSource.volume = 0f;
        }
        public void UpdateVolumeLevel()
        {
            ActiveSource.volume = PlayerPrefs.GetFloat(_volumeTag, 1f) * PlayerPrefs.GetFloat(_generalVolumeTag, 1f) * MuteModifier();
            InactiveSource.volume = 0f;
        }

        private int MuteModifier()
        {
            return _audioService.IsMuted ? 0 : 1;
        }
        private void PlayWithOneTime(AudioClip clip)
        {
            var time = _audioSource.timeSamples;
            _audioSource.clip = clip;
            _audioSource.Play();
            _audioSource.timeSamples = time;
        }
    } 
}
