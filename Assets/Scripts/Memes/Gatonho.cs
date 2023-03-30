using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Gatonho : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private int playerIsToRight;
    private UnityEngine.Vector3 playerPos;

    private Animator gatonhoAnimator;
    private SpriteRenderer gatonhoSR;

    public float gatonhoSpeed;
    public ParticleSystem gatonhoParticulas;
    private bool particulas, particulas2;
    public AudioSource[] gatonhoAudio;

    int vozGolfinho;

    [Header("Dialogo")]
    public GameObject caixaDialogo;
    public TextMeshProUGUI textMesh;
    public string[] linhas;
    public float textSpeed;
    private int index;
    private bool dialogou;



    void Start()
    {
        gatonhoAnimator = GetComponent<Animator>();
        gatonhoSR = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();

        dialogou = false;
        textMesh.text = string.Empty;
    }

    void Update()
    {

        if (UnityEngine.Vector3.Distance(transform.position, playerScript.transform.position) <= 5)
        {
            gatonhoSR.enabled = true;
            particulas2 = false;
            if(!particulas)
            {
                if (Time.time >= 1) gatonhoAudio[0].Play();
                gatonhoParticulas.Play();
                particulas = true;
            }

            if (UnityEngine.Vector3.Distance(transform.position, playerScript.transform.position) <= 3 && !dialogou)
            {
                caixaDialogo.SetActive(true);
                dialogo();
            }
        }

        else if(UnityEngine.Vector3.Distance(transform.position, playerScript.transform.position) >= 6)
        {
            gatonhoSR.enabled = false;
            caixaDialogo.SetActive(false);
            dialogou = false;
            textMesh.text = string.Empty;
            particulas = false;
            if(!particulas2)
            {
                gatonhoAudio[vozGolfinho].Stop();
                gatonhoParticulas.Play();
                particulas2 = true;
            }
        }

        flipSprite();

        playerPos = playerScript.transform.position;

        if(Input.GetKeyDown(KeyCode.L))
        {
            if(textMesh.text == linhas[index])
            {
                gatonhoAudio[vozGolfinho].Stop();
                proximaLinha();
            }

            else

            {
                gatonhoAudio[vozGolfinho].Stop();
                StopAllCoroutines();
                textMesh.text = linhas[index];
            }
        }
    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {
        
    }

    void dialogo()
    {

        dialogou = true;
        caixaDialogo.SetActive(true);
        index = 0;
        StartCoroutine(digitaLinha());
    }

    void proximaLinha()
    {
        if(index < linhas.Length - 1)
        {
            index++;
            textMesh.text = string.Empty;
            StartCoroutine(digitaLinha());
        }

        else

        {
            caixaDialogo.SetActive(false);
        }
    }

    IEnumerator digitaLinha()
    {
        vozGolfinho = Mathf.RoundToInt(Random.Range(2, 4));
        gatonhoAudio[vozGolfinho].Play();
        caixaDialogo.GetComponent<Animator>().SetBool("isHablando", true);

        foreach (char c in linhas[index].ToCharArray())
        {
            textMesh.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        caixaDialogo.GetComponent<Animator>().SetBool("isHablando", false);
        gatonhoAudio[vozGolfinho].Stop();
    }

    void flipSprite()
    {
        if (transform.position.x > playerScript.transform.position.x)
        {
            transform.eulerAngles = new UnityEngine.Vector2(0f, 0);
            playerIsToRight = -1;
            caixaDialogo.transform.eulerAngles = new UnityEngine.Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new UnityEngine.Vector2(0f, 180f);
            playerIsToRight = 1;
            caixaDialogo.transform.eulerAngles = new UnityEngine.Vector2(0, 0);
        }
    }
}
