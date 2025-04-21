using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrassheadEnemy : MonoBehaviour
{
    public bool seesPlayer;
    public Animator animator;
    public GameObject player;
    NavMeshAgent agent;
    float idleTime = 1.5f, AttackRange = 2f, slamTime = 1.5f, range = 10f, steamTime = 4f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("idle", false);
        Invoke("StartSeeing", 10f);
    }
    void ChooseAction()
    {
        int r = Random.Range(1, 3);
        switch (r)
        {
            case 1:
                StartCoroutine(Slam());
                break;
            case 2:
                StartCoroutine(Steam());
                break;
        }
    }
    IEnumerator Slam()
    {
        agent.isStopped = false;
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
        agent.SetDestination(player.transform.position);
        Vector3 lastKnownPos = player.transform.position;
        while (Vector3.Distance(agent.transform.position, lastKnownPos) > AttackRange + agent.stoppingDistance)
        {
            
            yield return null;
        }
        animator.SetBool("run", false);
        animator.SetTrigger("slam");
        yield return new WaitForSeconds(slamTime);
        animator.ResetTrigger("slam");
        animator.SetBool("idle", true);
        yield return new WaitForSeconds(idleTime);
        ChooseAction();
        yield break;
    }
    IEnumerator Steam()
    {
        agent.isStopped = false;
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
        agent.SetDestination(player.transform.position);
        Vector3 lastKnownPos = player.transform.position;
        while (Vector3.Distance(agent.transform.position, lastKnownPos) > range)
        {
            yield return null;
        }
        animator.SetBool("run", false);
        agent.isStopped = true;
       
        animator.SetBool("steam", true);
        yield return new WaitForSeconds(steamTime);
        animator.SetBool("steam", false);

        animator.SetBool("idle", true);
        yield return new WaitForSeconds(idleTime);
        ChooseAction();
        yield break;
    }
    void StartSeeing()
    {
        ChooseAction();
    }
}
