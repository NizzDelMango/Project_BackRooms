using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCollider : MonoBehaviour
{
    public string startSceneName = "Start";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            Debug.Log("플레이어 엔딩 씬");
            SceneManager.LoadScene(startSceneName);
        }
    }
}
