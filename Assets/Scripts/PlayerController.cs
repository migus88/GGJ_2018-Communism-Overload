using System.Collections;
using migs.EventSystem;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : BaseBehaviour
{
    public float Speed = 0f;
    public float CurrentSpeed = 0f;
    public float StoppingTrashold = 1f;
    public float JumpingHeight = 0.3f;
    public float SpeedInc = 5f;
    public float SlippingTime = 1.5f;
    public KeyCode LeftButton = KeyCode.LeftArrow;
    public KeyCode RightButton = KeyCode.RightArrow;
    public KeyCode UpButton = KeyCode.UpArrow;
    public GameObject ShitTrail;

    private DirectionKey _lastKeyPressed;
    private Rigidbody2D _body;
    private bool _startedMoving = false;
    private float _lastTap = 0f;
    private bool _isGrounded = true;
    private float _startingY;
    private readonly List<KeyValuePair<float, KeyCode>> _tapQueue = new List<KeyValuePair<float, KeyCode>>();
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
        this._startingY = this._body.transform.position.y;
        //TEST
        //this._canMove = true;
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
            this._isSlipping = true;
            this._anim.SetBool("IsSliding", true);
            StartCoroutine(this.slip());
        }

        if (collider.gameObject.layer == 10) //Enemy Layer
        {
            this.die();
        }

        if (this._isUntouchable)
            return;

        if (collider.gameObject.layer == 8 || collider.gameObject.layer == 11) //Player layer and obstacles layer
        {
            this._isSlipping = false;
            StopCoroutine("slip");
            this.stopSlipping();
            this._body.velocity = new Vector2(0, 0);
            this._body.isKinematic = true;
            this._anim.SetTrigger("FallTrigger");
            this._audio.time = 0;
            this._audio.Play();
            this._isUntouchable = true;
            StartCoroutine(this.convertToTouchable());
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
            this._body.velocity = new Vector2(0, 0);
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
        CurrentSpeed = Speed * 1.5f;
        ShitTrail.gameObject.SetActive(true);
        yield return new WaitForSeconds(SlippingTime);
        this.stopSlipping();
    }

    private void stopSlipping()
    {
        if (ShitTrail != null)
            ShitTrail.gameObject.SetActive(false);
        CurrentSpeed = Speed;
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
        this.run();
    }

    private void run()
    {
        if (this._isSlipping)
            return;

        if (!this._canMove || this._body.isKinematic)
        {
            this._anim.SetBool("IsRunning", false);
            return;
        }

        CurrentSpeed = CurrentSpeed < 0 ? 0 : CurrentSpeed;

        //this._isGrounded = this._body.transform.position.y <= this._startingY + 0.02f;

        this._startedMoving = false;
        this._body.drag = 1f;

        if (Input.GetKeyDown(LeftButton) || Input.GetKeyDown(RightButton))
        {
            var currentKey = Input.GetKeyDown(LeftButton) ? LeftButton : RightButton;

            this._tapQueue.Add(new KeyValuePair<float, KeyCode>(Time.time, currentKey));

            KeyValuePair<float, KeyCode>? prev = null;

            if (this._tapQueue.Count >= 2)
            {
                prev = this._tapQueue[this._tapQueue.Count - 2];
            }

            if (prev != null && prev.Value.Value != currentKey)
            {
                CurrentSpeed += SpeedInc;
                CurrentSpeed = CurrentSpeed > Speed ? Speed : CurrentSpeed;
                this._startedMoving = true;
            }


            if (prev == null || prev.Value.Value == currentKey || Time.time - prev.Value.Key >= StoppingTrashold)
            {
                this._body.drag = 5f;

                //this._body.velocity = new Vector2(this._body.velocity.x, 0);
                this._anim.SetBool("IsRunning", false);
            }
        }

        /*CurrentSpeed -= SlowdownSpeed * Time.deltaTime;
        CurrentSpeed = CurrentSpeed < 0 ? 0 : CurrentSpeed;*/

        if (this._tapQueue.Count > 0 && Time.time - this._tapQueue[this._tapQueue.Count - 1].Key >= StoppingTrashold)
        {
            //this._body.drag = 5f;
            this._anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(UpButton) && this._isGrounded)
        {
            Debug.Log("Jumping");
            //this._body.velocity = new Vector2(this._body.velocity.x, 0);
            this._body.AddForce(transform.up * JumpingHeight);
            this._isGrounded = false;
            this._anim.SetTrigger("JumpTrigger");
        }


        /*else
        {
            this._anim.SetBool("IsRunning", false);
        }*/
    }

    private void FixedUpdate()
    {
        if (this._tapQueue.Count >= 2 && !this._isSlipping)
        {
            var last = this._tapQueue[this._tapQueue.Count - 2].Key;
            var diff = 1f - (Time.time - last);

            CurrentSpeed *= diff;
            //Debug.Log($"Diff = {diff}; CurrentSpeed = {CurrentSpeed}");
        }

        if (this._startedMoving || this._isSlipping)
        {
            if (!this._isSlipping)
                this._anim.SetBool("IsRunning", true);
            this._body.velocity = new Vector2(0, this._body.velocity.y);
            this._body.AddForce(transform.right * CurrentSpeed);
        }
    }

    public void OnPlayerGotUp()
    {
        this._body.isKinematic = false;
    }


    private enum DirectionKey { Left, Right };
}