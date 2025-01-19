using System.Collections;
using migs.EventSystem;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : BaseBehaviour
{
    private static readonly int IsRunningState = Animator.StringToHash("IsRunning");
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");

    public float Speed = 0f;
    public float StoppingTrashold = 1f;
    public float JumpingHeight = 0.3f;
    public float SlippingTime = 1.5f;
    public KeyCode LeftButton = KeyCode.LeftArrow;
    public KeyCode RightButton = KeyCode.RightArrow;
    public KeyCode UpButton = KeyCode.UpArrow;
    public GameObject ShitTrail;
    
    [Header("New Movement")]
    [SerializeField] private float _velocityIncrement = 0.2f;
    [SerializeField] private float _maxVelocity = 3f;

    private DirectionKey _lastKeyPressed;
    private Rigidbody2D _body;
    private bool _isGrounded = true;
    private bool _canMove = false;
    private Animator _anim;
    private bool _isUntouchable = false;
    private AudioSource _audio;
    private bool _isSlipping;

    private void Awake()
    {
        this._anim = GetComponent<Animator>();
        this._audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStatePayload));
        this._body = GetComponent<Rigidbody2D>();
    }

    private void onGameStatePayload(GameStatePayload obj)
    {
        if (obj.State == GameState.Started)
        {
            this._canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 14) //Shit layer
        {
            Destroy(collider.gameObject);
            _isSlipping = true;
            _anim.SetBool("IsSliding", true);
            StartCoroutine(slip());
        }

        if (collider.gameObject.layer == 10) //Enemy Layer
        {
            die();
        }

        if (_isUntouchable)
            return;

        if (collider.gameObject.layer == 8 || collider.gameObject.layer == 11) //Player layer and obstacles layer
        {
            _isSlipping = false;
            StopCoroutine(slip());
            stopSlipping();
            _body.velocity = new Vector2(0, _body.velocity.y);
            _currentVelocity = 0;
            _body.isKinematic = true;
            _anim.SetTrigger("FallTrigger");
            _audio.time = 0;
            _audio.Play();
            _isUntouchable = true;
            StartCoroutine(convertToTouchable());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) //Floor layer
        {
            this._isGrounded = true;
        }

        if (collision.gameObject.layer == 10) //Enemy Layer
        {
            this.die();
        }

        if (this._isUntouchable)
            return;

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11) //Player layer and obstacles layer
        {
            _currentVelocity = 0;
            this._body.velocity = new Vector2(0, _body.velocity.y);
            this._body.isKinematic = true;
            this._anim.SetTrigger("FallTrigger");
            this._audio.time = 0;
            this._audio.Play();
            this._isUntouchable = true;
            StartCoroutine(this.convertToTouchable());
        }
    }

    private IEnumerator slip()
    {
        _currentVelocity = _maxVelocity * 1.5f;
        
        ShitTrail.gameObject.SetActive(true);
        yield return new WaitForSeconds(SlippingTime);
        this.stopSlipping();
    }

    private void stopSlipping()
    {
        if (ShitTrail != null)
            ShitTrail.gameObject.SetActive(false);

        _currentVelocity = _maxVelocity;
        this._isSlipping = false;
        this._anim.SetBool("IsSliding", false);
    }

    private void die()
    {
        GameManager.Instance.State = GameState.Lose;
    }

    private IEnumerator convertToTouchable()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
        }

        this._isUntouchable = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        if(!_body.isKinematic)
        {
            _body.velocity = new Vector2(_currentVelocity, _body.velocity.y);
        }
        
        if(_isJumping)
        {
            _isJumping = false;
            _isGrounded = false;
            
            _body.AddForce(transform.up * JumpingHeight, ForceMode2D.Impulse);
            _anim.SetTrigger(JumpTrigger);
        }
    }

    private KeyCode _previousKeyPressed;
    private float _currentVelocity;
    private float _lastButtonPress;
    private bool _isJumping;

    private void HandleJumpInput()
    {
        if (!Input.GetKeyDown(UpButton) || !this._isGrounded)
        {
            return;
        }
        
        _isJumping = true;
    }
    
    private void HandleMovementInput()
    {
        if (_isSlipping)
            return;
        
        if (!_canMove || _body.isKinematic)
        {
            _anim.SetBool(IsRunningState, false);
            return;
        }
        
        if(Time.time - _lastButtonPress > StoppingTrashold)
        {
            _currentVelocity = 0;
            _anim.SetBool(IsRunningState, false);
        }

        if (!Input.GetKeyDown(LeftButton) && !Input.GetKeyDown(RightButton))
        {
            return;
        }
        
        _lastButtonPress = Time.time;
        var currentKeyPressed = Input.GetKeyDown(LeftButton) ? LeftButton : RightButton;
        
        if(_previousKeyPressed == currentKeyPressed)
        {
            return;
        }
        
        _anim.SetBool(IsRunningState, true);
        _currentVelocity = Math.Min(_maxVelocity, _currentVelocity + _velocityIncrement);
    }

    public void OnPlayerGotUp()
    {
        this._body.isKinematic = false;
    }


    private enum DirectionKey { Left, Right };
}