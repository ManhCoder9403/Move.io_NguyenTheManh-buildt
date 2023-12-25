using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _targetCircle;

    private PlayerData _playerData;
    private Transform _myTransform;
    protected override void Start()
    {
        this._joystick = LevelManager.Instance.joystick;
        _myTransform = this.transform;
        this.OnInit();
        base.Start();
    }
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Home))
        {
            ChangeAnim(Action.Idle);
        }
        else if (GameManager.Instance.IsState(GameState.Playing))
        {
            Move();
            Attack();
            SetTargetCircle();
        }
        else if (GameManager.Instance.IsState(GameState.Win))
        {
            ChangeAnim(Action.Win);
        }
    }
    public void OnInit()
    {
        _playerData = GameManager.Instance.PlayerData;
        _currentWp = _playerData.CurrentWeapon;
        _currentHair = _playerData.CurrentHair;
        _currentPant = _playerData.CurrentPant;
    }
    public override void Attack()
    {
        base.Attack();
    }
    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
    private void SetTargetCircle()
    {
        if (target != null)
        {
            _targetCircle.transform.position = new Vector3(target.transform.position.x, 0.1f, target.transform.position.z);
            _targetCircle.SetActive(true);
        }
        else
        {
            _targetCircle.SetActive(false);
        }
    }
    private void Move()
    {
        if (!canMove) return;
        Vector3 direction = new Vector3(_joystick.Horizontal * base.speed, 0f, _joystick.Vertical * base.speed);
        if (direction != Vector3.zero)
        {
            ChangeAnim(Action.Run);
            character.velocity = direction;
            Vector3 rotateDirection = new Vector3(direction.x, _myTransform.position.y, direction.z);
            _myTransform.rotation = Quaternion.LookRotation(rotateDirection);
        }
        else
        {
            if (isAttack) return;
            character.velocity = Vector3.zero;
            ChangeAnim(Action.Idle);
        }
    }
    public override void GetKill()
    {
        base.GetKill();
        SoundManager.Instance.PlaySFX(SFXType.WeaponHit);
        GameManager.Instance.ChangeGold(2);
        playerLevel += 1;
        if (playerLevel % scaleAt != 0) return;
        CameraFollow.Instance.ZoomOut(playerLevel);
    }
}
