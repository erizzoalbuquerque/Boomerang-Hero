using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] HeroController _hero;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Cinemachine.CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] GameObject _hideWhenInactiveObject;
    [SerializeField] GameObject _boomerangImpactSmallPrefab;
    [SerializeField] GameObject _boomerangImpactBigPrefab;
    [SerializeField] BoomerangTeleportParticles _boomerangTeleportParticles;

    [Header("Regular Settings")]
    [SerializeField] float _regularMaxSpeed = 1f;
    [SerializeField] float _regularTurningSpeed = 1f;

    [Header("Pulling Settings")]
    [SerializeField] float _pullingMaxSpeed = 1f;

    [Header("Flying Settings")]
    [SerializeField] float _expectedFlyingDuration = 5f;
    [SerializeField] float _flyingDurationFractionAfterBounce = 0.5f;
    [SerializeField] AnimationCurve _maxSpeedMultiplierCurve;
    [SerializeField] AnimationCurve _turningSpeedMultiplierCurve;

    [Header("Details")]
    [SerializeField] float _secondsBeforeCanBePutAway = 1f;
    [SerializeField] float _secondsBeforeCanBePulled = 0.1f;
    [SerializeField] int _consecutiveHitsOnSolidObjectsToBreak = 5;
    [SerializeField] float _cooldownAfterBroke = 1f;

    [Header("Debug")]
    [SerializeField] bool _debug = false;
    [SerializeField] float _gizmosSize = 0.5f;

    Vector2 _velocityAfterUpdate;
    Vector2 _desiredVelocity;

    float _modifiedFlyingDuration;

    bool _isThrown;
    bool _isBeingPulled;

    bool _canBePutAway;
    bool _canBePulled;

    float _timeSinceThrown = 0f;

    int _consecutiveHitsOnEnemies = 0;
    int _consecutiveHitsOnSolidObjects = 0;

    Vector3 _lastActivePosition;

    float _lastBreakTime = -1000f;

    public bool IsThrown { get => _isThrown;}
    public bool IsBeingPulled { get => _isBeingPulled; }
    public bool CanBePutAway { get => _canBePutAway; }
    public bool CanBePulled { get => _canBePulled; }


    // Reserved Methods
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Deactivate();
    }


    void Update()
    {

    }

    void FixedUpdate()
    {
        if (_isThrown)
        {
            Move();

            _timeSinceThrown += Time.deltaTime;

            if ((this.transform.position - _hero.transform.position).magnitude < 1f && _canBePutAway)
            {
                Deactivate();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        Vector2 collisionNormal = collision.GetContact(0).normal;

        if (collision.gameObject.tag == "Enemy")
        {

            DamageHitbox enemyHitbox = collision.gameObject.GetComponent<DamageHitbox>();

            if (enemyHitbox != null)
            {
                HitEnemy(enemyHitbox, collisionNormal);
            }
        }
        else
        {
            HitSolid(collisionNormal);
        }

        Bounce(collisionNormal);
    }


    void OnDrawGizmos()
    {
        if (_debug == false || _isThrown == false)
            return;

        //Green direction input
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_desiredVelocity.normalized.x, _desiredVelocity.normalized.y, 0f) * _gizmosSize);

        //Blue velocity
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_velocityAfterUpdate.normalized.x, _velocityAfterUpdate.normalized.y, 0f) * _gizmosSize);
    }

    //Public Methods

    public void Throw(Vector2 direction, Vector2 startPosition)
    {
        if (_isThrown == false)
        {
            if (Time.time - _lastBreakTime < _cooldownAfterBroke)
                return;

            Activate(direction, startPosition);
        }
    }


    public void Pull(bool pull)
    {
        _isBeingPulled = pull && _canBePulled;
    }



    //Private Methods

    void Move()
    {
        Vector2 currentVelocity = _rb.velocity;

        Vector2 heroDirection = (_hero.transform.position - this.transform.position).normalized;

        Vector2 desiredVelocity;
        Vector2 newVelocity;

        if (_isBeingPulled)
        {
            // Go straight to hero
            desiredVelocity = _pullingMaxSpeed * heroDirection;
            newVelocity = desiredVelocity;

        }
        else
        {
            float evalValue = Mathf.Clamp01(_timeSinceThrown / _modifiedFlyingDuration);

            float modifiedRegularMaxSpeed = _regularMaxSpeed * _maxSpeedMultiplierCurve.Evaluate(evalValue);
            float modifiedRegularTurningSpeed = _regularTurningSpeed * _turningSpeedMultiplierCurve.Evaluate(evalValue);

            desiredVelocity = modifiedRegularMaxSpeed * heroDirection;

            float maxDeltaAngle = modifiedRegularTurningSpeed * Time.deltaTime;
            newVelocity = UnityExtensions.RotateTowards(currentVelocity, desiredVelocity, maxDeltaAngle);
        }
 
        _rb.velocity = newVelocity;

        // Store last veolocity before collision
        _velocityAfterUpdate = newVelocity;

        // Stored for debug
        _desiredVelocity = desiredVelocity;

        _lastActivePosition = this.transform.position;
    }

    void Activate(Vector2 direction, Vector2 startPosition)
    {
        // Set initial boomerang position
        this.transform.position = startPosition;

        // Turn on physics
        _rb.simulated = true;

        //Detach from hero
        this.transform.parent = null;

        // Set velocity vector 2
        _rb.velocity = direction.normalized * _regularMaxSpeed;

        // Set internal state to know boomerang is not being pulled
        _isBeingPulled = false;

        // Activate all components
        for (int i=0; i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        // Prohibit user from pulling the boomerang before delay
        _canBePulled = false;
        Invoke("SetTrueCanBePulled", _secondsBeforeCanBePulled);

        // Prohibit user from putting away the boomerang before delay
        _canBePutAway = false;
        Invoke("SetTrueCanBePutAway", _secondsBeforeCanBePutAway);

        // Setting up variables
        _timeSinceThrown = 0f;
        _consecutiveHitsOnEnemies = 0;
        _consecutiveHitsOnSolidObjects = 0;

        // Flying initial state
        _modifiedFlyingDuration = _expectedFlyingDuration;

        _isThrown = true;
    }


    void Deactivate()
    {
        _isThrown = false;
        _isBeingPulled = false;
        _canBePutAway = false;
        _canBePulled = false;

        // Turn off physics
        _rb.simulated = false;

        //Attach to Hero
        this.transform.position = _hero.transform.position;
        this.transform.parent = _hero.transform;


        _hideWhenInactiveObject.SetActive(false);
    }


    void SetTrueCanBePutAway()
    {
        _canBePutAway = true;
    }


    void SetTrueCanBePulled()
    {
        _canBePulled = true;
    }    


    void HitEnemy(DamageHitbox enemyHitbox, Vector2 collisionNormal)
    {
        _consecutiveHitsOnEnemies++;
        _consecutiveHitsOnEnemies = Mathf.Clamp(_consecutiveHitsOnEnemies, 0, 3);

        if (_audioSource != null)
        {
            _audioSource.pitch = 1f + _consecutiveHitsOnEnemies * 0.3f;
            _audioSource.Play();
        }

        float intensity;
        if (_consecutiveHitsOnEnemies == 1)
        {
            intensity = 1;
        }
        else if (_consecutiveHitsOnEnemies == 2)
        {
            intensity = 2;
            _cinemachineImpulseSource?.GenerateImpulseWithForce(0.07f);
            if (_boomerangImpactSmallPrefab != null)
                Instantiate(_boomerangImpactSmallPrefab, this.transform.position, Quaternion.identity);
        }
        else
        {
            intensity = 3;
            _cinemachineImpulseSource?.GenerateImpulseWithForce(0.15f);
            if (_boomerangImpactSmallPrefab != null)
                Instantiate(_boomerangImpactSmallPrefab, this.transform.position, Quaternion.identity);
            if (_boomerangImpactBigPrefab != null)
                Instantiate(_boomerangImpactBigPrefab, this.transform.position, Quaternion.identity);
        }

        enemyHitbox.Hit(new HitInfo(-collisionNormal,intensity));
    }

    void HitSolid(Vector2 collisionNormal)
    {
        _consecutiveHitsOnSolidObjects++;

        if (_consecutiveHitsOnSolidObjects < _consecutiveHitsOnSolidObjectsToBreak)
        {
            Bounce(collisionNormal);
        }
        else
        {
            BreakBoomerang();
        }
    }

    void Bounce(Vector3 normalDirection)
    {
        _rb.velocity = Vector2.Reflect(_velocityAfterUpdate, normalDirection);

        _timeSinceThrown = 0f;
        _modifiedFlyingDuration = _expectedFlyingDuration * _flyingDurationFractionAfterBounce;

        _isBeingPulled = false;

        _canBePulled = false;
        Invoke("SetTrueCanBePulled", _secondsBeforeCanBePulled);
    }

    void BreakBoomerang()
    {
        if (!_isThrown)
            return;

        Deactivate();
        _boomerangTeleportParticles.Play(_lastActivePosition);

        _lastBreakTime = Time.time;
    }       
}
