using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShooter : MonoBehaviour
{
    [Header("Components")]
    public Transform ProjectileShooter;
    public GameObject ProjectilePrefab;
    public GameObject Hypershoot;
    public Animator animator;
    [SerializeField]
    private Player player;
    //public audioManager audioManager;

    [Header("Bullet")]
    public float projectileSpeed;
    [SerializeField]
    private float TimeLastShot;
    [SerializeField]
    private float shotNumber;
    public float shotMaxNumber = 3;
    private bool facingRight = true;
    private float Facing;
    private int shots;

    bool isShooting;

    void Start()
    {
        InvokeRepeating("Wait", 0f, .5f);
        shots = 0;
    }

    void Update()
    {
        TimeLastShot += Time.deltaTime;

        if(player.isDashing == false && player.dead == false && GetComponentInParent<Pause>().isPaused == false) shoot();  
        Facing = Input.GetAxis("Horizontal");
        if((Facing < 0 && facingRight) || (Facing > 0 && !facingRight))
        {
            facingRight = !facingRight;
        }

        player.Gunbar1.value = shotNumber;

        if(shotNumber >= shotMaxNumber) shotNumber = shotMaxNumber;
        if (shots >= 3) player.WeaponAnimator.SetBool("Hypershoot", true);
        else player.WeaponAnimator.SetBool("Hypershoot", false);
        if (shotNumber <= 0) shotNumber = 0;
    }

    void shoot()
    {
        if (Input.GetAxis("Fire1") != 0 || Input.GetButtonDown("Fire1"))
        {
            if(TimeLastShot >= .3f)
            {
                if (shotNumber >= 1 && TimeLastShot >= .15f)
                {
                    isShooting = true;
                    //player.canMove = false;

                    GameObject Projectile = Instantiate(ProjectilePrefab, ProjectileShooter.position, transform.rotation);
                    //player.vCam.GetComponent<CameraShake>().CameraShaker(1.4f, .3f, .1f);
                    player.cameraShake(.1f);
                    player.playSound("ShotSound");

                    shots++;

                    TimeLastShot = 0f;

                    if (facingRight)
                    {
                        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
                    }
                    else
                    {
                        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
                    }

                    shotNumber--;
                    //player.canMove = true;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            if (shots >= 3 && player.isGrounded && TimeLastShot >= .15f)
            {
                GameObject HypershootProjectile = Instantiate(Hypershoot, ProjectileShooter.position, transform.rotation);
                StartCoroutine(HyperShoot());
                shotNumber -= 3;
                TimeLastShot = 0f;
                shots = 0;
                player.playSound("HypershootSound");
                player.cameraShake(.3f);
                //if (facingRight) //player.vCam.GetComponent<CameraShake>().CameraDutch(-.5f, .3f);
                //else //player.vCam.GetComponent<CameraShake>().CameraDutch(1f, .3f);
            }

        }
    }

    void Wait()
    {
        if(TimeLastShot >= .5)
        {
            shotNumber++;
        }
    }

    IEnumerator shootCoroutine()
    {
        yield return new WaitForSeconds(.3f);
    }

    IEnumerator HyperShoot()
    {
        player.hyperShooting = true;
        player.canMove = false;
        player.playerRB.velocity = new Vector2(0f, player.playerRB.velocity.y);
        yield return new WaitForSeconds(.3f);
        player.canMove = true;
        player.hyperShooting = false;
    }
}
