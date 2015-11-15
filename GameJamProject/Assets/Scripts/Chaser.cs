using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour
{

    public float speed = 20.0f;
    public bool followPlayer = false;
    public Transform target;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        // if no target specified, assume the player
        if (target == null)
        {

            if (!followPlayer && GameObject.FindWithTag("Target") != null)
            {
                //TODO choose target
                target = GameObject.FindWithTag("Target").GetComponent<Transform>();
            }
            else if (followPlayer)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
        if(gameObject.GetComponent<Animator>() != null)
            animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //WITHOUT navmesh
        if (target == null)
            return;

        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
        gameObject.GetComponent<NavMeshAgent>().speed = speed;

        if (target.GetComponent<HealthController>().isFullHealth())
            FindNewTarget();
        ApplyAnimations();
    }
    private void FindNewTarget()
    {
        SetTarget(GameManager.gm.GetRandomTarget());
    }
    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        if (!followPlayer)
            target = newTarget;
    }
    private void ApplyAnimations()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", gameObject.GetComponent<NavMeshAgent>().velocity.magnitude);
        }
    }
}