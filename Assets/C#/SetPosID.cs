using UnityEngine;
public class SetPosID
{
    private SOData _soData;
    public SetPosID(SOData soData)
    {
        _soData = soData;
    }
    public void Update(int id)
    {
        _soData._PosID[id] = _soData._randomNumber;
    }
}

