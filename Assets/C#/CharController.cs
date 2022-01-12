using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController
{
    private GameObject _char;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRend;
    private Sprite[] _sprites;
    private Vector2 direction;
    private float _speed = 1f;
    public CharController(GameObject character, float charspeed, Sprite[] sprites)
    {
        _char = character;
        _speed = charspeed;
        _sprites = sprites;
    }
    public void Start()
    {
        _rb = _char.GetComponent<Rigidbody2D>();
        _spriteRend = _char.GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal") * _speed;
        direction.y = Input.GetAxisRaw("Vertical") * _speed;
    }
    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * _speed * Time.deltaTime);
    }
    public void ChangeSprite()
    {
        if (direction.x < 0)
        {
            _spriteRend.sprite = _sprites[0];
        }
        else if(direction.x > 0)
        {
            _spriteRend.sprite = _sprites[1];
        }
        if (direction.y < 0)
        {
            _spriteRend.sprite = _sprites[2];
        }
        else if (direction.y > 0)
        {
            _spriteRend.sprite = _sprites[3];
        }
    }
}
