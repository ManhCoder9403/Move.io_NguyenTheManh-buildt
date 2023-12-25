using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float _timer;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Action.Idle);
        _timer = Random.Range(2f, 4f);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.target is null || _timer <= 0f)
        {
            bot.SetState(new RunState());
        }
        else
        {
            bot.Attack();
        }
        _timer -= Time.fixedDeltaTime;
    }

    public void OnExit(Bot bot)
    {
        bot.ResetTarget();
    }
}
