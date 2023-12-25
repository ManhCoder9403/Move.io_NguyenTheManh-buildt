using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Characters _attacker;
    private float _shootSpeed = 5f;
    private float _rotateSpeed = 400f;
    private Transform _thisTransform;
    private void FixedUpdate()
    {
        _thisTransform.Rotate(Vector3.back, _rotateSpeed * Time.fixedDeltaTime);
    }
    public void OnInit(Vector3 direction, float attackRange, Characters attacker)
    {
        this._attacker = attacker;
        this._thisTransform = this.transform;
        if ((_attacker as Player) != null)
        {
            SoundManager.Instance.PlaySFX(SFXType.WeaponThrow);
        }
        var destination = this.transform.position + direction.normalized * attackRange;
        var duration = Vector3.Distance(this.transform.position, destination) / _shootSpeed;
        transform.DOMove(destination, duration).SetEase(Ease.Linear).OnComplete(() => Despawn());
    }

    public void Despawn()
    {
        _attacker.WeaponOnHand.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ((int)LayerType.Character))
        {
            Characters temp = other.GetCharacter();
            if (temp.IsDead) return;
            if (this._attacker == temp) return;
            temp.OnDeath();
            this.Despawn();
            _attacker.GetKill();
        }
        if (other.gameObject.layer == ((int)LayerType.Radar)) return;
    }
}
