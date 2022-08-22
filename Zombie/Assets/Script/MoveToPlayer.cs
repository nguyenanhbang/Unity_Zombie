using UnityEngine;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {

    float moveSpeed;
    public float minMoveSpeed = 0.05f;
    public float maxMoveSpeed = 0.3f;
    GameObject player;
    public float attackRange = 1;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateMoveSpeed();
	}

    void UpdateMoveSpeed()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }
	
    void Move()
    {
        if (player == null)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isIdle", true);
            gameObject.GetComponent<ZombieController>().isAttack = true;
            gameObject.GetComponent<MoveToPlayer>().enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
        Move();
	}
}
