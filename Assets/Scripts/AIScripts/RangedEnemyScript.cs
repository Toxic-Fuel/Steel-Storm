using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool seesPlayer;
    public GameObject player;
    public Animator animator;

    public float attackSpeed;
    public float AttackRange = 1f;
    public string[] effects;
    public float baseDamage;
    public float range;
    float rangeValue = 2;
    public LobAttack lobAttack;
    // Start is called before the first frame update
    void Start()
    {
        lobAttack.enemy = player;
        ChooseAction();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseAction()
    {
        if (seesPlayer)
        {
            if(Vector3.Distance(agent.transform.position, player.transform.position) <= AttackRange+ rangeValue*4)
            {
                StartCoroutine(AttackMelee());
            }
            else
            {
                StartCoroutine(AttackRanged());
            }
            
        }
    }
    IEnumerator AttackMelee()
    {
        
        Debug.Log("NavAI " + name + " is going towards player");
        agent.SetDestination(player.transform.position);
        while (Vector3.Distance(agent.transform.position, player.transform.position) > AttackRange+agent.stoppingDistance)
        {
            yield return null;
        }
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
        
        yield return new WaitForSeconds(attackSpeed);
        if(Vector3.Distance(agent.transform.position, player.transform.position) <= AttackRange + agent.stoppingDistance){
            player.GetComponent<EntityHealth>().TakeDamage(baseDamage, effects);
            Debug.Log(name + " attacked player");
        }
        
        ChooseAction();
        yield break;
    }
    IEnumerator AttackRanged()
    {

        Debug.Log("NavAI " + name + " is shooting player");
        agent.SetDestination(player.transform.position);
        while (Vector3.Distance(agent.transform.position, player.transform.position) > range)
        {
            yield return null;
        }
        agent.isStopped = true;
        if (animator != null)
        {
            animator.SetTrigger("RangeAttack");
        }

        yield return new WaitForSeconds(attackSpeed);
        if (Vector3.Distance(agent.transform.position, player.transform.position) <= range+ rangeValue)
        {
            lobAttack.enemy = player;
            
            lobAttack.CreateBullet();
            
            Debug.Log(name + " attacked player");
        }
        agent.isStopped = false;
        ChooseAction();
        yield break;
    }
}
