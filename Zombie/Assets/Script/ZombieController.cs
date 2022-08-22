using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    public int zombieHealth = 3;    
    public float shootTime = 0.5f;
    public bool isAttack = false;
    public float attackTime = 1;    
    public AudioClip zombieDeathSound;
    public float damge = 1;
    public bool IsShooten
    {
        get { return isShooten; }
        set
        {
            isShooten = value;
            ShootenAnim(isShooten);
            UpdateShootenTime();
        }
    }

    private Animator anim;
    private bool isShooten;
    private bool isDead = false;
    private float lastAttackTime = 0;
    private AudioSource audioS;
    private float lastShootenTime = 0;
    private GameObject player;
    private GameObject gameController;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        IsShooten = false;
        anim.SetBool("isDead", false);
        audioS = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void UpdateShootenTime()
    {
        lastShootenTime = Time.time;
    }

    void UpdateAttackTime()
    {
        lastAttackTime = Time.time;
    }

    void ShootenAnim(bool isShooten)
    {
        anim.SetBool("isShooten", isShooten);
    }

    void AttackAnim(bool isAttack)
    {
        anim.SetBool("isAttack", isAttack);
    }
	
    public void GetHit(int damge)
    {
        if (isDead)
            return;
        audioS.Play();
        IsShooten = true;
        zombieHealth -= damge;       

        if (zombieHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;
        audioS.clip = zombieDeathSound;
        audioS.Play();
        anim.SetBool("isDead", true);
        gameController.GetComponent<GameController>().GetPoint(1);
        Destroy(gameObject, 2f);
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackTime)
        {            
            AttackAnim(true);
            UpdateAttackTime();
            player.GetComponent<PlayerController>().GetHit(damge);
        }
        else
        {
            AttackAnim(false);
        }
    }

	// Update is called once per frame
	void Update () {
        if (IsShooten && Time.time >= lastShootenTime + shootTime)
        {
            IsShooten = false;
        }
        if (isAttack)
        {
            Attack();
        }
    }
}
