using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivillianController : MonoBehaviour
{
    public Vector3 BeginPos;
    private Vector3 _randomPos;
    [SerializeField] private Sprite _hitSprite;
    private bool _moving = true;
    private bool _isHit;
    public bool IsHit { set=>_isHit = value;get => _isHit; }
    private PlayerMovement _player;
    private bool _hasPosition;
    private bool _hasSquashed = true;
    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;
    private Vector3 _position;
    private float _radius = 10;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>(4);

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    private float moveTimer;

    [SerializeField] private float _speed = 0.01f;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        Generator generator = FindObjectOfType<Generator>();
        BeginPos = generator._roomPositionList[generator.CurrentRoom];
        _randomPos = BeginPos + new Vector3((Random.insideUnitCircle * _radius).x, (Random.insideUnitCircle * _radius).y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= 3)
        {
            _randomPos = BeginPos + new Vector3((Random.insideUnitCircle * _radius).x, (Random.insideUnitCircle * _radius).y, 0);
            _moving = true;

            moveTimer = 0;

        }
        float distance = Vector3.Distance(transform.position, _randomPos);

        if (_moving)
        {
            _rb.MovePosition(Vector2.Lerp(transform.position, _randomPos, Time.deltaTime * _speed));

            if (_hasSquashed)
            {
                StartCoroutine(Squash());
            }
        }
        //if (distance < 1)
        //{
        //    //_randomPos = _beginPos + new Vector3((Random.insideUnitCircle * _radius).x, (Random.insideUnitCircle * _radius).y, 0);
        //    _moving = true;
        //}
    }

    public void Hit()
    {
        GetComponent<AudioSource>().clip = _clips[Random.Range(0,4)];
        GetComponent<AudioSource>().Play();
        _spriteRenderer.sprite = _hitSprite;
        if(!_isHit)
        {
        FindObjectOfType<Flock>().CreateBoy(transform.position, _hitSprite);
            _isHit=true;
        }
        GetComponentInChildren<SpriteRenderer>().enabled=false;
        Destroy(gameObject,GetComponent<AudioSource>().clip.length);


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
