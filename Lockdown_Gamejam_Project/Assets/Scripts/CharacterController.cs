using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]private float _moveSpeed;
    [SerializeField] private GameObject _gun;
    [SerializeField]private GameObject _bulletPrefab;
    [SerializeField]private float _force;
    [SerializeField] private Transform _bulletSpawn;
    private GameObject _bullet;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MovePerson();
        if(Input.GetMouseButtonDown(0))
        ShootGun();
        MoveGun();

    }
    private void ShootGun()
    {
        GameObject _bullet =Instantiate(_bulletPrefab,_bulletSpawn.position,transform.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(_gun.transform.forward*_force,ForceMode2D.Impulse);

    }
    private void MovePerson()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up *_moveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down *_moveSpeed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //_gun.transform.localPosition = new Vector3(-0.72f,_gun.transform.position.y,_gun.transform.position.z);
            transform.position += Vector3.left *_moveSpeed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //_gun.transform.localPosition = new Vector3(0.72f,_gun.transform.position.y,_gun.transform.position.z);
            transform.position += Vector3.right *_moveSpeed;
        }
    }
        private void MoveGun()
        {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - _gun.transform.position.y, lookAt.x - _gun.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        _gun.transform.rotation = Quaternion.Euler(-AngleDeg, 90, 0);
        }
}
