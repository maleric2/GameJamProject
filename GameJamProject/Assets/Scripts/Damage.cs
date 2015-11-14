using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{

    public float damageAmount = 10.0f;

    public bool damageOnTrigger = true;
    public bool damageOnCollision = false;
    public bool continuousDamage = false;
    public float continuousTimeBetweenHits = 0;

    public bool destroySelfOnImpact = false;
    public float delayBeforeDestroy = 0.0f;
    public GameObject explosionPrefab;

    private float savedTime = 0;

    Animator animator;
    bool damageInAction = false;

    void Start()
    {
        if (gameObject.GetComponent<Animator>() != null)
            animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //ApplyAnimations(damageInAction);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            ApplyAnimations(true);
            Debug.Log("OnTriggerEnter");
        }
        if (damageOnTrigger)
        {
            if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")
                return;

            if (collision.gameObject.GetComponent<HealthController>() != null)
            {
                Debug.Log("OnTriggerEnter");
                collision.gameObject.GetComponent<HealthController>().TakeDamage(damageAmount);

                if (destroySelfOnImpact)
                {
                    Destroy(gameObject, delayBeforeDestroy);
                }

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (damageOnCollision)
        {
            if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")
                return;

            if (this.tag.Equals(collision.gameObject.tag))
                return;

            if (collision.gameObject.GetComponent<HealthController>() != null)
            {
                Debug.Log("OnCollisionEnter");
                ApplyAnimations(true);
                collision.gameObject.GetComponent<HealthController>().TakeDamage(damageAmount);


                if (destroySelfOnImpact)
                {
                    Destroy(gameObject, delayBeforeDestroy);
                }

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                }
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            ApplyAnimations(false);
    }
    void OnCollisionExit(Collision collision)
    {
        if (damageOnCollision)
        {
            if (collision.gameObject.GetComponent<HealthController>() != null)
            {
                Debug.Log("OnCollisionExit");
                ApplyAnimations(false);
            }
        }
    }

    void OnCollisionStay(Collision collision) // this is used for damage over time things
    {
        if (continuousDamage)
        {
            if ((collision.gameObject.tag == "Target" || collision.gameObject.tag == "Player") && collision.gameObject.GetComponent<HealthController>() != null)
            {   // is only triggered if whatever it hits is the player
                if (Time.time - savedTime >= continuousTimeBetweenHits)
                {
                    ApplyAnimations(true);
                    savedTime = Time.time;
                    collision.gameObject.GetComponent<HealthController>().TakeDamage(damageAmount);
                }
            }
        }
    }


    private void ApplyAnimations(bool attack)
    {
        if (animator != null)
        {
            animator.SetBool("Attack", attack);
        }
    }


}