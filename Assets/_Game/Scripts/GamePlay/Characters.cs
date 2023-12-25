using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private CircleControl _radar;
    [SerializeField] private WeaponDataManager _weaponDataManager;
    [SerializeField] private GameObject _weaponOnHand;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private Weapon _wpPrefab;
    [SerializeField] protected Rigidbody character;
    [SerializeField] private SkinnedMeshRenderer _pantMaterial;
    [SerializeField] private Transform _headPosition;

    private bool _canAttack => target != null && isIdle && !isAttack && !isDead && !isWin;
    private Transform _thisTransform;
    private WeaponData _wpData;
    private SkinData _hairData;
    private SkinData _pantData;
    private Vector3 _defaultScale;
    private int _maxWeapon = 2;
    private int _maxSpawnBot;
    private float _attacktimer;
    private float _defaultSpeed = 1.5f;
    private float _maxScaleRate = 2f;
    private float _scaleRate = 1f;
    private float _radarRadius = 5f;
    private float _attackSpeed;
    private float defaultattackRange = 5f;
    private GameObject _currentWeaponOnHandObj;
    private GameObject _currentHairObj;
    private ObjectPool<Weapon> _weaponPool;

    protected int _currentWp;
    protected int _currentHair;
    protected int _currentPant;
    protected float attackRange;
    protected int scaleAt;
    protected float speed = 5f;
    protected bool isIdle;
    protected bool isDead;
    protected bool isAttack;
    protected bool isWin;
    protected bool isDance;
    protected bool isUlti;
    protected Action<Characters> OnDeadCallBack;
    protected List<Characters> targetList = new List<Characters>();
    protected int WPCount { get => _weaponDataManager.GetWPDataCount(); }
    public Characters target;
    public int playerLevel;
    public int level;
    public bool canMove => !isDead && !isWin;
    public bool IsDead => isDead;
    public GameObject WeaponOnHand => _weaponOnHand;
    protected virtual void Start()
    {
        _maxSpawnBot = BotManager.Instance.botMax;
        scaleAt = PlayingUI.Instance.botCount / _maxSpawnBot;
        _thisTransform = transform;
        ChangeWeapon(_currentWp);
        ChangeHair(_currentHair);
        ChangePant(_currentPant);
    }
    public void OnTarget(Characters target)
    {
        this.target = target;
    }
    public virtual void OnDeath()
    {
        ChangeAnim(Action.Dead);
        OnDeadCallBack?.Invoke(this);
    }
    public virtual void Attack()
    {
        if (_attacktimer > 0f) _attacktimer -= Time.deltaTime;
        else isAttack = false;

        if (!_canAttack) return;
        _thisTransform.LookAt(new Vector3(target.transform.position.x, _thisTransform.position.y, target.transform.position.z));
        if (_attacktimer <= 0f)
        {
            ChangeAnim(Action.Attack);
            _attacktimer += _attackSpeed;
            WeaponOnHand.SetActive(false);
            Weapon weapon = _weaponPool.GetPoolObject();
            SetWeapon(weapon);
        }
    }
    private void SetWeapon(Weapon weapon)
    {
        weapon.transform.position = _shootPosition.position;
        weapon.transform.localScale = _defaultScale * _scaleRate;
        Vector3 direction = new Vector3(target.transform.position.x - _shootPosition.position.x, 0f,
            target.transform.position.z - _shootPosition.position.z);
        weapon.OnInit(direction, attackRange, this);
        weapon.gameObject.SetActive(true);

    }
    public void ChangeWeapon(int weaponID)
    {
        if (_weaponOnHand != null)
        {
            Destroy(_currentWeaponOnHandObj);
        }
        this._wpData = _weaponDataManager.GetWeaponDataByIndex(weaponID);
        this._weaponOnHand = _wpData.weaponOnHand;
        SetRange(_wpData.range);
        SetSpeed(_wpData.speed);
        _currentWeaponOnHandObj = Instantiate(_weaponOnHand, _holdPosition);
        this._wpPrefab = _wpData.weapon;
        _defaultScale = _wpPrefab.transform.localScale;
        _weaponPool = new ObjectPool<Weapon>(_wpPrefab.gameObject, _maxWeapon);
    }
    public void ChangeHair(int skinID)
    {
        if (_weaponOnHand != null)
        {
            Destroy(_currentHairObj);
        }
        this._hairData = SkinShopUI.Instance.HairData.GetItemDataById(skinID);
        _currentHairObj = Instantiate(_hairData.item, _headPosition);
    }
    public void ChangePant(int skinID)
    {
        this._pantData = SkinShopUI.Instance.PantData.GetItemDataById(skinID);
        _pantMaterial.material = _pantData.material;
    }
    public void SetRange(float newRange)
    {
        this.attackRange = defaultattackRange + newRange;
        this._radar.transform.localScale = Vector3.one * attackRange / _radarRadius;
    }
    public void SetSpeed(float newSpeed)
    {
        this._attackSpeed = _defaultSpeed + newSpeed;
    }
    public void UpdateAnimParameters()
    {
        _anim.SetBool(Constants.IDLE_ANIM, isIdle);
        _anim.SetBool(Constants.DEAD_ANIM, isDead);
        _anim.SetBool(Constants.ATTACK_ANIM, isAttack);
        _anim.SetBool(Constants.WIN_ANIM, isWin);
        _anim.SetBool(Constants.DANCE_ANIM, isDance);
        _anim.SetBool(Constants.ULTI_ANIM, isUlti);
    }
    public void SubcribeOnDeadCallBack(Action<Characters> callBack)
    {
        OnDeadCallBack += callBack;
    }
    private void UnsubcribeOnDeadCallBack(Action<Characters> callBack)
    {
        OnDeadCallBack -= callBack;
    }
    public void FoundTarget(Characters target)
    {
        if (target == this || target.isDead) return;

        target.SubcribeOnDeadCallBack(LostTarget);

        if (this.target is null)
        {
            this.target = target;
        }
        else
        {
            targetList.Add(target);
        }
    }
    public void LostTarget(Characters target)
    {
        target.UnsubcribeOnDeadCallBack(LostTarget);

        if (this.target == target)
        {
            this.target = null;
            if (targetList.Count > 0)
            {
                this.target = targetList[0];
                targetList.RemoveAt(0);
            }
        }
        else
        {
            targetList.Remove(target);
        }
    }
    public void ChangeAnim(Action action)
    {
        switch (action)
        {
            case Action.Idle:
                isIdle = true;
                isDead = false;
                isAttack = false;
                isWin = false;
                isDance = false;
                break;
            case Action.Run:
                isIdle = false;
                isDead = false;
                isAttack = false;
                isWin = false;
                isDance = false;
                break;
            case Action.Attack:
                isAttack = true;
                isDead = false;
                isWin = false;
                isUlti = false;
                break;
            case Action.Win:
                isWin = true;
                isDead = false;
                break;
            case Action.Dance:
                isDance = true;
                break;
            case Action.Dead:
                isDead = true;
                break;
            default:

                break;
        }
        UpdateAnimParameters();
    }
    public virtual void GetKill()
    {
        PlayingUI.Instance.SetBotCount(PlayingUI.Instance.botCount - 1);
        level += 1;
        if (level % scaleAt != 0) return;
        _scaleRate = Mathf.Lerp(_scaleRate, _maxScaleRate, 1f / (_maxSpawnBot - (level / scaleAt) + 1));
        transform.localScale = Vector3.one * _scaleRate;
        attackRange += 1f / _maxSpawnBot;
    }
}



