using System;
using Godot;

namespace oce_game_jam_3.AI
{
    public class AIBase
    {
        private enum AIState
        {
            Idle, Aware, Chasing, Attacking, Patrolling, Fleeing
        }
        // intention: we are going to enter at the idle state. 
        // if the player is detected, move towards
        // if the player is touching base, swing
        // if the player is lost, go to nearest point in patrol and continue
        private AIState state = AIState.Idle;
        private bool allowFleeing;
        private float timeInState = 0;

        private const float maxChase = 10.0f;
        private const float maxChaseTime = 5.0f;

        public AIBase(bool allowFleeing = true) => this.allowFleeing = allowFleeing;

        public void resetTimer()
        {
            timeInState = 0.0f;
        }

        public void receiveObservations(float delta, bool enemyDetected, float enemyDistance)
        {
            if (state == AIState.Idle && timeInState > 1.0f)
            {
                if (enemyDetected)
                {
                    GD.Print("AI moving to Aware");
                    state = AIState.Aware;
                    resetTimer();
                }
                else 
                {
                    GD.Print("AI Moving from Idle to patrol");
                    state = AIState.Patrolling;
                    resetTimer();
                }
            }

            if (state == AIState.Aware)
            {
                if (enemyDistance < maxChase)
                {
                    GD.Print("AI Moving from Aware to Chase");
                    state = AIState.Chasing;
                    resetTimer();
                }
                else if (!enemyDetected)
                {
                    GD.Print("AI moving from Aware to Patrol");
                    state = AIState.Patrolling;
                    resetTimer();
                }
            }

            if (state == AIState.Attacking)
            {
                throw new NotImplementedException();
            }

            if (state == AIState.Chasing)
            {
                if (timeInState > maxChaseTime)
                {
                    GD.Print("AI resting");
                    state = AIState.Idle;
                    resetTimer();
                }

                else if (enemyDistance < 0.1f)
                {
                    GD.Print("AI attacking from chase");
                    state = AIState.Attacking;
                    resetTimer();
                }

                else if (!enemyDetected)
                {
                    GD.Print("AI returning to patrol, enemy missing");
                    state = AIState.Patrolling;
                    resetTimer();
                }
            }

            if (state == AIState.Fleeing)
            {
                if (timeInState > 3.0f)
                {
                    GD.Print("AI Going down in flames");
                    state = AIState.Chasing;
                    resetTimer();
                }
            }

            if (state == AIState.Patrolling)
            {
                if (enemyDetected)
                {
                    GD.Print("AI moving to chase");
                    state = AIState.Chasing;
                    resetTimer();
                }
            }

            timeInState += delta;
        }

        // do i need a nextAction method? How will I get state out of the FSM?
    }
}