using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _bulletType;
    public int _bullets = 6;
    public int _numOfBullets;
    public Image[] _imageBullets;
    private float _bulletDisparedTime;
    private bool isRegen;

    [SerializeField]
    private Texture2D _dispareCursor;

    [SerializeField]
    private Texture2D _normalCursor;
    private Vector2 _cursorHotspot;

    [SerializeField]
    private GameObject[] _bulletPrefab;

    private int _bulletSelectedPosition = 0;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private float _lastFireTime;

    // Update is called once per frame
    void Update()
    {  
        updateBullets();  
        changeBulletSelected(); 
    }

   private void FireBullet()
    {
        if (_bulletPrefab[_bulletSelectedPosition].CompareTag("FlashShot") || _bulletPrefab[_bulletSelectedPosition].CompareTag("Bullet")) {
            instatiateBullets(0);
        } else if (_bulletPrefab[_bulletSelectedPosition].CompareTag("Shotgun")) {
           instatiateBullets(15);
           instatiateBullets(0);
           instatiateBullets(-15);
        } else {
            StartCoroutine(bulletStorm());
        }
    }

    private void OnFire(InputValue inputValue)
    {
        if(inputValue.isPressed && _numOfBullets > 0)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            int cost =  getBulletSelected().getCost();

            if(timeSinceLastFire >= _timeBetweenShots && cost <= _numOfBullets)
            {
                FireBullet();
                StartCoroutine(cooldownCursor());
                _numOfBullets-= cost;
                _lastFireTime = Time.time;
            }
        }
    }

    private void updateBullets(){
        for (int cont = 0; cont < _imageBullets.Length; cont++) {  
            _imageBullets[cont].enabled = cont < _numOfBullets;
        }


        if (_numOfBullets < _bullets && !isRegen) {
            StartCoroutine(regenBullets());
            isRegen = true;
        }

    }

    private IEnumerator regenBullets(){
        yield return new WaitForSeconds(2f);
        if (_numOfBullets < _bullets) _numOfBullets++;
        isRegen = false;
    }

    private IEnumerator cooldownCursor() {
        _cursorHotspot = new Vector2(_dispareCursor.width/2, _dispareCursor.width/2);
        Cursor.SetCursor(_dispareCursor, _cursorHotspot, CursorMode.Auto);
        yield return new WaitForSeconds(_timeBetweenShots);
        _cursorHotspot = new Vector2(_normalCursor.width/2, _normalCursor.width/2);
        Cursor.SetCursor(_normalCursor, _cursorHotspot, CursorMode.Auto);
    }

    private void changeBulletSelected() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_bulletSelectedPosition < _bulletPrefab.Length-1) {
                ++_bulletSelectedPosition;
            }else {
                _bulletSelectedPosition = 0;
            }
        }
        _bulletType.text = getBulletSelected().getName();
    }

    private Bullet getBulletSelected() {
        return  _bulletPrefab[_bulletSelectedPosition].GetComponent<Bullet>();
    }

    private GameObject instatiateBullets(float angle) {
        GameObject bullet = Instantiate(_bulletPrefab[_bulletSelectedPosition], _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Vector2 direction = rotation * transform.up;

        rigidbody.velocity = getBulletSelected().getSpeed() * direction;
        return bullet;
    }

    private IEnumerator bulletStorm() {
        for (int cont = 1; cont <= 20; cont++){
            yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
            instatiateBullets(Random.Range(0, 5));
        }
    }
}
