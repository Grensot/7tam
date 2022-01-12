using UnityEngine;
public class Sort
{
    private SOData _soData;
    private CalculateRandom _calculateRandom;
    private SetPosID _setPosID;
    private GameObject[] _rock;
    private Transform[] _pos;
    public Sort(SOData soData, CalculateRandom calculateRandom, SetPosID setPosID, GameObject[] rock, Transform[] pos)
    {
        _soData = soData;
        _calculateRandom = calculateRandom;
        _setPosID = setPosID;
        _rock = rock;
        _pos = pos;
    }
    public void SortPos()
    {
        for (int i = 0; i < _rock.Length; i++)
        {
            _calculateRandom.Update();
            _setPosID.Update(i);
            //Для более свободного перемещения(Но могут спавниться ромбы)
            /*for (int b = 0; b < i; b++)
            {
                while (_soData._PosID[i] == _soData._PosID[b] || _soData._PosID[b] - 1 == _soData._PosID[i] || _soData._PosID[b] + 1 == _soData._PosID[i] || _soData._PosID[b] - 7 == _soData._PosID[i] || _soData._PosID[b] + 7 == _soData._PosID[i])
                {
                    _calculateRandom.Update();
                    _setPosID.Update(i);
                    b = 0;
                }
            }*/
        }
    }
}
