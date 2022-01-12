using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb
{
    private GameObject _bomb;
    private Sprite[] _sprites;
    private GameObject _Char;
    private GameObject[] _enemy;
    private GameObject _lose;
    private SOData _soData;
    public Bomb(GameObject bomb, Sprite[] sprites, GameObject _char, GameObject[] enemy, GameObject lose, SOData soData)
    {
        _bomb = bomb;
        _sprites = sprites;
        _Char = _char;
        _enemy = enemy;
        _lose = lose;
        _soData = soData;
    }
    private RaycastHit2D _hitInfo0, _hitInfo1, _hitInfo2, _hitInfo3;
    public void DrawRays()
    {
        _hitInfo0 = Physics2D.Raycast(_bomb.transform.position, -Vector2.up, 3.0f);
        _hitInfo1 = Physics2D.Raycast(_bomb.transform.position, Vector2.up, 3.0f);
        _hitInfo2 = Physics2D.Raycast(_bomb.transform.position, -Vector2.left, 3.0f);
        _hitInfo3 = Physics2D.Raycast(_bomb.transform.position, Vector2.left, 3.0f);
        HitCheck();
    }
    public void HitCheck()
    {
        if (_hitInfo0.collider != null)
        {
            Comparison(_hitInfo0);
        }
        if (_hitInfo1.collider != null)
        {
            Comparison(_hitInfo1);
        }
        if (_hitInfo2.collider != null)
        {
            Comparison(_hitInfo2);
        }
        if (_hitInfo3.collider != null)
        {
            Comparison(_hitInfo3);
        }
    }
    public void Comparison(RaycastHit2D hitInfo)
    {
        if (hitInfo.transform.gameObject == _Char)
        {
            _lose.SetActive(true);
            _Char.SetActive(false);
        }
        if (hitInfo.transform.gameObject == _enemy[0])
        {
            var Sprite = _enemy[0].GetComponent<SpriteRenderer>();
            if(_soData._enemyStateDog == 1)
            Sprite.sprite = _sprites[_soData._enemySpriteDog + 8];
            if (_soData._enemyStateDog == 2)
                Sprite.sprite = _sprites[_soData._enemySpriteDog + 4];
            _soData._enemyStateDog = 3;
        }
        if (hitInfo.transform.gameObject == _enemy[1])
        {
            var Sprite = _enemy[1].GetComponent<SpriteRenderer>();
            if (_soData._enemyStateFarmer == 1)
                Sprite.sprite = _sprites[_soData._enemySpriteFarmer + 20];
            if (_soData._enemyStateFarmer == 2)
                Sprite.sprite = _sprites[_soData._enemySpriteDog + 16];
            _soData._enemyStateFarmer = 3;
        }
    }
}
