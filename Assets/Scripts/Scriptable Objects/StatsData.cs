using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "Game/Stats")]
public class StatsData : ScriptableObject
{
    public float speed;
    public float acceleration;
    public float brake;
    public float weight;
    public float handling;
}
