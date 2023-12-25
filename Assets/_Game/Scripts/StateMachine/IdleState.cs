using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float _timer;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Action.Idle);
        _timer = Random.Range(0f, 3f);
    }

    public void OnExecute(Bot bot)
    {
        if (_timer <= 0f)
        {
            bot.SetState(new RunState());
        }
        if (bot.target != null)
        {
            bot.SetState(new AttackState());
            return;
        }
        _timer -= Time.fixedDeltaTime;
    }

    public void OnExit(Bot bot)
    {

    }
}
