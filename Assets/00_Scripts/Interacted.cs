using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacted : MonoBehaviour, IInteractable
{
   public void Interact()
    {
        Debug.Log("Interacted");
    }
}
