using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    private float chaseSpeed = 1f;
    private Vector3 targetPosition;
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.transform.GetComponent<Enemy>();
        targetPosition = enemy.targetPosition;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform enemyTrans = animator.transform;

        //if (enemy == null)
        //{
        //    Debug.Log("Enemey NULL!!!!!");
        //}

        targetPosition = enemy.targetPosition;
        if (Vector2.Distance(enemyTrans.position, targetPosition) > 0.1f)
        {
            enemyTrans.position = Vector2.MoveTowards(enemyTrans.position, targetPosition, chaseSpeed * Time.deltaTime);
            enemyTrans.up = (targetPosition - enemyTrans.position).normalized;
        }
        else
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);
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
