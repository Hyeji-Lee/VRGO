using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f; //distance that gun can reach


    float timer; // only attack when time is right
    Ray shootRay; // to know what it is hit 
    RaycastHit shootHit; //return back to us whatever we've hit 
    int shootableMask; // to make sure we only hit shootable things 
    ParticleSystem gunParticles; 
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f; 


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop (); // prevent replaying particles already exist 
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position); // This is the first point. We have to Calculate second point.

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward; // z axis plus number is considered forward 

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) //when hit, draw line to that point
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null) //if we hit wall or table, then no enemyHealth there.
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point); //second point 
        }
		else//when not, draw really long line.
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
