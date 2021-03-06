﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D _rigidbody;
    private bool _hasSquashed = true;
    private bool _hasTurned = false;

    private bool _hasDied;
    private float _deathTimer;

    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;


    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _force;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private SpriteRenderer character;

    private bool _hasShot = true;

    public float Reloadtime = 1f;

    public List<GameObject> _followers = new List<GameObject>();
    [SerializeField] private AudioClip _shootingStart;
    [SerializeField] private AudioClip _shootingMid;
    [SerializeField] private AudioClip _shootingEnd;
    [SerializeField] private AudioClip _dieClip;

    Vector2 movement;
    public bool IsHit;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!IsHit)
        {
        Move();
        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().clip = _shootingStart;
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetMouseButton(0))
        {
            if(!GetComponent<AudioSource>().isPlaying)
            {
            GetComponent<AudioSource>().clip = _shootingMid;
            GetComponent<AudioSource>().Play();
            }
            ShootGun();
        }
        if(Input.GetMouseButtonUp(0))
        {
            GetComponent<AudioSource>().clip = _shootingEnd;
            GetComponent<AudioSource>().Play();
        }
        MoveGun();
        }
        if(_hasDied)
        {
            _deathTimer += Time.deltaTime;
            if(_deathTimer >= 3f)
            {
                LoadDeathScene();
            }
        }
        
    }

    private void Move()
    {
        Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>mouse.x)
        {
            character.flipX = true;
        }
        else
        {
            character.flipX = false;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetAxisRaw("Horizontal") < 0 && !_hasTurned)
        {

            _hasTurned = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && _hasTurned)
        {

            _hasTurned = false;
        }

        movement = movement.normalized;

        if (movement.magnitude > 0.1f & _hasSquashed)
        {
            StartCoroutine(Squash());
        }
    }

    private void ShootGun()
    {
        if (_hasShot)
        {
            _hasShot = false;
            StartCoroutine(ShootCouroutine(Reloadtime));
        }
    }
    private void MoveGun()
    {
        Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>mouse.x)
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = true;
            _gun.transform.localPosition = new Vector3(-0.4f,0.15f,-0.2f);
        }
        else
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = false;
            _gun.transform.localPosition = new Vector3(0.38f,0.07f,-0.2f);
        }
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - _gun.transform.position.y, lookAt.x - _gun.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        _gun.transform.rotation = Quaternion.Euler(-AngleDeg, 90, 0);
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * moveSpeed * Time.deltaTime);
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
        Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>mouse.x)
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

    IEnumerator Squash()
    {
        float time = 0;
        _hasSquashed = false;


        float sin = 0;

        while (sin >= 0)
        {
             sin = _Squash * Mathf.Sin(time * Frequency);
             character.transform.localScale =Vector3.Scale(Vector3.one + new Vector3(-sin, sin, -sin),Vector3.one*2);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _hasSquashed = true;
        yield return null;
    }
    public void Die()
    {
        IsHit = true;
        _rigidbody.isKinematic =true;
        GetComponent<AudioSource>().clip = _dieClip;
        GetComponent<AudioSource>().Play();
        _hasDied = true;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        _gun.SetActive(false);


        
    }

    private void LoadDeathScene()
    {
        SceneManager.LoadScene(2);
    }
}
