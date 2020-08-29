using Godot;

namespace oce_game_jam_3.AI
{
    public class AIBase
    {
        // intention: we are going to enter at the idle state. 
        // if the player is detected, move towards
        // if the player is touching base, swing
        // if the player is lost, go to nearest point in patrol and continue
        private bool allowFleeing;
        private float timeInState = 0;

        private const float maxChase = 10.0f;
        private const float maxChaseTime = 5.0f;

        public AI.AIState State { get ; private set; }

        public AIBase(bool allowFleeing = true) => this.allowFleeing = allowFleeing;

        private void changeState(AIState toState)
        {
            GD.Print("AI moving from " + State + " to " + toState + " after " + (timeInState) + " seconds");
            State = toState;
            timeInState = 0.0f;
        }

        public void receiveObservations(float delta, bool enemyDetected, float enemyDistance)
        {
            if (State == AIState.Idle && timeInState > 1.0f)
            {
                if (enemyDetected)
                {
                    changeState(AIState.Aware);
                }
                else 
                {
                    changeState(AIState.Patrolling);
                }
            }

            if (State == AIState.Aware)
            {
                if (enemyDistance < maxChase)
                {
                    changeState(AIState.Chasing);
                }
                else if (!enemyDetected)
                {
                    changeState(AIState.Patrolling);
                }
            }

            if (State == AIState.Attacking)
            {
                if (enemyDetected)
                {
                    if (enemyDistance > 0.1f)
                    {
                        changeState(AIState.Chasing);
                    }
                }
                else
                {
                    changeState(AIState.Patrolling);
                }
            }

            if (State == AIState.Chasing)
            {
                if (timeInState > maxChaseTime)
                {
                    changeState(AIState.Idle);
                }

                else if (enemyDistance < 0.1f)
                {
                    changeState(AIState.Attacking);
                }

                else if (!enemyDetected)
                {
                    changeState(AIState.Patrolling);
                }
            }

            if (State == AIState.Fleeing)
            {
                if (timeInState > 3.0f)
                {
                    changeState(AIState.Chasing);
                }
            }

            if (State == AIState.Patrolling)
            {
                if (enemyDetected)
                {
                    changeState(AIState.Chasing);
                }
            }

            timeInState += delta;
        }

        // do i need a nextAction method? How will I get state out of the FSM?
    }
}