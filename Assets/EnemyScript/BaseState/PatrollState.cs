using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public Animator animator;

    public override void Enter()
    {
        animator = stateMachine.animator;

        animator.SetBool("Patrol", true);
    }

    public override void Perform()
    {
        PatrolCycle();
        if(enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
            animator.SetBool("Patrol",false);

}

   public  void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }else
            {
                animator.SetBool("Patrol", false);
            }
        }
        else
        {
            animator.SetBool("Patrol", true);
        }

    }


}
