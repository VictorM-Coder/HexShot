using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
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
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private float _lastFireTime;

    // Update is called once per frame
    void Update()
    {  
        updateBullets();   
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _bulletSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue)
    {
        if(inputValue.isPressed && _numOfBullets > 0)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if(timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();
                StartCoroutine(cooldownCursor());
                _numOfBullets--;
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
}
