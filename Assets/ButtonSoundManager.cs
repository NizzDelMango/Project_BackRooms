using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    //  재생할 사운드 클립
    public AudioClip hoverSound;

    // 사운드를 재생할 AudioSource
    private AudioSource audioSource;

    void Start()
    {
        // 씬에서 AudioSource 컴포넌트를 찾거나 추가합니다.
        // 일반적으로 씬에 AudioSource 컴포넌트가 있는 빈 오브젝트를 사용하는 것이 좋습니다.
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // 만약 오브젝트에 AudioSource가 없다면 추가합니다.
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // EventTrigger에서 호출할 함수
    public void PlayHoverSound()
    {
        if (audioSource != null && hoverSound != null)
        {
            // 현재 재생 중인 사운드가 없다면 재생 (옵션)
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(hoverSound);
            }
        }
    }
}