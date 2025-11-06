using UnityEngine;

public class SlideController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        FirstPersonController playerScript = other.GetComponent<FirstPersonController>();

        if (playerScript != null)
        {
            // 플레이어 조작 스크립트를 끕니다.
            playerScript.enabled = false;
        }
    }

}