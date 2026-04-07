using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float posX;
    public float posY;
    public float posZ;
    public bool finishedRace;
    public List<bool> ringsPassed;
}
