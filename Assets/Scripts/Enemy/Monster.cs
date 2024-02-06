using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour, IAffectableByAura
{

    [Range(0f, 10f)]
    [SerializeField] float baseSpeed;
    [NonSerialized] public float moveSpeed;
    [NonSerialized] public float bonusMoveSpeedAura = 0f;

    [SerializeField] float baseMaxHP;
    [NonSerialized] public float currentHP;
    [NonSerialized] public float maxHP;

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

    bool giveEXP = false;

    Rigidbody2D monsterBody;
    public List<Aura> Auras = new List<Aura>();
    public EnemyRarity rarity = new EnemyRarity();

    void Start()
    {
        CalculateStat(true);

        lootManager = GameObject.FindWithTag("Loot Manager");
        animator = gameObject.GetComponent<Animator>();
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

            AnimationManager.PlayDeathAnimation(deadAnim, gameObject.transform, deadtime, deadSound[UnityEngine.Random.Range(0, 2)],1);
            lootManager.GetComponent<Loot>().Bounty(bounty, transform.position + new Vector3(0, gameObject.GetComponent<BoxCollider2D>().size.y, 0));
            Destroy(gameObject);
        }
        if (currentHP / maxHP >= 0.5f)  hpDisplay.GetComponent<Image>().color = new Color(0, 1, 0, 1);

        if ((currentHP / maxHP >= 0.25f)&&(currentHP / maxHP < 0.5f)) hpDisplay.GetComponent<Image>().color = new Color(1, 0.5668887f, 0, 1);

        if (currentHP / maxHP < 0.25f) hpDisplay.GetComponent<Image>().color = new Color(1, 0.04673456f, 0, 1);

        hpDisplay.GetComponent<RectTransform>().offsetMin = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x*(1-currentHP/maxHP), 0);

        if(monsterBody.velocity.y==0) transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        HandleAuraEffect();

    }

    public void CalculateStat(bool ini = false)
    {
        maxHP = (float)System.Math.Round(baseMaxHP * (1 + Mathf.Pow(WaveController.currentWave, 2f) * 0.003f), 0);
        maxHP = maxHP * (1 + rarity.magnitude[0] /100f);
        if (ini) currentHP = maxHP;

        moveSpeed = baseSpeed*(1+bonusMoveSpeedAura);
    }

    IEnumerator GetHit()
    {
 
        moveSpeed = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.1f, 0) ;
        //      animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.05f);
      //      animator.SetBool("Hit", false);
        moveSpeed = baseSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield break;
    }

    void ResetMaterial()
    {
        monsterSprite.material = defaultMaterial;
    }

    public void TakeDamage(float damage, bool hit = true)
    {
        currentHP -= damage;
        // Play hit animation if this is a hit
        if (hit)
        {
            AudioSource.PlayClipAtPoint(painSound[UnityEngine.Random.Range(0, painSound.Length)], Camera.main.transform.position, 0.15f);
            monsterSprite.material = flashMaterial;
            Invoke("ResetMaterial", 0.05f);
        }
        // if ((currentHP > 0) &&(!animator.GetBool("Hit")))
        //    StartCoroutine(GetHit());
    }

    public void Knockback(float x, float y)
    {
        monsterBody.AddForce(new Vector2(x,y),ForceMode2D.Impulse);
    }

    public void ApplyAura(Aura aura)
    {

        // If aura already exists, refresh duration
        Aura _aura;
        _aura = Auras.FirstOrDefault(_aura => _aura.auraId == aura.auraId && _aura.source == aura.source);

        if (_aura != null)
        {
            _aura.currentDuration = 0;
            // If max stack has not been reached, increase stack
            if (_aura.stack < _aura.maxStack) _aura.stack += 1;
        }
        else
            Auras.Add(aura);
    }

    public void HandleAuraEffect()
    {
        // Reset Bonus Stat from auras
        ResetAuraBonus();

        foreach (Aura aura in Auras) 
        {
            // Count duration if aura is not permanent
            if (!aura.isPermanent) aura.currentDuration += Time.deltaTime;

            // Handle aura effect accordingly
            switch (aura.auraType)
            {
                case AuraObject.AuraType.StatModifier:
                    StatModifier statModifier = aura as StatModifier;
                    switch (statModifier.statType)
                    {
                        case StatModifierObject.Stat.MoveSpeed:
                            bonusMoveSpeedAura += statModifier.magnitude;
                            break;
                        case StatModifierObject.Stat.HP:
                            break;
                        default:
                            break;
                    }
                    break;
                case AuraObject.AuraType.DamageOvertime:
                    DamageOverTime dot = aura as DamageOverTime;
                    dot.currentTick += Time.deltaTime;
                    if (dot.currentTick >= dot.tick)
                    {
                        dot.currentTick -= dot.tick;
                        TakeDamage(AuraManager.CalculateDamageOverTime(dot), false);
                    }
                    break;
                default:
                    break;
            }
        }

        // Recalculate stat after being affected by auras
        CalculateStat();
        // Remove expired auras
        Auras.RemoveAll(aura => aura.currentDuration >= aura.baseDuration);
    }

    public void ResetAuraBonus()
    {
        bonusMoveSpeedAura = 0f;
    }
}

