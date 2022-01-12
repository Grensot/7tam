using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    private GameObject _char;
    private GameObject[] _enemy;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRend;
    private Sprite[] _sprites;
    private Vector2 direction;
    private Vector2[] _enemyWays;
    private float _speed = 1f;
    private int _way;
    private RaycastHit2D RayCheck, RayCheckPlayer;
    private SOData _soData;
    private GameObject[] _destination;
    private GameObject _lose;
    public EnemyController(GameObject[] enemy, float charspeed, Sprite[] sprites, Vector2[] enemyWays, GameObject Cchar, SOData soData, GameObject[] destination, GameObject lose)
    {
        _char = Cchar;
        _enemy = enemy;
        _speed = charspeed;
        _sprites = sprites;
        _enemyWays = enemyWays;
        _soData = soData;
        _destination = destination;
        _lose = lose;
    }
    public void Start()
    {
        _soData._enemyStateDog = 1;
        _rb = _enemy[0].GetComponent<Rigidbody2D>();
        _spriteRend = _enemy[0].GetComponent<SpriteRenderer>();
    }
    public void FindWay()
    {
        _way = Random.Range(0, _enemyWays.Length);
    }
    public void Update()
    {
        DrawRay();
        CheckWay();
        ChangeMovement();
        if (_soData._enemyStateDog == 2)
        {
            ChangeMovement2();
        }
        if (_soData._enemyStateDog != 3)
        {
            _speed = 10f;
        }
    }
    public void FixedUpdate()
    {
        if(_soData._enemyStateDog == 3)
        {
            _speed = 0;
        }
        _rb.MovePosition(_rb.position + direction * _speed * Time.deltaTime);
    }
    public void ChangeMovement()
    {
        if (_soData._enemyStateDog != 3)
        {
            if (_way == 0)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 1)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 2)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 3)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
        }
    }
    public void ChangeMovement2()
    {
        if (_soData._enemyStateDog != 3)
        {
            if (_way == 0)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way + 4;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 1)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way + 4;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 2)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way + 4;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
            else if (_way == 3)
            {
                direction = _enemyWays[_way];
                _soData._enemySpriteDog = _way + 4;
                _spriteRend.sprite = _sprites[_soData._enemySpriteDog];
            }
        }
    }
    public void DrawRay()
    {
        RayCheck = Physics2D.Raycast(new Vector3(_enemy[0].transform.position.x + direction.x, _enemy[0].transform.position.y + direction.y, 0), direction * 1.5f, 0.2f);
        RayCheckPlayer = Physics2D.Raycast(new Vector3(_enemy[0].transform.position.x + direction.x, _enemy[0].transform.position.y + direction.y, 0), direction * 1.5f, 100f);
    }
    public void Check()
    {
        if (_soData._enemyStateDog != 3 && RayCheck.collider != null && RayCheck.transform.gameObject == _char)
        {
            Debug.Log("??");
            _lose.SetActive(true);
            _char.SetActive(false);
        }
        if (_soData._enemyStateDog == 1 && RayCheckPlayer.transform.gameObject == _char)
        {
            _soData._enemyStateDog = 2;
            _soData._InView = true;
        }
        else if (_soData._enemyStateDog == 1 && RayCheckPlayer.transform.gameObject != _char)
        {
            _soData._InView = false;
        }
        if (_soData._enemyStateDog == 2 && ((_enemy[0].transform.position.x <= _destination[0].transform.position.x && _enemy[0].transform.position.x >= _destination[0].transform.position.x - 1) || 
           (_enemy[0].transform.position.x >= _destination[0].transform.position.x && _enemy[0].transform.position.x <= _destination[0].transform.position.x + 1)) &&
           ((_enemy[0].transform.position.y <= _destination[0].transform.position.y && _enemy[0].transform.position.y >= _destination[0].transform.position.y - 1) ||
           (_enemy[0].transform.position.y >= _destination[0].transform.position.y && _enemy[0].transform.position.y <= _destination[0].transform.position.y + 1)))
        {
            _soData._enemyStateDog = 1;
        }
        if (_soData._enemyStateFarmer == 3 && ((_enemy[0].transform.position.x <= _enemy[1].transform.position.x && _enemy[0].transform.position.x >= _enemy[1].transform.position.x - 1) ||
           (_enemy[0].transform.position.x >= _enemy[1].transform.position.x && _enemy[0].transform.position.x <= _enemy[1].transform.position.x + 1)) &&
           ((_enemy[0].transform.position.y <= _enemy[1].transform.position.y && _enemy[0].transform.position.y >= _enemy[1].transform.position.y - 1) ||
           (_enemy[0].transform.position.y >= _enemy[1].transform.position.y && _enemy[0].transform.position.y <= _enemy[1].transform.position.y + 1)))
        {
            _soData._enemyStateFarmer = 1;
        }
    }
    public void CheckWay()
    {
        if (RayCheck.collider != null && RayCheck.transform.gameObject != _char)
        {
            FindWay();
            _soData._enemyStateDog = 1;
        }
    }
}
