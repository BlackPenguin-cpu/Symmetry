using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public struct TagName
{
    public const string Platform = "Platform";
}
#region 플레이어 인포 클래스
[System.Serializable]
public class StartStat
{
    public readonly float originalMaxHp = 100;
    public readonly float originalSpeed = 20;
    public readonly float originalCrit = 5;
    public readonly float originalDef = 10;
    public readonly float originalCooldown = 0;
    public readonly float originalDashCooldown = 0.7f;
    public readonly int originalDashCount = 1;
    public readonly float originalAttackDamage = 10;
}
public class PlayerWeaponSkillInfo
{
    public bool swordIronHeart;
    public bool swordTenacity;
    public bool swordGodBless;
    public bool daggerSwift;
    public bool daggerComboAttack;
    public bool daggerHiddenCard;
    public bool axeTakle;
    public bool axeEarthQuake;
    public bool axeEruption;

    public void SwordSkillCheck(int level)
    {
        if (level >= 5)
        {
            swordGodBless = true;
        }
        else if (level >= 3)
        {
            swordTenacity = true;
        }
        else if (level >= 1)
        {
            swordIronHeart = true;
        }
    }
    public void DaggerSkillCheck(int level)
    {
        if (level >= 5)
        {
            daggerHiddenCard = true;
        }
        else if (level >= 3)
        {
            daggerComboAttack = true;
        }
        else if (level >= 1)
        {
            daggerSwift = true;
        }
    }
    public void AxeSkillCheck(int level)
    {
        if (level >= 5)
        {
            axeEruption = true;
        }
        else if (level >= 3)
        {
            axeEarthQuake = true;
        }
        else if (level >= 1)
        {
            axeTakle = true;
        }
    }
}
[System.Serializable]
public class MagicPower
{
    public int silpidLeap = 0;
    public int giantPower = 0;
    public int ironSkin = 0;
    public int magicHeart = 0;
    public bool isMagicHeartActive = false;
    public int invisibleHand = 0;
    public int sharpEye = 0;
    public int timeQuick = 0;
    public int thaumcraft = 0;
}
[System.Serializable]
public class PlayerInfo
{
    private Dictionary<PlayerWeaponType, int> level = new Dictionary<PlayerWeaponType, int>()
    { [PlayerWeaponType.Axe] = 0, [PlayerWeaponType.Dagger] = 0, [PlayerWeaponType.Sword] = 0 };
    public PlayerWeaponSkillInfo skillInfo = new PlayerWeaponSkillInfo();
    public MagicPower magicPower = new MagicPower();
    private StartStat startStat = new StartStat();
    public Dictionary<PlayerWeaponType, int> _level
    {
        get { return level; }
        set
        {
            level = value;
            LevelStat();
        }
    }

    public void LevelStat()
    {
        switch (weaponType)
        {
            case PlayerWeaponType.Sword:
                attackDamage = (10 * level[PlayerWeaponType.Sword]) + 10 + startStat.originalAttackDamage;
                maxHp = (10 * level[PlayerWeaponType.Sword]) + 10 + startStat.originalMaxHp;
                skillInfo.SwordSkillCheck(level[PlayerWeaponType.Sword]);
                break;
            case PlayerWeaponType.Dagger:
                attackDamage = (8 * level[PlayerWeaponType.Dagger]) + 8 + startStat.originalAttackDamage;
                crit = (level[PlayerWeaponType.Dagger] * 2) + startStat.originalCrit;
                skillInfo.DaggerSkillCheck(level[PlayerWeaponType.Dagger]);
                break;
            case PlayerWeaponType.Axe:
                attackDamage = (15 * level[PlayerWeaponType.Axe]) + 20 + startStat.originalAttackDamage;
                def = (level[PlayerWeaponType.Axe] * 5) + 5 + startStat.originalDef;
                skillInfo.AxeSkillCheck(level[PlayerWeaponType.Axe]);
                break;
        }
    }

    public PlayerWeaponType weaponType;

    [SerializeField] private float maxHp = 100;
    [SerializeField] private float hp = 100;
    [SerializeField] private float speed = 20; // velocity
    [SerializeField] private float crit = 5;
    [SerializeField] private float def = 10;
    [SerializeField] private float cooldown = 0;
    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private int dashCount = 1;
    private int curDashCount;
    public float dashCooldown = 0.7f;
    public int _dashCount
    {
        get
        {
            int returnValue = dashCount;
            if (weaponType == PlayerWeaponType.Dagger)
                returnValue++;
            if (magicPower.silpidLeap >= 1)
                returnValue++;
            return returnValue;
        }
        set => dashCount = value;
    }
    public int _curDashCount
    {
        get { return curDashCount; }
        set
        {
            value = Mathf.Clamp(value, 0, dashCount);
            curDashCount = value;
        }
    }
    public float _maxHp
    {
        get
        {
            float returnValue = maxHp;
            returnValue += Crystals[(int)CrystalsType.HEALTH] * 10;
            return returnValue;
        }
        set => maxHp = value;
    }
    public float _hp
    {
        get { return hp; }
        set
        {
            if (value < hp && weaponType == PlayerWeaponType.Sword)
            {
                if (value <= 0)
                {
                    value = 0;
                }
                else
                {
                    value = Mathf.Min(value + 2, hp);
                }
            }
            if (value <= 0)
            {
                if (skillInfo.swordGodBless && swordSkill3Boolean == false)
                {
                    value = 1;
                    swordSkill3Boolean = true;
                }
            }
            hp = value;
        }
    }
    public float _speed
    {
        get
        {
            float returnValue = speed;
            returnValue += returnValue * (Crystals[(int)CrystalsType.SPEED] * 0.1f + magicPower.silpidLeap * 0.1f);
            if (weaponType == PlayerWeaponType.Axe)
            {
                returnValue *= 0.8f;
            }
            else if (weaponType == PlayerWeaponType.Dagger)
            {
                returnValue *= 1.1f;
                if (skillInfo.daggerSwift)
                {
                    returnValue *= 1.1f;
                }
                if (skillInfo.daggerComboAttack)
                {
                    returnValue += returnValue * ((daggerSkill2Count * 5) * 0.01f);
                }
            }

            return returnValue;
        }
        set => speed = value;
    }
    public float _crit
    {
        get
        {
            float returnValue = crit;
            returnValue += magicPower.sharpEye * 2;
            if (magicPower.sharpEye >= 1)
            {
                returnValue += 1;
            }
            if (magicPower.sharpEye >= 4)
            {
                returnValue += 1;
            }
            return returnValue;
        }
        set => crit = value;
    }
    public float _def
    {
        get
        {
            float returnValue = def;
            returnValue += Crystals[(int)CrystalsType.DEFFENCE] * 5 + magicPower.ironSkin * 5;
            if (skillInfo.swordIronHeart)
            {
                returnValue *= 1.15f;
            }
            if (hp / maxHp <= 0.3f && skillInfo.swordTenacity)
            {
                returnValue *= 1.3f;
            }
            return returnValue;
        }
        set { def = value; }
    }
    public float _cooldown
    {
        get
        {
            float returnValue = cooldown;
            returnValue += (cooldown * (Crystals[(int)CrystalsType.TIME]) * 0.1f + magicPower.timeQuick * 0.05f);
            return returnValue;
        }
        set { cooldown = value; }
    }
    public float _attackDamage
    {
        get
        {
            float returnValue = attackDamage;
            returnValue += (returnValue * (Crystals[(int)CrystalsType.POWER]) * 0.1f + magicPower.giantPower * 0.15f);

            if (skillInfo.swordTenacity && hp / maxHp <= 0.3f)
                returnValue *= 1.3f;

            if (PlayerDATypeList.TheOneRing)
                returnValue *= 1.6f;
            return returnValue;
        }
        set { attackDamage = value; }
    }
    public float _attackSpeed
    {
        get
        {
            float returnValue = attackSpeed;
            if (weaponType == PlayerWeaponType.Axe)
                returnValue *= 0.8f;
            if (weaponType == PlayerWeaponType.Dagger)
                returnValue *= 1.1f;
            returnValue += returnValue * (Crystals[(int)CrystalsType.ATTACKSPEED] * 0.1f + magicPower.invisibleHand * 0.1f);

            if (skillInfo.daggerComboAttack)
            {
                returnValue += (daggerSkill2Count * 5) * 0.01f;
            }

            if (bloodGauntletDuration > 0)
                return returnValue * 1.5f;
            else
                return returnValue;
        }
        set { attackSpeed = value; }
    }

    readonly float ConDefValue = 50;
    public float ReturnDefValue()
    {
        return 1 - (_def / (_def + ConDefValue));
    }
    public float bloodGauntletDuration;
    public PlayerDAType PlayerDATypeList;
    public int[] Crystals = new int[(int)CrystalsType.END];

    //무기 관련 인덱스

    //죽으면 초기화..
    public bool swordSkill3Boolean = false;

    public int daggerSkill2Count = 0;
    public float daggerSkill2PassedTime = 0;
}
[System.Serializable]
public class PlayerDAType
{
    public bool WindEarRing;
    public bool NeedleArmour;
    public bool KnifeCape;
    public bool CurseKnife;
    public bool BloodGauntlet;
    public bool CrystalOrb;
    public bool TheOneRing;
}
#endregion
public enum PlayerWeaponType
{
    NONE,
    Sword,
    Dagger,
    Axe
}
public enum PlayerState
{
    Idle,
    Walk,
    Dash,
    Attack,
    Jump,
    Die = 6
}
public enum PlayerStateOnAir
{
    NONE,
    FALLING,
    JUMPATTACK
}
[RequireComponent(typeof(Rigidbody2D))]

public class Player : Entity, ITypePlayer
{
    public static Player Instance = null;

    [SerializeField] private GameObject DashShadow;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public AttackCollision[] attackCollisions;

    private float horizontal;
    private Animator animator;
    private new BoxCollider2D collider;
    private PlayerHpView hpView;
    [SerializeField] public PlayerState state;
    PlayerState _state
    {
        get { return state; }
        set
        {
            if (stat.weaponType == PlayerWeaponType.Axe && stateOnAir == PlayerStateOnAir.JUMPATTACK && value != PlayerState.Die)
            {
                return;
            }
            if (state == PlayerState.Die)
                return;
            state = value;
        }
    }
    [SerializeField] PlayerStateOnAir stateOnAir;

    PlayerStateOnAir _stateOnAir
    {
        get { return stateOnAir; }
        set { stateOnAir = value; }
    }
    public PlayerInfo stat;
    public float JumpPower;

    private float curDashCooltime = 0;

    private bool canJump = false;
    public override float _maxHp
    { get => stat._maxHp; set => stat._maxHp = value; }
    public override float _hp
    {
        get => stat._hp;
        set
        {
            if (value < hp)
            {
                if (_state == PlayerState.Dash) return;
                value = (int)(hp - ((hp - value) * stat.ReturnDefValue()));
                value = Mathf.Clamp(value, 0, hp);
            }
            base._hp = stat._hp = value;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        stat._level = stat._level;
        stat._hp = stat._maxHp;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        attackCollisions = GetComponentsInChildren<AttackCollision>();
        hpView = GetComponentInChildren<PlayerHpView>();
        base.Start();
    }
    private void Update()
    {
        InputManager();
        JumpCheck();
        AnimationController();
        PlayerItemContoroller();

        if (hp <= 0)
        {
            _state = PlayerState.Die;
        }
        if (stat._dashCount != stat._curDashCount)
            curDashCooltime += Time.deltaTime;
        if (curDashCooltime > stat.dashCooldown && stat._dashCount > stat._curDashCount)
        {
            curDashCooltime = 0;
            stat._curDashCount = stat._dashCount;
        }
    }
    void FixedUpdate()
    {
        Move();
    }
    void PlayerItemContoroller()
    {
        if (stat.PlayerDATypeList.CurseKnife && cursedKnifeCooldown < curCursedKnifeCooldown)
        {
            cursedKnifeAction();
            curCursedKnifeCooldown = 0;
        }
        if (stat.daggerSkill2PassedTime < 0)
        {
            stat.daggerSkill2Count = 0;
        }
        curCursedKnifeCooldown += Time.deltaTime;
        curWindEarRingDashCooldown += Time.deltaTime;
        stat.bloodGauntletDuration -= Time.deltaTime;
        stat.daggerSkill2PassedTime -= Time.deltaTime;
    }
    void AnimationController()
    {
        if (_stateOnAir != PlayerStateOnAir.NONE && state != PlayerState.Dash)
        {
            state = PlayerState.Jump;
        }
        animator.SetInteger("State", (int)_state);
        animator.SetInteger("StateOnAir", (int)_stateOnAir);
        animator.SetInteger("Weapon", (int)stat.weaponType);
        animator.SetBool("isAttack", isAttack);
        animator.SetFloat("AttackSpeed", stat._attackSpeed);
    }

    public override void Die()
    {
        if (stat.magicPower.magicHeart >= 1 && stat.magicPower.isMagicHeartActive == false)
        {
            stat.magicPower.isMagicHeartActive = true;
            switch (stat.magicPower.magicHeart)
            {
                case 1:
                    _hp = maxHp / 20;
                    break;
                case 2:
                    _hp = maxHp / 50;
                    break;
            }
        }
        state = PlayerState.Die;
    }

    public override void OnHit(Entity entity, float Damage = 0)
    {
        SoundManager.instance.PlaySoundClip("SFX_Hit", SoundType.SFX, 1f);
        if (_state == PlayerState.Dash) return;
        hpView.onHit();
        OnHitEffect.Instance.OnHitFunc();
        rigid.AddForce(new Vector3(entity.transform.position.x > transform.position.x ? -100 : 100, 0));
        if (stat.PlayerDATypeList.NeedleArmour && Random.Range(0, 10) == 0) needleArmourAction(entity);
    }
    #region 플레이어 인풋
    //플레이어 인풋
    void InputManager()
    {
        horizontal = 0;
        if (UI_Manager.Inst != null && UI_Manager.Inst.PlayerMove_control == true) return;
        if (state == PlayerState.Die) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnAttack();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
    }
    private void Dash()
    {
        if (_state != PlayerState.Walk && _state != PlayerState.Idle && _state != PlayerState.Jump) return;
        if (stat._curDashCount > 0)
        {
            stat._curDashCount--;
            StartCoroutine(DashAction());
        }
        else if (stat.PlayerDATypeList.WindEarRing)
        {
            windEarRingAction();
        }
    }
    private IEnumerator DashAction()
    {
        _state = PlayerState.Dash;
        DashSoundPlay();
        float originGravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        Vector3 dir = Vector3.right * (!sprite.flipX ? 1 : -1) / 2;
        var waitSec = new WaitForSeconds(0.01f);

        var enemies = new List<BaseEnemy>();
        for (int i = 0; i < 10; i++)
        {
            if (stat.PlayerDATypeList.KnifeCape)
            {
                knifeCapeAction(enemies);
            }
            if (stat.skillInfo.axeTakle)
            {
                AxeSkill1(enemies);
            }
            if (i % 3 == 1)
                DashShadowCreate();

            rigid.velocity = Vector2.zero;
            transform.position += dir;
            yield return waitSec;
        }
        rigid.gravityScale = originGravity;
        _state = PlayerState.Idle;
    }

    public void DashSoundPlay() => SoundManager.instance.PlaySoundClip("SFX_Dash", SoundType.SFX);

    void DashShadowCreate()
    {
        ObjectPool.Instance.CreateObj(DashShadow, transform.position, Quaternion.identity);
        if (DarkPlayer.Instance != null)
            ObjectPool.Instance.CreateObj(DashShadow, DarkPlayer.Instance.transform.position, Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true;

    }

    private void Jump()
    {
        if (_state == PlayerState.Dash || _state == PlayerState.Attack) return;
        if (canJump == false) return;

        SoundManager.instance.PlaySoundClip("SFX_Jump", SoundType.SFX);

        _state = PlayerState.Jump;
        canJump = false;
        rigid.velocity = new Vector3(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }
    private void JumpCheck()
    {
        RaycastHit2D[] raycasts = Physics2D.BoxCastAll(transform.position, transform.lossyScale, 0, Vector2.down, collider.size.y / 2);
        if (raycasts != null)
        {
            foreach (RaycastHit2D ray in raycasts)
            {
                if (!ray.transform.gameObject.tag.Contains("Platform")) continue;
                if (rigid.velocity.y <= 0.1f)
                {
                    if (_state == PlayerState.Jump || _stateOnAir != PlayerStateOnAir.NONE)
                    {
                        _state = PlayerState.Idle;
                    }
                    canJump = true;
                }
                if (_stateOnAir == PlayerStateOnAir.FALLING || _stateOnAir == PlayerStateOnAir.JUMPATTACK)
                {
                    _stateOnAir = PlayerStateOnAir.NONE;
                }
            }
            if (_stateOnAir != PlayerStateOnAir.JUMPATTACK && raycasts.Length == 1 && raycasts.FirstOrDefault().transform.gameObject == gameObject)
            {
                _stateOnAir = PlayerStateOnAir.FALLING;
            }
        }
    }
    private void Move()
    {
        if (stat.weaponType == PlayerWeaponType.Axe && _stateOnAir == PlayerStateOnAir.JUMPATTACK)
            return;
        if (_state == PlayerState.Dash)
            return;
        if (_state == PlayerState.Attack && _stateOnAir != PlayerStateOnAir.JUMPATTACK)
        {
            PlayerDirFix();
        }
        if (_stateOnAir == PlayerStateOnAir.NONE && state != PlayerState.Jump && _stateOnAir != PlayerStateOnAir.JUMPATTACK)
            _state = PlayerState.Walk;

        PlayerDirFix();

        Vector2 dir = new Vector2(horizontal, 0) * stat._speed * Time.deltaTime;
        transform.Translate(dir);

        if (dir == Vector2.zero && _state == PlayerState.Walk)
            _state = PlayerState.Idle;
    }

    private void PlayerDirFix()
    {
        if (horizontal == 1) sprite.flipX = false;
        if (horizontal == -1) sprite.flipX = true;
    }
    #region 공격관련함수
    bool isAttack;
    Coroutine nowAttackAction;
    public void OnAttack()
    {
        if (_state != PlayerState.Idle && _state != PlayerState.Walk && _state != PlayerState.Attack && _state != PlayerState.Jump)
        {
            return;
        }
        if (_stateOnAir == PlayerStateOnAir.FALLING || state == PlayerState.Jump)
        {
            _stateOnAir = PlayerStateOnAir.JUMPATTACK;
        }
        else
        {
            _state = PlayerState.Attack;
        }
        if (nowAttackAction != null)
            StopCoroutine(nowAttackAction);
        nowAttackAction = StartCoroutine(AttackAction(_stateOnAir));
    }
    public override void Attack(Entity target, float atkDmg)
    {

        Attack(target.GetComponent<BaseEnemy>(), atkDmg);
    }
    public void Attack(BaseEnemy target, float atkDmg)
    {
        bool isCrit = Random.Range(0, 100) < stat._crit;
        if (isCrit)
        {
            atkDmg *= 1.5f;
        }

        //TODO: 매니저에 넣어놔 Load 비용 ㅅㅂ
        DamageText text = ObjectPool.Instance.CreateObj(Resources.Load<GameObject>("Player/DamageText"), target.transform.position
            , Quaternion.Euler(0, 0, target.dimensionType == DimensionType.OVER ? 0 : 180)).GetComponent<DamageText>();
        text.damageValue = atkDmg;
        text.isCrit = isCrit;
        text.dimensionType = target.dimensionType;
        text.Init();

        if (stat.skillInfo.axeEarthQuake)
        {
            target.buffList.stun = 2;
        }

        base.Attack(target, atkDmg);
    }
    public void AnimOnAttack()
    {
        isAttack = true;
    }
    public void AnimEndJumpAttack()
    {
        _stateOnAir = PlayerStateOnAir.FALLING;
    }
    public void AnimNotAttack()
    {
        isAttack = false;
    }
    //해당 함수는 애니메이터에서 호출
    public void AnimAttackFunc(int index)
    {
        if (DarkPlayer.Instance != null)
            DarkPlayer.Instance.OnAttack(stat.weaponType, index);
        //AttackSoundPlay();
        foreach (AttackCollision attackCollision in attackCollisions)
        {
            if (attackCollision.index == index && attackCollision.weaponType == stat.weaponType)
            {
                if (stat.weaponType == PlayerWeaponType.Dagger && Input.GetAxisRaw("Horizontal") == (sprite.flipX ? -1 : 1) && _stateOnAir == PlayerStateOnAir.NONE)
                {
                    rigid.AddForce(Vector2.right * (sprite.flipX ? -1 : 1) * 5, ForceMode2D.Impulse);
                }
                if (stat.weaponType == PlayerWeaponType.Axe)
                {
                    CameraManager.Instance.CameraShake(0.1f, 0.1f, 0.05f);
                }
                attackCollision.OnAttack(this);
            }
        }
    }
    public void AttackSoundPlay()
    {
        switch (stat.weaponType)
        {
            case PlayerWeaponType.Sword:
                SoundManager.instance.PlaySoundClip("SFX_Weapon_Sword", SoundType.SFX);
                break;
            case PlayerWeaponType.Dagger:
                SoundManager.instance.PlaySoundClip("SFX_Weapon_Dagger", SoundType.SFX);
                break;
            case PlayerWeaponType.Axe:
                SoundManager.instance.PlaySoundClip("SFX_Weapon_Axe", SoundType.SFX);
                break;
            default:
                break;
        }
    }

    IEnumerator AttackAction(PlayerStateOnAir onAirState)
    {
        //애니메이션에서 isattack을 true로 바꿈
        yield return new WaitForSeconds(1 / stat._attackSpeed);
        if (onAirState != PlayerStateOnAir.NONE)
        {
            _stateOnAir = PlayerStateOnAir.FALLING;
        }
        else
        {
            _state = PlayerState.Idle;
        }
        isAttack = false;
    }

    #endregion

    #endregion
    #region 플레이어 아이템

    float windEarRingDashCooldown => stat.dashCooldown;
    float curWindEarRingDashCooldown;
    void windEarRingAction()
    {
        if (curWindEarRingDashCooldown > windEarRingDashCooldown)
        {
            curWindEarRingDashCooldown = 0;
            SoundManager.instance.PlaySoundClip("SFX_Dash_Levitation", SoundType.SFX);
            StartCoroutine(DashAction());
        }
    }
    void needleArmourAction(Entity entity)
    {
        Attack(entity, stat._attackDamage / 5);
        entity._hp -= stat._attackDamage / 5;
    }
    void knifeCapeAction(List<BaseEnemy> enemies)
    {
        RaycastHit2D[] rays = Physics2D.BoxCastAll(transform.position, collider.size, 0, Vector2.zero);
        foreach (RaycastHit2D raycast in rays)
        {
            if (raycast.collider.TryGetComponent(out BaseEnemy enemy))
            {
                if (!enemies.Find(x => x == enemy))
                {
                    enemy._hp -= 10;
                    enemies.Add(enemy);
                }
            }
        }
    }
    public float cursedKnifeCooldown = 10;
    float curCursedKnifeCooldown = 0;
    void cursedKnifeAction()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        BaseEnemy target = null;
        float maxDistance = 0;


        foreach (BaseEnemy enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (maxDistance < distance)
            {
                maxDistance = distance;
                target = enemy;
            }
        }

        CursedKnife obj = Instantiate(Resources.Load<CursedKnife>(""), transform.position, Quaternion.identity);
        obj.damage = stat._attackDamage * 2;
        obj.speed = stat._speed * 2;
        obj.rotatePower = 5;
        obj.target = target.gameObject;
    }
    public void BloodGauntletAction(Entity entity)
    {
        if (stat.PlayerDATypeList.BloodGauntlet)
            stat.bloodGauntletDuration = 1.5f;
    }
    public float crystalOrbCoolodwn = 40;
    //private float curCrystalOrbCoolodwn = 0;
    void CrystalOrbAction()
    {
        //겁나 큰 폭*발 (리소스 주면 함 ㄹㅇㅋㅋ)
    }
    void TheOneRing()
    {
        //참격도 리소스 주면 함 ㄹㅇㅋㅋ
    }
    #endregion
    #region 플레이어 장비 스킬
    #region 단검
    //Enemy에서 발동시킴
    public void DaggerSkill2() // WOW
    {
        if (stat.skillInfo.daggerComboAttack)
        {
            stat.daggerSkill2Count++;
            stat.daggerSkill2PassedTime = 5;
            stat.daggerSkill2Count = Mathf.Max(stat.daggerSkill2Count, 5);
        }
    }
    //Animator에서 발동시킴
    public void DaggerSkill3()
    {
        if (stat.skillInfo.daggerHiddenCard)
        {
            StartCoroutine(DaggerSkill3Coroutine());
        }
    }
    IEnumerator DaggerSkill3Coroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj
    = ObjectPool.Instance.CreateObj(Resources.Load<GameObject>("Player/DaggerSkill3Obj"), transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
            obj.GetComponent<DaggerSkillObj>().damage = stat._attackDamage / 3;
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
    #region 도끼
    private float originalGravityValue;
    void AxeStompReady()
    {
        originalGravityValue = rigid.gravityScale;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = -1f;

        Invoke("delayOriginalValue", 1);
    }
    void delayOriginalValue()
    {
        rigid.gravityScale = originalGravityValue;
    }
    void AxeStomp()
    {
        StartCoroutine(AxeStompCoroutine(originalGravityValue));
    }
    IEnumerator AxeStompCoroutine(float originalValue)
    {
        Vector2 dir = new Vector2(0, -originalValue * 10);

        while (_stateOnAir != PlayerStateOnAir.NONE)
        {
            rigid.velocity = dir;
            yield return null;
        }
        CameraManager.Instance.CameraShake(0.1f, 0.2f, 0.03f);
        AnimAttackFunc(10);
        rigid.gravityScale = originalValue;
    }
    void AxeSkill1(List<BaseEnemy> enemies)
    {
        float forcePower = 30;

        RaycastHit2D[] rays = Physics2D.BoxCastAll(transform.position, collider.size, 0, Vector2.zero);
        foreach (RaycastHit2D raycast in rays)
        {
            if (raycast.collider.TryGetComponent(out BaseEnemy enemy))
            {
                if (!enemies.Find(x => x == enemy))
                {
                    enemy._hp -= stat._attackDamage / 4;
                    enemy.GetComponent<Rigidbody2D>().AddForce(sprite.flipX ? Vector2.left : Vector2.right * forcePower);
                    enemies.Add(enemy);
                }
            }
        }
    }
    public void AxeSkill2(List<BaseEnemy> enemies)
    {
        foreach (BaseEnemy enemy in enemies)
        {
            enemy.buffList.stun = 2;
        }
    }
    void AxeSkill3()
    {
        //리소스 들어오면 제작 시작
    }

    #endregion
    #endregion
}
