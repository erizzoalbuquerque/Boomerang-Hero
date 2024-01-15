using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Boomerang : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Hero _hero;
    [SerializeField] AudioSource _audioSource;

    [Header("Regular Settings")]
    [SerializeField] float _regularMaxSpeed = 1f;
    [SerializeField] float _regularTurningSpeed = 1f;

    [Header("Pulling Settings")]
    [SerializeField] float _pullingMaxSpeed = 1f;

    [Header("Flying Settings")]
    [SerializeField] float _baseFlyingTime = 5f;
    [SerializeField] float _baseFlyingMultiplierAfterBounce = 0.5f;
    [SerializeField] AnimationCurve _maxSpeedMultiplierCurve;
    [SerializeField] AnimationCurve _turningSpeedMultiplierCurve;

    [Header("Details")]
    [SerializeField] float _secondsBeforeCanBePutAway = 1f;
    [SerializeField] float _secondsBeforeCanBePulled = 0.1f;

    [Header("Debug")]
    [SerializeField] bool _debug = false;
    [SerializeField] float _gizmosSize = 0.5f;

    Vector2 _velocityAfterUpdate;
    Vector2 _desiredVelocity;

    float _modifiedBaseFlyingTime;

    bool _isThrown;
    bool _isBeingPulled;

    bool _canBePutAway;
    bool _canBePulled;

    float _timeSinceThrown = 0f;

    int _consecutiveHits = 0;


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
            float evalValue = Mathf.Clamp01(_timeSinceThrown / _modifiedBaseFlyingTime);

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
    }


    public void Throw(Vector2 direction, Vector2 startPosition) 
    {
        if (_isThrown == false)
        {   
            Activate(direction, startPosition);
        }
    }


    public void Pull(bool pull)
    {
        _isBeingPulled = pull && _canBePulled;
    }


    void Activate(Vector2 direction, Vector2 startPosition)
    {
        // Set initial boomerang position
        this.transform.position = startPosition;

        // Turn on physics
        _rb.simulated = true;

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

        _timeSinceThrown = 0f;

        // Flying initial state
        _modifiedBaseFlyingTime = _baseFlyingTime;

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

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _consecutiveHits = 0;
    }


    void SetTrueCanBePutAway()
    {
        _canBePutAway = true;
    }


    void SetTrueCanBePulled()
    {
        _canBePulled = true;
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
                print("Achou hitbox");
                HitEnemy(enemyHitbox, -collisionNormal);
            }
        }

        Bounce(collisionNormal);
    }


    void HitEnemy(DamageHitbox enemyHitbox, Vector2 direction)
    {
        enemyHitbox.Hit(new HitInfo(direction));

        _consecutiveHits = Mathf.Clamp(_consecutiveHits++, 0, 3);

        if (_audioSource != null)
        {
            _audioSource.pitch = 1f + _consecutiveHits * 0.3f;
            _audioSource.Play();
        }
    }


    void Bounce(Vector3 normalDirection)
    {
        _rb.velocity = Vector2.Reflect(_velocityAfterUpdate, normalDirection);

        _timeSinceThrown = 0f;
        _modifiedBaseFlyingTime = _baseFlyingTime * _baseFlyingMultiplierAfterBounce;

        _isBeingPulled = false;

        _canBePulled = false;
        Invoke("SetTrueCanBePulled", _secondsBeforeCanBePulled);
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
}
