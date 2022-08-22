using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int damge = 1;

    public float fireTime = 0.3f;
    public GameObject smoke;
    public GameObject gunHead;
    public float playerHealth = 10;
    public AudioClip playerDeathSound;
    public Slider healthBar;

    private float playerCurrentHealth = 10;
    private float lastFireTime = 0;
    private Animator anim;
    private AudioSource audioS;
    private GameObject gameController;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        UpdateFireTime();
        audioS = gameObject.GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        healthBar.maxValue = playerHealth;
        healthBar.value = playerCurrentHealth;
        healthBar.minValue = 0;
	}

    void UpdateFireTime()
    {
        lastFireTime = Time.time;
    } 

    void SetFireAnim(bool isFire)
    {
        anim.SetBool("isShoot", isFire);
    }
	
    public void GetHit(float damge)
    {
        audioS.Play();
        playerCurrentHealth -= damge;
        healthBar.value = playerCurrentHealth;

        if (playerCurrentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        audioS.clip = playerDeathSound;
        audioS.Play();
        gameController.GetComponent<GameController>().EndGame();
    }

    void Fire()
    {
        if (Time.time >= lastFireTime + fireTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_IOS || UNITY_ANDROID
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {            
                    SetFireAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
                }
            }
#else           

            RaycastHit hit;

            if (Physics.Raycast(gunHead.transform.position, gunHead.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {
                    SetFireAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
                }
            }
#endif
            UpdateFireTime();
        }
        else
        {
            SetFireAnim(false);
        }
    }

    void InsSmoke()
    {
        GameObject sm = Instantiate(smoke, gunHead.transform.position, gunHead.transform.rotation) as GameObject;
        Destroy(sm, 0.5f);
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButton(0))
        {
            Fire();
        }
	}
}
