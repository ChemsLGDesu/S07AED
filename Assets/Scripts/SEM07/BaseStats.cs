using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Scriptable Objects/EntityStats")]
public class BaseStats : ScriptableObject
{
    public string Name;
    public float speed;//->mayor velocidad
    public int id;//->menor id
}
