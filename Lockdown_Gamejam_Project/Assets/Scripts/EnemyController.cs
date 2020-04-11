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
    private bool _isHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        if(distance<10)
        {
            _timer += Time.deltaTime;
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
        transform.position = Vector3.Lerp(transform.position,_player.transform.position,Time.deltaTime*0.3f);
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
        _isHit = true;
    }
}
