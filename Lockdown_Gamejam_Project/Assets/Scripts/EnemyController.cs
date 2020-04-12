using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _force;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private SpriteRenderer character;
    private GameObject _player;
    private float _timer;
    private bool _hasShot = true;
    public bool _isHit;

    private Rigidbody2D _rb;

    private bool _hasSquashed = true;
    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>(5);
    RoomTriggerScript _room;

    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _room =FindObjectOfType<RoomTriggerScript>();
    }


    void Update()
    {
        if(FindObjectOfType<PlayerMovement>()==null)
        {
            return;
        }
        _player = FindObjectOfType<PlayerMovement>().gameObject;
        
        if(!_isHit)
        {
        float distance = Vector3.Distance(transform.position,_player.transform.position);
        _rb.isKinematic =true;
        if(distance<10)
        {
            _timer += Time.deltaTime;
            if (_hasSquashed)
            {
                StartCoroutine(Squash());
            }
            FollowPlayer();
            MoveGun();
            if(_timer>3)
            {
                GetComponent<AudioSource>().Play();
                ShootPlayer();
                _timer=0;
            }
        }
        }
        else
        {
            Destroy(_gun);
        }
        
    }
    IEnumerator Squash()
    {
        float time = 0;
        _hasSquashed = false;


        float sin = 0;

        while (sin >= 0)
        {
            sin = _Squash * Mathf.Sin(time * Frequency);
            transform.localScale = Vector3.one + new Vector3(-sin, sin, -sin);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _hasSquashed = true;
        yield return null;
    }

    void ShootPlayer()
    {
        if (_hasShot)
        {
            _hasShot = false;
            StartCoroutine(ShootCouroutine(0.4f));
        }
    }
    void FollowPlayer()
    {
        _rb.isKinematic=false;
        _rb.MovePosition(Vector2.Lerp(transform.position,_player.transform.position,Time.deltaTime*0.3f));
    }

    private void MoveGun()
    {
        Vector3 player = Camera.main.WorldToViewportPoint(_player.transform.position);
        Vector3 enemyPos = Camera.main.WorldToViewportPoint(transform.position);
        if(enemyPos.x>player.x)
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = true;
            GetComponentInChildren<SpriteRenderer>().flipX=true;
            _gun.transform.localPosition = new Vector3(-0.2f,-0.25f,-0.2f);
        }
        else
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = false;
            GetComponentInChildren<SpriteRenderer>().flipX=false;
            _gun.transform.localPosition = new Vector3(0.05f,-0.25f,-0.2f);
        }

        Vector3 lookAt = _player.transform.position;

        float AngleRad = Mathf.Atan2(lookAt.y - _gun.transform.position.y, lookAt.x - _gun.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        _gun.transform.rotation = Quaternion.Euler(-AngleDeg, 90, 0);
    }

        IEnumerator ShootCouroutine(float Seconds)
    {

        Shoot();
        yield return new WaitForSeconds(0.07f);

        Shoot();
        yield return new WaitForSeconds(0.07f);

        Shoot();

        yield return new WaitForSeconds(Seconds);
        _hasShot = true;
        yield return null;
    }

    private void Shoot()
    {
        Vector3 player = Camera.main.WorldToViewportPoint(_player.transform.position);
        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>player.x)
        {
            GameObject _bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.Euler(0,0,_gun.transform.rotation.eulerAngles.x));
            _bullet.GetComponent<Rigidbody2D>().AddForce(_gun.transform.forward * _force, ForceMode2D.Impulse);
        }
        else
        {
            GameObject _bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.Euler(0,0,-_gun.transform.rotation.eulerAngles.x));
            _bullet.GetComponent<Rigidbody2D>().AddForce(_gun.transform.forward * _force, ForceMode2D.Impulse);
        }
        
    }
    public void Hit()
    {
        if(!_isHit)
        {
            character.GetComponent<Collider2D>().enabled = false;
            GetComponent<AudioSource>().clip = _clips[Random.Range(0,5)];
            GetComponent<AudioSource>().Play();
            _rb.isKinematic=true;

            Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
        
        }
        _isHit = true;
    }
}
