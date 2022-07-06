using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ZombieEnemy : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    CapsuleCollider collid;
    GameObject player;
    public GameObject FirstPerson;
    public Victory victory;

    public float visible = 30f;
    public float angleV = 70f;
    public float damage;
    public float damageDelay = 1f;
    public bool die=false;
    float timer;
    [SerializeField] private float health = 200f;
    private int s = 0;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        collid = GetComponent<CapsuleCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && s == 0)
        {
            s = 1;
            agent.speed = 0;
            collid.isTrigger = true;
            victory.GetComponent<Victory>().mission++;
            Destroy(collid);
            animator.SetBool("Die", true);
        }
    }

    public void Damage()
    {

       
        HealthBar.AdjustCurrentValue(-damage);
        timer = 0f;
    }

    void Update()
    {

        if (player != null && s == 0)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 2f)
            {
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                if (timer < damageDelay)
                {
                    timer += Time.deltaTime; //добавить в таймер
                }
                if(timer >= damageDelay)
                {
                    Damage();
                }

            }
            else if (distance < visible)
            {
                //Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);
                //float angle = Quaternion.Angle(transform.rotation, look);
                //if (angle < angleV)
                //{
                //    RaycastHit hit;
                //    Ray ray = new Ray(transform.position + Vector3.up, player.transform.position - transform.position + Vector3.up);
                //    if (Physics.Raycast(ray, out hit, visible))
                //    {
                //        if (hit.transform.gameObject == player)
                //        {
                            agent.destination = player.transform.position;
                //    }
                //}

                //}
            }
            if (agent.velocity.magnitude > 2f)
            {
                animator.SetBool("attack", false);
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("walk", false);
            }
        }
    }
}