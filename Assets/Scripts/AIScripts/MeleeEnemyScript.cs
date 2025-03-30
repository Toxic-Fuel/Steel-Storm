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
    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(AttackMelee());
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
}
