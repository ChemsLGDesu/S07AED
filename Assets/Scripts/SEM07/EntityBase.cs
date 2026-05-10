using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public string entityName;
    public BaseStats stats;

    public int ID => stats.id;
    public float Speed => stats.speed;

    public override string ToString()
    {
        return entityName + " ID " + ID + " Speed " + Speed;
    }
}
