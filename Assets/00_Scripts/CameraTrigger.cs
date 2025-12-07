using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTrigger : MonoBehaviour
{
    public Animator Camera;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Camera != null)
            {
                Camera.SetTrigger("CameraTrigger");
            }
        }
    }
}