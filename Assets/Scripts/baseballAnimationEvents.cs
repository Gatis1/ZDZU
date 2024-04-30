using UnityEngine;

public class baseballAnimationEvents : StateMachineBehaviour
{
    public int counter = 0;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter++;
        // Once the animation ends, we are ready to SHOOT the ball
        Shooting.ReadyToShootBall = true;
    }
}
