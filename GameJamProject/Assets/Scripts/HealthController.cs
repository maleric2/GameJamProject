using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    public float scoreValue;

    public float startingHealth = 100.0f;               
    public Slider healthSlider;                             
    public Image healthImage;                           
    public Color fullHealthColor = new Color(0.87f, 0.541f, 0, 1);
    public Color zeroHealthColor = new Color(0.4196f, 0.9647f, 1, 1);
    //public GameObject explosionPrefab;                new Color (0.4196f, 0.9647f, 1, 1);

    //private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
    //private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
    public float currentHealth;                      
    private bool isDead;

    Animator animator;
    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        //m_ExplosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();

        // Get a reference to the audio source on the instantiated prefab.
        //m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        // Disable the prefab so it can be activated when it's required.
        //m_ExplosionParticles.gameObject.SetActive(false);

        currentHealth = startingHealth;
        isDead = false;

        SetHealthUI();



        if (gameObject.GetComponent<Animator>() != null)
            animator = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        SetHealthUI();

        if (currentHealth <= 0.0f && !isDead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        healthSlider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        healthImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }
    public bool isFullHealth()
    {
        if (currentHealth >= 100) return true;
        else return false;
    }

    private void OnDeath()
    {
        GameManager.finalScore += scoreValue;

        if (gameObject.tag.Equals("HealBot"))
            GameManager.healBotKillCount += 1;
        if (gameObject.tag.Equals("FightBot"))
            GameManager.fightBotKillCount += 1;

        isDead = true;
        ApplyAnimations();

        if (gameObject.GetComponent<Chaser>())
        {
            gameObject.GetComponent<Chaser>().target = null;
        }
        if(gameObject.GetComponent<NavMeshAgent>() && gameObject.GetComponent<NavMeshAgent>().isOnNavMesh)
        {
			gameObject.GetComponent<NavMeshAgent>().Stop();
			gameObject.GetComponent<Rigidbody>().velocity= new Vector3(0,0,0);
		}
			

        
        // Move the instantiated explosion prefab to the tank's position and turn it on.
        //m_ExplosionParticles.transform.position = transform.position;
        //m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        //m_ExplosionParticles.Play();

        // Play the tank explosion sound effect.
        //m_ExplosionAudio.Play();

        // Turn the tank off.
        //gameObject.SetActive(false);
    }

    private void ApplyAnimations()
    {
        if (animator != null)
        {
            animator.SetBool("Dead", isDead);

            if(gameObject.tag.Equals("Player"))
                Invoke("Death", 2.0f);
            else
                Invoke("Death", 1.2f);
            //Invoke("Death", animator.GetCurrentAnimatorClipInfo(0)[0].clip.length+0.7f);
        }
    }
    private void Death()
    {
        if(gameObject.tag.Equals("Player"))
        GameManager.gm.gameState = GameManager.gameStates.GameOver;

        DestroyObject(this.gameObject);
        //this.gameObject.GetComponent<Collider>().isTrigger = true;
        //RigidbodyConstraints rgbConstraints = new RigidbodyConstraints();
        //this.gameObject.GetComponent<Rigidbody>().constraints= rgbConstraints;
        //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down);
    }

}
