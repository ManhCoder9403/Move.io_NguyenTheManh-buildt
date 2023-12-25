using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private float _timer;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Action.Run);
        bot.MoveRandomly();
        _timer = bot.timeToRun;
    }

    public void OnExecute(Bot bot)
    {
        if (bot.target != null)
        {
            bot.SetState(new AttackState());
            return;
        }
        if (_timer <= 0f)
        {
            bot.timeToRun = 0;
            bot.SetState(new IdleState());
        }
        _timer -= Time.fixedDeltaTime;
    }

    public void OnExit(Bot bot)
    {
        bot.StopMove();
    }
}
