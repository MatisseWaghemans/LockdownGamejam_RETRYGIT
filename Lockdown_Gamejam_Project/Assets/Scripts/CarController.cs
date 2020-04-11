using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private bool _moveLeft;
    private Vector3 _beginPos;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isStalinCar = false;
    [SerializeField] private GameObject _playerPrefab;
    bool _stalinCreated;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _beginPos = transform.position;
     if(GetComponentInChildren<SpriteRenderer>().flipX)
     _moveLeft = true;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Camera.main.WorldToViewportPoint(transform.position).x>1||Camera.main.WorldToViewportPoint(transform.position).x<0)
        {
        transform.position = _beginPos;
        if(_isStalinCar){
            Destroy(gameObject);
        }
        }
        if(!_moveLeft){
            if(_isStalinCar)
            {
            if(Camera.main.WorldToViewportPoint(transform.position).x<0.5f)
            {
             transform.position+=Vector3.right*_speed;
            }
            else 
            {
                _timer +=Time.deltaTime;
                if(!_stalinCreated)
                {
                    Instantiate(_playerPrefab,transform.position,transform.rotation);
                    Camera.main.GetComponent<CameraScriptWard>().enabled = true;
                    _stalinCreated =true;

                }
                if(_timer>1){
                    transform.position+=Vector3.right*_speed;
                }
            }
            }
            else transform.position+=Vector3.right*_speed;
        }
        else
        {
            transform.position+=Vector3.left*_speed;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
        transform.position = _beginPos;
        if(_isStalinCar){
            Destroy(gameObject);
        }
        }
    }
}
