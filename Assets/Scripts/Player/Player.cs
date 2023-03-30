using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;
using Cinemachine;


public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D playerRB;
    private CapsuleCollider2D cd2d;
    private Animator animator;
    public LayerMask Enemy;
    public LayerMask Ground;
    public GameObject floatingDamage;
    public audioManager audioManager;
    private CinemachineImpulseSource cinemachineImpulse;
    private gamePika gamePikaFoda;

    public GameObject monsterGunLightObject;

    public LevelLoader levelLoader;

    [Header("Particles")]
    public ParticleSystem dust;
    public ParticleSystem walkingDust;
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem dashDustParticleR;
    public ParticleSystem dashDustParticleL;

    [Header("Run")]
    public float Speed;
    public float inputAxis;
    private bool isRunning;

    [Header("Jump")]
    private bool jumpCD;
    public float JumpForce;
    public int jumpNumber;
    public float jumpCDT;
    private float jumpCDCounting;
    private float Yspeed;

    [Header("Dash")]
    private bool canDash = true;
    public bool isDashing;
    public float DashForce;
    public float dashTime;
    public float dashCDT;
    private float dashCDCounting;
    private int dashAnimation;
    public GameObject trail;
    private float startTimeBtwSpawn = 0.025f;
    private float timeBtwSpawn = 0;


    [Header("Stats")]
    public bool isGrounded;
    public bool canMove = true;
    public bool hyperShooting;
    public int facingDirection;
    private bool isIdle, isJumping, isFalling;
    private float cd;
    public bool fall;
    private bool dashIvulnerable;

    [Header("Health")]
    public bool dead;
    public GameObject deadGun;
    private bool gunInstantiated = false;
    public int playerMaxLife;
    public int playerLife;
    public Vector2 restartCheckPointLocation;

    [Header("Combat")]
    public string Weapon;
    public float weaponDamage;
    private bool recovering;
    public float recoveryTime;
    private float recoveryCounting;

    [Header("Items")]
    public bool hasTheMap = false;

    [Header("Collision")]
    public bool Pedestal = false;
    public bool Portal = false;

    [Header("PlayerSounds")]
    public sound[] playerSounds;
    public AudioMixerGroup audioMixer;

    [Header("HUD")]
    public bool ponDiscovered;
    public Image sofieAndPonIMAGE;
    public Sprite justSofie;
    public Sprite sofieAndPon;
    public Text damage;
    public Image WeaponIMAGE;
    public Sprite[] WeaponSprite;
    public Animator WeaponAnimator;
    public Slider Gunbar1;
    public Image HealthBarIMAGE;
    public Sprite[] HealthBar;
    public Image HealthIMAGE;
    public Sprite[] Health;

    private void Awake()
    {
        foreach (sound s in playerSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.is3D;
        }
    }
    void Start()
    {
        gamePikaFoda = GameObject.FindGameObjectWithTag("gamePika").GetComponent<gamePika>();
        if (gamePikaFoda.checkPointRespawn) transform.position = gamePikaFoda.lastCheckPoint; gamePikaFoda.checkPointRespawn = false;

        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        cd2d = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Weapon = "Gun";
        playerLife = playerMaxLife;
        dead = false;
        isGrounded = true;
        canDash = true;
        isDashing = false;
        ponDiscovered = false;
        hyperShooting = false;
        facingDirection = 1;
        isRunning = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(playerLife > playerMaxLife) playerLife = playerMaxLife;

        HUD();
        PlayerAnimation();
        dashTrail();

        if (playerLife <= 0)
        {
            dead = true;
            if(!gunInstantiated)
            {
                GameObject deadGunInstance = (GameObject)Instantiate(deadGun, transform.position, transform.rotation);
                gunInstantiated = true;
            }
            stopSound("PlayerCaveWalking");
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }

        CD();

        if (GetComponent<Pause>().isPaused == false && dead == false && canMove == true && isDashing == false)
        {
            Movement();
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<Pause>().isPaused == false && dead == false && canMove == true && isDashing == false)
        {
        }
    }

    void CD()
    {
        if (recovering)
        {
            recoveryCounting += Time.deltaTime;
            if (recoveryCounting >= recoveryTime)
            {
                recoveryCounting = 0;
                recovering = false;
            }
        }

        if (!canDash)
        {
            dashCDCounting += Time.deltaTime;
            if (dashCDCounting >= dashCDT)
            {
                dashCDCounting = 0;
                canDash = true;
            }
        }

        if (jumpCD)
        {
            jumpCDCounting += Time.deltaTime;
            if (jumpCDCounting >= jumpCDT)
            {
                jumpCDCounting = 0;
                jumpCD = false;
            }
        }
    }

    void HUD()
    {
        if (ponDiscovered == true)
        {
            sofieAndPonIMAGE.sprite = sofieAndPon;
            WeaponIMAGE.rectTransform.anchoredPosition = new Vector2(15, -308.8f);
        }

        else
        {
            sofieAndPonIMAGE.sprite = justSofie;
            WeaponIMAGE.rectTransform.anchoredPosition = new Vector2(15, -212.5f);

        }

        damage.text = weaponDamage.ToString();

        if (inputAxis == 0) gameObject.GetComponentInChildren<projectileShooter>().transform.localPosition = new Vector2(.9f, .14f);
        else gameObject.GetComponentInChildren<projectileShooter>().transform.localPosition = new Vector2(1f, 0f);

        if (Weapon == "Gun") WeaponIMAGE.sprite = WeaponSprite[1];
        else WeaponIMAGE.sprite = WeaponSprite[0];

        HealthBarIMAGE.sprite = HealthBar[Mathf.Abs(5 - playerMaxLife)];

        if (playerLife <= 0)
        {
            HealthIMAGE.rectTransform.anchoredPosition = new Vector2(HealthIMAGE.rectTransform.anchoredPosition.x, 500);
            HealthIMAGE.sprite = Health[0];
        }
        else
        {
            HealthIMAGE.sprite = Health[playerLife - 1];
            if (playerLife <= (100 * playerMaxLife / 100)) HealthIMAGE.color = new Color(0, 1, 0, 1);
            if (playerLife <= (75 * playerMaxLife / 100)) HealthIMAGE.color = new Color(1, 1, 0, 1);
            if (playerLife <= (50 * playerMaxLife / 100)) HealthIMAGE.color = new Color(1, 0.5f, 0, 1);
            if (playerLife <= (25 * playerMaxLife / 100)) HealthIMAGE.color = new Color(0.8f, 0, 0, 1);
        }
    }

    void Movement()
    {
        // grupo : Davi Eduardo, Gabriel Alexander, Isaque Rodrigues

        inputAxis = Input.GetAxis("Horizontal");
        Vector2 SpeedV = playerRB.velocity;
        SpeedV.x = inputAxis * Speed;

        if (isDashing == false)
        {
            playerRB.velocity = SpeedV;
        }
        else
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f); ;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isDashing && canDash)
        {
            StartCoroutine(Roll(facingDirection));
            //facingDirection = -1;
        }

        if (inputAxis > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            facingDirection = 1;
        }   

        if (inputAxis < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            facingDirection = -1;
        }

        if (isGrounded == true)
        {
            jumpNumber = 1;
            if(Input.GetButtonDown("Jump") && !jumpCD)
            {
                Jump();
                jumpParticle.Play();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && jumpNumber > 0 && !jumpCD)
            {
                jumpNumber--;
                Jump();
            }
        }
    }

    void Jump()
    {

        playerRB.velocity = Vector2.up * JumpForce;
        jumpCD = true;
        //Debug.Log("Jump");
    }

    IEnumerator Roll(float direction)
    {
        dashIvulnerable = true;
        canDash = false;
        isDashing = true;
        dashParticle.Play();
        playSound("dashSound");
        if (direction == 1) dashDustParticleR.Play();
        else dashDustParticleL.Play();
        animator.SetTrigger("Dash 1");
        playerRB.AddForce(new Vector2(DashForce * direction, 0), ForceMode2D.Impulse);
        float gravity = playerRB.gravityScale;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        dashParticle.Stop();
        if(isGrounded) dust.Play();
        dashIvulnerable = false;
    }

    void dashTrail()
    {
        if(isDashing)
        {
            if(timeBtwSpawn <= 0)
            {
                GameObject trailIstance = (GameObject)Instantiate(trail, transform.position, transform.rotation);
                Destroy(trailIstance, 0.4f);
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else 
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }

    public void playerTakeDamageAUX(bool knockback, int damage)
    {
        if(!dashIvulnerable)
        {
            if (knockback) playerTakeDamage(damage);
            else playerTakeDamageWithoutKnockback(damage);
        }
    }

    private void playerTakeDamage(int damage)
    {
        if (recovering == false && !dead)
        {
            playSound("damage");
            GameObject floatingDamageInstantiated = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().text = "-" + damage.ToString();
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().color = Color.red;
            playerLife -= damage;
            Knockback();
            recovering = true;
            StartCoroutine(redDamage());
            //vCam.GetComponent<CameraShake>().CameraShaker(3,3,.3f);
            cameraShake(1);
        }
    }
    private void playerTakeDamageWithoutKnockback(int damage)
    {
        if (recovering == false && !dead)
        {
            playSound("damage");
            GameObject floatingDamageInstantiated = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().text = "-" + damage.ToString();
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().color = Color.red;
            playerLife -= damage;
            StartCoroutine(redDamage());
            recovering = true;
            //vCam.GetComponent<CameraShake>().CameraShaker(3, 3, .3f);
            cameraShake(1);
        }
    }
    IEnumerator redDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void PlayerAnimation()
    {
        if (fall)
        {
            isGrounded = true;
            if (!dead) dust.Play();
        }

        //if (isDashing) dashParticle.Play();
        //else dashParticle.Stop();

        if(!dead && !isDashing)
        {
            float Xspeed = Mathf.Abs(playerRB.velocity.x);
            if (Mathf.RoundToInt(Xspeed) != 0)
            {
                //Debug.Log("andou");
                animator.SetBool("isRunning", true);
                if (isRunning == false && isGrounded)
                {
                    if (Time.time >= .2f && !isDashing) walkingDust.Play();
                    isRunning = true;
                    playSound("PlayerCaveWalking");
                }
            }
            else
            {
                //Debug.Log("parou");
                animator.SetBool("isRunning", false);
                if (isRunning == true)
                {
                    isRunning = false;
                    stopSound("PlayerCaveWalking");
                    walkingDust.Stop();
                }
            }

            float Yspeed = playerRB.velocity.y;

            if (Mathf.RoundToInt(Yspeed) > 0 && !isGrounded)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
                isJumping = true;
                isFalling = false;
                //dust.Play();
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                isJumping = false;
                isFalling = true;
            }

            if (isGrounded)
            {
                animator.SetBool("isFalling", false);
            }
       
            if (isDashing)
            {
                    //if (dashAnimation == 1) animator.SetTrigger("Dash 1");
                //if (dashAnimation == 2) animator.SetTrigger("Dash 2");
            }

            if (hyperShooting)
            {
                animator.SetBool("Hypershooting", true);
            }
            else animator.SetBool("Hypershooting", false);

            if (isGrounded && !isRunning) isIdle = true;
            else isIdle = false;

            if (!canMove && !hyperShooting && !dead) animator.SetBool("isTakingDamage", true);
            else animator.SetBool("isTakingDamage", false);
        }

        else if (!isDashing)
        {
            animator.SetBool("isTakingDamage", false);
            animator.SetTrigger("isDead");
        }
    }

    void Knockback()
    {
        playerRB.AddForce(new Vector2(9 * -facingDirection, 7), ForceMode2D.Impulse);
        StartCoroutine("Freeze");
    }

    public IEnumerator Freeze()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    public void updateCheckPoint(Vector2 checkPointLocation)
    {
        restartCheckPointLocation = checkPointLocation;
    }

    public void cameraShake(float intensity)
    {
        cinemachineImpulse.GenerateImpulse(intensity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = col.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
    }

    public void playSound(string soundName)
    {
        sound s = Array.Find(playerSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Player Audio wasn't found");
            return;
        }

        s.source.Play();
    }

    public void pauseSound(string soundName)
    {
        sound s = Array.Find(playerSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Player Audio wasn't found");
            return;
        }

        s.source.Pause();
    }

    public void resumeSound(string soundName)
    {
        sound s = Array.Find(playerSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Player Audio wasn't found");
            return;
        }

        s.source.UnPause();
    }

    public void stopSound(string soundName)
    {
        sound s = Array.Find(playerSounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Player Audio wasn't found");
            return;
        }

        s.source.Stop();
    }
}
