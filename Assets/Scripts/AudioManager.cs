using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public async UniTask PlayAudioClip(List<AudioClip> clipList, CancellationToken ct)
    {
        foreach (AudioClip clip in clipList)
        {
            PlayAudioClip(clip);
            await UniTask.WaitForSeconds(clip.length, cancellationToken : ct);

            if (ct.IsCancellationRequested)
            {
                StopAudioClip();
                break;
            }
        }
    }

    private void StopAudioClip()
    {
        audioSource.Stop();
    }
}
