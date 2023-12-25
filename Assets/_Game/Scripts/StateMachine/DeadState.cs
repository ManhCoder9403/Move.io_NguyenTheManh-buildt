using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private float _timer;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Action.Dead);
        _timer = 2f;
    }

    public void OnExecute(Bot bot)
    {
        if (_timer <= 0)
        {
            bot.SetState(new IdleState());
        }
        _timer -= Time.fixedDeltaTime;
    }

    public void OnExit(Bot bot)
    {
        bot.ReturnBotToPool();
    }
}
