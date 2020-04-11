using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivillianController : MonoBehaviour
{
    private Vector3 _randomPos;
    [SerializeField] private Sprite _hitSprite;
    private bool _moving = true;
    private bool _isHit;
    public bool IsHit { get => _isHit; }
    private PlayerMovement _player;
    private bool _hasPosition;
    private bool _hasSquashed = true;
    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;
    private Vector3 _position;
    private float _radius = 2;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>(4);

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    [SerializeField] private float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _randomPos = new Vector3(Random.insideUnitCircle.x*10,Random.insideUnitCircle.y*10,0);

        _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_rb.velocity);
        _rb.velocity = Vector2.one * _speed * Time.deltaTime;
        _player = FindObjectOfType<PlayerMovement>();
        if(!_isHit)
        {
            if (_moving)
            {
                _rb.MovePosition(Vector2.Lerp(transform.position, _randomPos, Time.deltaTime * _speed));

                if (_hasSquashed)
                {
                    StartCoroutine(Squash());
                }
            }

            float distance = Vector3.Distance(transform.position,_randomPos);
            if(distance<1)
            {
                _moving = false;
                _randomPos =Random.insideUnitCircle*10;
            }
        }
        else FollowLeader();
    }
    void FollowLeader()
    {
        if (_hasSquashed)
        {
            StartCoroutine(Squash());
        }
     //   transform.parent = _player.transform;
     //   
     //   if(_player._followers.Capacity>=10)
     //   {
     //       _radius =4;
     //   }
     //   if(!_hasPosition)
     //   {
     //       _player._followers.Add(gameObject);
     //       GetComponent<AudioSource>().clip = _clips[Random.Range(0,4)];
     //       GetComponent<AudioSource>().Play();
     //       _position = new Vector3((Random.insideUnitCircle.x*_radius),(Random.insideUnitCircle.y*_radius),0);
     //       _hasPosition = true;
     //   }
     //   if(Vector3.Distance(transform.transform.position, _position)>_radius-0.2f)
     //   {
     //   transform.localPosition = Vector3.Lerp(transform.localPosition,_position,Time.deltaTime);
     //   }
     //   if(transform.parent.GetComponentInChildren<SpriteRenderer>().flipX)
     //       _spriteRenderer.flipX = true;
     //   else _spriteRenderer.flipX = false;
        


    }
    public void Hit()
    {
        _spriteRenderer.sprite = _hitSprite;
        FindObjectOfType<Flock>().agents.Add(GetComponent<Agent>());
        GetComponentInChildren<SpriteFlock>().isEnabled = true;
        gameObject.layer = 14;
        transform.GetChild(0).gameObject.layer = 14;
        _isHit = true;
    }

    IEnumerator Squash()
    {
        float time = 0;
        _hasSquashed = false;


        float sin = 0;

        while (sin >= 0)
        {
             sin = _Squash * Mathf.Sin(time * Frequency);
            transform.localScale =Vector3.Scale(Vector3.one + new Vector3(-sin, sin, -sin),Vector3.one*2);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _hasSquashed = true;
        yield return null;
    }
}
