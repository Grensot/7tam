using UnityEngine;
public class CalculateRandom
{
    private SOData _soData;
    private Transform[] _pos;
    public CalculateRandom(SOData soData, Transform[] pos)
    {
        _soData = soData;
        _pos = pos;
    }
    public void Update()
    {
        _soData._randomNumber = Random.Range(0, _pos.Length);
    }
}
