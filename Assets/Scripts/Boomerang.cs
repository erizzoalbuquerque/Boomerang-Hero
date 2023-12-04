using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Hero _hero;

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

    Vector2 _velocity, _desiredVelocity;

    float _modifiedBaseFlyingTime;

    bool _isThrown;
    bool _isPulling;

    bool _canBePutAway;
    bool _canBePulled;

    float _timeSinceThrown = 0f;

    public event Action dashStarted;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _isThrown = false;
        _isPulling = false;
        _canBePutAway = false;
        _canBePulled = false;

        _timeSinceThrown = 0f;

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

        Vector2 newDesiredVelocity;
        Vector2 newVelocity;
        if (_isPulling == false)
        {
            float modifiedRegularMaxSpeed = _regularMaxSpeed * _maxSpeedMultiplierCurve.Evaluate(Mathf.Clamp01(_timeSinceThrown / _modifiedBaseFlyingTime));
            float modifiedRegularTurningSpeed = _regularTurningSpeed * _turningSpeedMultiplierCurve.Evaluate(Mathf.Clamp01(_timeSinceThrown / _modifiedBaseFlyingTime));

            newDesiredVelocity = modifiedRegularMaxSpeed * heroDirection;
            newVelocity = RotateTowards(currentVelocity, newDesiredVelocity, modifiedRegularTurningSpeed * Time.deltaTime);
        }
        else
        {
            newDesiredVelocity = _pullingMaxSpeed * heroDirection;
            newVelocity = newDesiredVelocity;
        }
            


        _rb.velocity = newVelocity;

        _desiredVelocity = newDesiredVelocity;
        _velocity = newVelocity;
    }


    Vector2 RotateTowards(Vector2 current, Vector2 target, float maxRadiansDelta)
    {
        float angle = Vector2.SignedAngle(current, target);

        // I want to favor clockwise movement.
        if (angle == 180f)
        {
            angle = -180;
        }

        Vector2 newDirection = Quaternion.AngleAxis(Mathf.Clamp(angle,-maxRadiansDelta * Mathf.Rad2Deg,maxRadiansDelta * Mathf.Rad2Deg ), Vector3.forward) * current;

        newDirection = newDirection.normalized * target.magnitude;

        return newDirection;
    }


    public void Throw(Vector2 direction, Vector2 startPosition) 
    {
        if (_isThrown == false)
        {   
            Activate(direction,startPosition);
        }
    }

    void PutAway()
    {
        print("Put Away()");
        Deactivate();
    }

    void Activate(Vector2 direction, Vector2 startPosition)
    {
        this.transform.position = startPosition;

        _rb.simulated = true;

        _velocity = direction.normalized * _regularMaxSpeed;
        _rb.velocity = _velocity;

        _isPulling = false;

        for (int i=0; i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _canBePulled = false;
        Invoke("SetTrueCanBePulled", _secondsBeforeCanBePulled);


        _canBePutAway = false;
        Invoke("SetTrueCanBePutAway", _secondsBeforeCanBePutAway);

        _timeSinceThrown = 0f;
        _modifiedBaseFlyingTime = _baseFlyingTime;

        _isThrown = true;
    }

    void Deactivate()
    {
        _canBePutAway = true;

        _rb.simulated = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _isThrown = false;
    }

    void SetTrueCanBePutAway()
    {
        _canBePutAway = true;
    }

    void SetTrueCanBePulled()
    {
        _canBePulled = true;
    }

    public void Pull(bool pull)
    {
        if (pull && _canBePulled)
        {
            _isPulling = true;
            dashStarted.Invoke();
        }
        else
        {
            _isPulling = false;
        }
    }

    void Bounce(Vector3 normalDirection)
    {
        _rb.velocity = Vector2.Reflect(_velocity, normalDirection);

        _timeSinceThrown = 0f;
        _modifiedBaseFlyingTime = _baseFlyingTime * _baseFlyingMultiplierAfterBounce;

        _isPulling = false;
        
        _canBePulled = false;
        Invoke("SetTrueCanBePulled", _secondsBeforeCanBePulled);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ReceiveHit(-collision.GetContact(0).normal);
        }

        Bounce(collision.GetContact(0).normal);
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
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(_velocity.normalized.x, _velocity.normalized.y, 0f) * _gizmosSize);
    }
}
