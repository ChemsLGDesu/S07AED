using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public BaseStats stats;
    public string entityName;
    public float id;
    public float speed;

    private void Awake()
    {
        if (stats != null)
        {
            entityName = stats.name;
            id = stats.id;
            speed = stats.speed;
        }
    }
}
