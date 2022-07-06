using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    GameObject player;
    [SerializeField] private float health = 100f;

    public float visible = 30f;
    public float angleV = 70f;

    // Start is called before the first frame update
    private int s = 0;
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0&&s==0)
        {
            s = 1;
            //animator.SetBool("Die", true);
            Destroy(gameObject,3.0f);
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (player != null && s==0)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 2f)
            {
                //animator.SetBool("Attack", true);
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
                //animator.SetBool("Attack", false);
            }
            else
            {

 
            }
        }
 
    }
}
