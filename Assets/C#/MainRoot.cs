using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MainRoot : MonoBehaviour
{
    [SerializeField]
    private Transform[] _pos;
    [SerializeField]
    private GameObject[] _rock;
    [SerializeField]
    private SOData _soData;
    [SerializeField]
    private GameObject _char;
    [SerializeField]
    private float _charspeed;
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private GameObject[] _bomb;
    [SerializeField]
    private GameObject[] _timer;
    [SerializeField]
    private float[] _time;
    [SerializeField]
    private GameObject[] _enemy;
    [SerializeField]
    private GameObject _lose;
    [SerializeField]
    private Sprite[] _enemySprite;
    [SerializeField]
    private Vector2[] _enemyWays;
    [SerializeField]
    private GameObject _desObj;
    [SerializeField]
    private GameObject[] _destination;
    private int i, i2;
    private int _numberOfSpriteT;
    private SpriteRenderer _SRtimer;
    private SpriteRenderer _SRbomb;
    private CalculateRandom _calculateRandom;
    private Sort _sort;
    private SetPosID _setPosID;
    private CharController _charController;
    private Bomb _Bomb;
    private EnemyController _enemyController;
    private FarmerController _farmerController;
    private bool _ReBomb = false;
    private void Start()
    {
        _calculateRandom = new CalculateRandom(_soData, _pos);
        _setPosID = new SetPosID(_soData);
        _sort = new Sort(_soData, _calculateRandom, _setPosID, _rock, _pos);
        _charController = new CharController(_char, _charspeed, _sprites);
        _enemyController = new EnemyController(_enemy, _charspeed, _enemySprite, _enemyWays, _char, _soData, _destination, _lose);
        _farmerController = new FarmerController(_enemy, _charspeed, _enemySprite, _enemyWays, _char, _soData, _destination, _lose);
        _sort.SortPos();
        _charController.Start();
        _enemyController.Start();
        _farmerController.Start();
        StartCoroutine(ChangeWayDog(2));
        StartCoroutine(ChangeWayFarmer(2));
        StartCoroutine(Check(0.5f));
        InsR();
        _destination[0] = Instantiate(_desObj, _char.transform.position, _char.transform.rotation);
        _destination[1] = Instantiate(_desObj, _char.transform.position, _char.transform.rotation);
    }
    private void Update()
    {
        _charController.Update();
        _charController.ChangeSprite();
        _farmerController.Update();
        _enemyController.Update();
        if (Input.GetKeyDown(KeyCode.F) && _ReBomb == false)
        {
            PlantBomb();
            _ReBomb = true;
            StartCoroutine(ReBomb(10));
        }
        if(_soData._enemyStateDog == 2 && _soData._InView == true)
        {
            _destination[0].transform.position = _char.transform.position;
        }
        if (_soData._enemyStateFarmer == 2 && _soData._InView == true)
        {
            _destination[0].transform.position = _char.transform.position;
        }
        if (_soData._enemyStateDog == 1 && i == 0)
        {
            StartCoroutine(ChangeWayDog(2));
            i++;
        }
        if (_soData._enemyStateFarmer == 1 && i2 == 0)
        {
            StartCoroutine(ChangeWayFarmer(2));
            i2++;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void FixedUpdate()
    {
        _charController.FixedUpdate();
        _enemyController.FixedUpdate();
        _farmerController.FixedUpdate();
    }
    private void InsR()
    {
        for (int i = 0; i < _rock.Length; i++)
        {
            Debug.Log(_soData._PosID[i]);
            Instantiate(_rock[0],_pos[_soData._PosID[i]].position, _pos[_soData._PosID[i]].rotation);
        }
    }
    private void PlantBomb()
    {
        _bomb[1] = Instantiate(_bomb[0], _char.transform.position, _char.transform.rotation);
        _bomb[1].SetActive(true);
        _SRbomb = _bomb[1].GetComponent<SpriteRenderer>();
        InsTimer();
    }
    private void InsTimer()
    {
        _timer[1] = Instantiate(_timer[0], _bomb[1].transform.position + new Vector3(0, 1, 0), _bomb[1].transform.rotation);
        _timer[1].transform.localScale -= new Vector3(0.5f, 0.5f, 0);
        _SRtimer = _timer[1].GetComponent<SpriteRenderer>();
        StartCoroutine(Timer(1));
    }
    private IEnumerator ChangeWayDog(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_soData._enemyStateDog == 1)
        {
            StartCoroutine(ChangeWayDog(2));
            _enemyController.FindWay();
        }
        else if(_soData._enemyStateDog == 2)
        {
            StartCoroutine(ChangeWayDog(5));
            _enemyController.FindWay();
        }
        i = 0;
    }
    private IEnumerator ChangeWayFarmer(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_soData._enemyStateFarmer == 1)
        {
            StartCoroutine(ChangeWayFarmer(2));
            _farmerController.FindWay();
        }
        else if (_soData._enemyStateFarmer == 2)
        {
            StartCoroutine(ChangeWayFarmer(5));
            _farmerController.FindWay();
        }
        i2 = 0;
    }
    private IEnumerator Timer(float sec)
    {
        _SRtimer.sprite = _sprites[_numberOfSpriteT + 4];
        yield return new WaitForSeconds(sec);
        _numberOfSpriteT++;
        if (_numberOfSpriteT != 4)
        {
            StartCoroutine(Timer(1));
        }
        else
        {
            _Bomb = new Bomb(_bomb[1], _enemySprite, _char, _enemy, _lose, _soData);
            _Bomb.DrawRays();
            _SRbomb.sprite = _sprites[8];
            _bomb[1].transform.localScale -= new Vector3(0.7f, 0.7f, 0);
            Destroy(_timer[1]);
            StartCoroutine(Detonate(1));
            _numberOfSpriteT = 0;
        }
    }
    private IEnumerator Detonate(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(_bomb[1]);
    }
    private IEnumerator Check(float sec)
    {
        yield return new WaitForSeconds(sec);
        _enemyController.Check();
        _farmerController.Check();
        StartCoroutine(Check(0.2f));
    }
    private IEnumerator ReBomb(float sec)
    {
        yield return new WaitForSeconds(sec);
        _ReBomb = false;
    }
}