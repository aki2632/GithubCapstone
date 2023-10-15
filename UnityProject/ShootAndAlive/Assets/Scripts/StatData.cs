using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/StatData")]
public class StatData : ScriptableObject
{
    public enum StatType { AttackSpeed, AttackDamage, MoveSpeed}

    [Header("# Main Info")]
    public StatType statType;
    public int statId;
    public string statName;
    public string statDesc;

    [Header("# Level Data")]
    public float[] damages; // Attack Damage
    public float[] aSpeed; // Attack Speed
    public float[] mSpeed; // Move Speed

}
