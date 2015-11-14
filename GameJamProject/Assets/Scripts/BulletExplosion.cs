using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour {

    public LayerMask enemyMask;                        // Enemies layer
    public ParticleSystem explosionParticles;         
    public AudioSource explosionAudio;              
    public float maxDamage = 100.0f;                    
    public float explosionForce = 1000.0f;              
    public float maxLifeTime = 2.0f;                    
    public float explosionRadius = 5.0f;               

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            HealthController targetHealth = targetRigidbody.GetComponent<HealthController>();

            if (!targetHealth)
                continue;

            float damageAmount = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damageAmount);
        }

        // Unparent the particles from the shell.
        explosionParticles.transform.parent = null;

        // Play the particle system.
        explosionParticles.Play();

        // Play the explosion sound effect.
        explosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        Destroy(explosionParticles.gameObject, explosionParticles.duration);

        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
