using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

    public Rigidbody bulletPrefab;                   
    public Transform fireTransformPoint;          
    public Slider aimSlider;                
    public AudioSource shootingAudioSource;
    public AudioClip chargingClip;            
    public AudioClip fireClip;                
    public float minLaunchForce = 15.0f;        
    public float maxLaunchForce = 30.0f;        
    public float maxChargeTime = 0.75f;       

    private float currentLaunchForce;         
    private float chargeSpeed;                
    private bool isFired;                   

    private void Start()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }


    private void Update()
    {
        aimSlider.value = minLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (currentLaunchForce >= maxLaunchForce && !isFired)
        {
            // ... use the max force and launch the shell.
            currentLaunchForce = maxLaunchForce;
            Fire();

            // If it plays charge afterwards, it never plays the Fire clip!
            //shootingAudioSource.clip = chargingClip;
            //shootingAudioSource.Play();

            isFired = false;
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetMouseButtonDown(0))
        {
            // ... reset the fired flag and reset the launch force.
            isFired = false;
            currentLaunchForce = minLaunchForce;

            // Change the clip to the charging clip and start it playing.
            shootingAudioSource.clip = chargingClip;
            shootingAudioSource.Play();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetMouseButton(0) && !isFired)
        {
            // Increment the launch force and update the slider.
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            aimSlider.value = currentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetMouseButtonUp(0) && !isFired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        isFired = true;

        Rigidbody shellInstance = (Rigidbody)Instantiate(bulletPrefab, fireTransformPoint.position, fireTransformPoint.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = currentLaunchForce * fireTransformPoint.forward; ;

        // Change the clip to the firing clip and play it.
        shootingAudioSource.clip = fireClip;
        shootingAudioSource.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        currentLaunchForce = minLaunchForce;
    }
}
