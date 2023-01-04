using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowMage : BaseEnemy
{
    public bool isShielding = false;
    [SerializeField] GameObject ShieldObj;
    [SerializeField] GameObject meteorObj;
    [SerializeField] GameObject SpaceBoomObj;
    [SerializeField] AttackProjectile HommingProjectile;

    [SerializeField] RectTransform HealthFillImage;
    public override float _hp
    {
        get => base._hp;
        set
        {
            if (isShielding) return;
            base._hp = value;
        }
    }
    public override EnemyState _state
    {
        get => base._state;
        set
        {
            if (state == EnemyState.DIE) return;
            base._state = value;
        }
    }
    protected override void Start()
    {
        BaseStatSet(2000, 0, 0, 3, 0, 0, 0, 0);
        base.Start();
    }
    protected override void Update()
    {
        AnimController();
        if (player._hp <= 0)
        {
            _state = EnemyState.HIT;
            return;
        }
        if (_state == EnemyState.HIT)
            curAttackDelay = 0;

        HealthFillImage.localScale = new Vector2(1, hp / maxHp);

        curAttackDelay += Time.deltaTime;
        buffList.stun -= Time.deltaTime;
    }
    public override void OnObjCreate()
    {
        player = Player.Instance;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        attackCollisions = GetComponentsInChildren<AttackCollision>();

        Debug.Log(gameObject.name);
        hp = _maxHp;
        sprite.color = Color.white;

        StartCoroutine(BossPatternFunc());
    }
    public override void Move()
    {
        if (_state != EnemyState.MOVE) return;
        if (attackCollisions[0].isCanAttack(this))
        {
            _state = EnemyState.IDLE;
            return;
        }
        float dir;

        if (player.transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
            dir = 1;
        }
        else
        {
            sprite.flipX = true;
            dir = -1;
        }

        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }
    IEnumerator BossPatternFunc()
    {
        WaitForSeconds waitSeconds = new WaitForSeconds(1);
        yield return waitSeconds;

        while (state != EnemyState.DIE)
        {
            _state = EnemyState.IDLE;
            if (Random.Range(0, 2) == 0 && !isShielding)
            {
                yield return StartCoroutine(DimensionLeapCoroutine());
            }
            _state = EnemyState.MOVE;
            yield return waitSeconds;

            _state = EnemyState.ATTACK;
            int PatternValue = Random.Range(0, 100);
            if (PatternValue > 70 && isShielding == false)
            {
                yield return StartCoroutine(Shield());
            }
            else if (PatternValue > 40)
            {
                yield return StartCoroutine(HommingMissile());
            }
            else if (PatternValue > 20)
            {
                yield return StartCoroutine(Meteor());
            }
            else
            {
                yield return StartCoroutine(SpaceBoom());
            }
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator SpaceBoom()
    {
        SpriteRenderer sprite = ObjectPool.Instance.CreateObj(SpaceBoomObj, new Vector3(Player.Instance.transform.position.x, 0), Quaternion.identity).GetComponent<SpriteRenderer>();
        sprite.flipY = dimensionType == DimensionType.UNDER;
        yield return null;
    }
    IEnumerator Meteor()
    {
        for (int i = 0; i < 3; i++)
        {
            meteorObj.GetComponent<AttackAlert>().dimensionType = dimensionType;
            ObjectPool.Instance.CreateObj(meteorObj, dimensionType == DimensionType.OVER ? Player.Instance.transform.position : DarkPlayer.Instance.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator HommingMissile()
    {
        AttackProjectile attackProjectile = ObjectPool.Instance.CreateObj(HommingProjectile.gameObject, transform.position, Quaternion.identity).GetComponent<AttackProjectile>();
        attackProjectile.Init(this, 10, 10, 1, ProjectileType.Homming, dimensionType == DimensionType.OVER ? Player.Instance.gameObject : DarkPlayer.Instance.gameObject, 10);
        yield return null;
    }
    IEnumerator Shield()
    {
        GameObject obj = Instantiate(ShieldObj, transform);
        obj.GetComponent<SpriteRenderer>().flipY = dimensionType == DimensionType.UNDER;
        isShielding = true;
        yield return null;
    }
    IEnumerator DimensionLeapCoroutine()
    {
        float value = 1;
        while (value > 0)
        {
            value -= Time.deltaTime;
            transform.localScale = new Vector3(1, value, 1);
            yield return null;
        }
        dimensionType = (dimensionType == DimensionType.OVER ? DimensionType.UNDER : DimensionType.OVER);
        sprite.flipY = !sprite.flipY;
        transform.position = new Vector3(transform.position.x + Random.Range(-10, 10), -transform.position.y);
        rigid.gravityScale = -rigid.gravityScale;
        while (value < 1)
        {
            value += Time.deltaTime;
            transform.localScale = new Vector3(1, value, 1);
            yield return null;
        }
    }
    public override void OnHit(Entity atkEntity, float Damage)
    {
        if (isShielding) return;
        rigid.AddForce(new Vector3(transform.position.x > atkEntity.transform.position.x ? 40 : -40, 0, 0));
        StartCoroutine(HitEffectCoroutine());
    }
    public override void OnDieActionAdd()
    {
        onDie += () => ObjectPool.Instance.CreateObj(GameManager.Instance.DropGoods, transform.position, transform.rotation);
        onDie += () => Player.Instance.DaggerSkill2(); // HOLY SHIT
        onDie += () => CameraManager.Instance.CameraShake(1f, 0.6f, 0.05f);
        onDie += () => player.BloodGauntletAction(this);
        onDie += () => _state = EnemyState.DIE;
        onDie += () => DropHealingOrbObj();
    }
    void OnDelete()
    {
        ObjectPool.Instance.DeleteObj(gameObject);
        UIManager.instance.Die_System();
    }
}
