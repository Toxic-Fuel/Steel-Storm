using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool seesPlayer;
    public GameObject player;
    public Animator animator;

    public float attackSpeed;
    public float AttackRange = 1f;
    public string[] effects;
    public float baseDamage;
    public float idleTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ChooseAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAction()
    {
        if (seesPlayer)
        {
            StartCoroutine(AttackMelee());
        }
    }
    IEnumerator AttackMelee()
    {
        //animator.SetBool("idle", false);
        animator.SetBool("run", true);
        Debug.Log("NavAI " + name + " is going towards player");
        do
        {
            Vector3 PlayerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            agent.SetDestination(PlayerPos);
            while (Vector3.Distance(agent.transform.position, PlayerPos) > AttackRange + agent.stoppingDistance)
            {
                yield return null;
            }
        } while (Vector3.Distance(agent.transform.position, player.transform.position) > AttackRange + agent.stoppingDistance);
        animator.SetBool("run", false);
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        
        yield return new WaitForSeconds(attackSpeed);
        if(Vector3.Distance(agent.transform.position, player.transform.position) <= AttackRange + agent.stoppingDistance){
            player.GetComponent<EntityHealth>().TakeDamage(baseDamage, effects);
            Debug.Log(name + " attacked player");
        }
        animator.ResetTrigger("Attack");
        //animator.SetBool("idle", true);
        yield return new WaitForSeconds(idleTime);
        ChooseAction();
        yield break;
    }
}
