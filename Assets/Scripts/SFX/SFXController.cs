using UnityEngine;
using System.Collections;

using Asteroids.Utils;

namespace Asteroids.SFX
{
    public class SFXController : MonoBehaviour, IGameService
    {
        private GameObject _audioSourcePrefab;
        private IEnumerator _audioCompletionCoroutine;

        private const string audioSourcePath = "SFX/AudioSource";

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void InitService()
        {
            _audioSourcePrefab = Resources.Load<GameObject>(audioSourcePath);
        }

        public void EndService()
        {
            if (_audioCompletionCoroutine != null)
            {
                StopCoroutine(_audioCompletionCoroutine);
                _audioCompletionCoroutine = null;
            }
        }

        public void PlaySfx(string clipName)
        {
            var clip = Resources.Load<AudioClip>("SFX/" + clipName);
            var source = ObjectPool.Spawn(_audioSourcePrefab, transform.position, Quaternion.identity);
            source.transform.SetParent(transform);

            var audioSource = source.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();

            if (_audioCompletionCoroutine != null)
            {
                StopCoroutine(_audioCompletionCoroutine);
                _audioCompletionCoroutine = null;
            }

            _audioCompletionCoroutine = WaitForAudioCompletion(audioSource);
            StartCoroutine(_audioCompletionCoroutine);
        }

        private IEnumerator WaitForAudioCompletion(AudioSource audioSource)
        {
            //Wait until the audio finishes playing
            yield return new WaitForSeconds(audioSource.clip.length);

            ObjectPool.Despawn(audioSource.gameObject);
        }
    }
}
