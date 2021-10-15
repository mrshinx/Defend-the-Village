using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{

    [Range(0f, 10f)]
    [SerializeField] float baseSpeed;
    [SerializeField] float baseHP;
    [SerializeField] float deadtime;
    [SerializeField] float hitTime;
    [SerializeField] int bounty;
    public float monsterEXP;
    [SerializeField] GameObject deadAnim;
    [SerializeField] GameObject hpDisplay;
    [SerializeField] GameObject hpSize;
    [SerializeField] AudioClip[] deadSound;
    [SerializeField] AudioClip[] painSound;
    [SerializeField] Material flashMaterial;
    SpriteRenderer monsterSprite;
    Material defaultMaterial;
    GameObject lootManager;
    public GameObject lastHiter;
    Animator animator;
    float walkSpeed;
    public float currentHP;
    public float HP;
    bool giveEXP = false;

    Rigidbody2D monsterBody;


    void Start()
    {
        lootManager = GameObject.FindWithTag("Loot Manager");
        HP = (float)System.Math.Round(baseHP * (1 + Mathf.Pow(WaveController.currentWave, 2f) * 0.003f), 0); 
        currentHP = HP;
        animator = gameObject.GetComponent<Animator>();
        walkSpeed = baseSpeed;
        hpSize.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, 0.1f);
        monsterBody = GetComponent<Rigidbody2D>();

        monsterSprite = GetComponent<SpriteRenderer>();
        defaultMaterial = monsterSprite.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
                if (!giveEXP)
                {
                    lastHiter.GetComponent<TowerProperties>().GetExp(monsterEXP);
                    giveEXP = true;
                }

            CharDeath.DeathAnim(deadAnim, gameObject.transform, deadtime, deadSound[UnityEngine.Random.Range(0, 2)],1);
            lootManager.GetComponent<Loot>().Bounty(bounty, transform.position + new Vector3(0, gameObject.GetComponent<BoxCollider2D>().size.y, 0));
            Destroy(gameObject);
        }
        if (currentHP / HP >= 0.5f)  hpDisplay.GetComponent<Image>().color = new Color(0, 1, 0, 1);

        if ((currentHP / HP >= 0.25f)&&(currentHP / HP < 0.5f)) hpDisplay.GetComponent<Image>().color = new Color(1, 0.5668887f, 0, 1);

        if (currentHP / HP < 0.25f) hpDisplay.GetComponent<Image>().color = new Color(1, 0.04673456f, 0, 1);

        hpDisplay.GetComponent<RectTransform>().offsetMin = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x*(1-currentHP/HP), 0);

        if(monsterBody.velocity.y==0) transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);

    }



    IEnumerator GetHit()
    {
 
        walkSpeed = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.1f, 0) ;
        //      animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.05f);
      //      animator.SetBool("Hit", false);
        walkSpeed = baseSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield break;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        AudioSource.PlayClipAtPoint(painSound[UnityEngine.Random.Range(0, painSound.Length)], Camera.main.transform.position,0.15f);
        monsterSprite.material = flashMaterial;
        Invoke("ResetMaterial", 0.05f);
        // if ((currentHP > 0) &&(!animator.GetBool("Hit")))
        //    StartCoroutine(GetHit());
    }

    public void Knockback(float x, float y)
    {
        monsterBody.AddForce(new Vector2(x,y),ForceMode2D.Impulse);
    }

    void ResetMaterial()
    {
        monsterSprite.material = defaultMaterial;
    }
}

