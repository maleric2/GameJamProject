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
        Move();
        

        if (target.GetComponent<HealthController>().isFullHealth())
            FindNewTarget();
        ApplyAnimations();

		/*if (gameObject.GetComponent<HealthController> ().currentHealth <= 0) {
			gameObject.GetComponent<NavMeshAgent>().speed = 0;
		}*/
    }
    void Move()
    {
        //NAVMESH
        /*gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
        */

        transform.LookAt(target);

        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);

        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if (distance > 1.2f)
            transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void FindNewTarget()
    {
        WaveManager.wm.GetRandomTarget();
        SetTarget(WaveManager.wm.GetWaveTarget());
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