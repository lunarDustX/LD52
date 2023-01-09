using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private float patrolSpeed = 1f;
    private int pointIndex;
    GameObject[] patrolPoints;
    private GameObject player;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Enemy enemy = animator.transform.GetComponent<Enemy>();
        patrolPoints = enemy.patrolSpots.patrolPoints;
        pointIndex = 0;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform enemyTrans = animator.transform;
        Vector3 targetPosition = patrolPoints[pointIndex].transform.position;

        if (Vector2.Distance(enemyTrans.position, targetPosition) > 0.1f)
        {
            enemyTrans.position = Vector2.MoveTowards(enemyTrans.position, targetPosition, patrolSpeed * Time.deltaTime);

            enemyTrans.up = (targetPosition - enemyTrans.position).normalized;
        }
        else
        {
            pointIndex++;
            pointIndex %= patrolPoints.Length;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPatrolling", false);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
