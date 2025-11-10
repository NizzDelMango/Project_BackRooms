using UnityEngine;
using UnityEngine.AI; 

public class MonsterAI : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
      
        if (playerTransform != null)
        {

            agent.SetDestination(playerTransform.position);
        }
    }
}