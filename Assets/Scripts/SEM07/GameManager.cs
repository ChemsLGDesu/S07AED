using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PriorityQueue<EntityBase> queue;
    public Transform startPoint;
    public TMP_Text TextOrden;
    
    private PriorityType priorityType;
    public float spacing = 2f;

    [Button]
    public void CrearCola()
    {
        var entities = FindObjectsByType<EntityBase>(FindObjectsSortMode.None);

        queue = new PriorityQueue<EntityBase>(Compare);

        foreach (var e in entities)
            queue.Enqueue(e);

        Debug.Log("Cola creada");
        UpdateUI();
        UpdatePositions();
    }

    bool Compare(EntityBase a, EntityBase b)
    {
        if (priorityType == PriorityType.MayorVelocidad)
            return a.Speed > b.Speed;
        else
            return a.ID < b.ID;
    }

    [Button]
    public void CambiarAPrioridadVelocidad()
    {
        priorityType = PriorityType.MayorVelocidad;
        ReordenarCola();
        UpdatePositions();
    }

    [Button]
    public void CambiarAPrioridadID()
    {
        priorityType = PriorityType.MenorID;
        ReordenarCola();
        UpdatePositions();

    }

    void ReordenarCola()
    {
        if (queue == null) return;

        queue.Comparation(Compare);
        UpdateUI();
        UpdatePositions();
    }


    [Button] // Mostrar orden
    public void MostrarOrden()
    {
        if (queue == null) return;

        var list = queue.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log((i + 1) + ". " + list[i]);
        }
    }

    [Button] // Siguiente turno
    public void SiguienteTurno()
    {
        if (queue == null || queue.Count == 0) return;

        var turno = queue.Dequeue();
        Debug.Log("Turno: " + turno);
        UpdatePositions();

    }
    void UpdateUI() // Mostrar orden en UI
    {
        if (queue == null) return;

        var list = queue.ToList();

        string texto = "";
        texto += "Criterio: " + priorityType + "\n\n";

        for (int i = 0; i < list.Count; i++)
        {
            var e = list[i];
            texto += (i + 1) + ". " + e.gameObject.name + "\n" + e.entityName
               + " | ID: " + e.ID
               + " | Speed: " + e.Speed
               + "\n";
        }

        TextOrden.text = texto;
    }
    void UpdatePositions()
    {
        if (queue == null) return;

        var list = queue.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Vector3 pos = startPoint.position + new Vector3(0, -i * spacing, 0);
            list[i].transform.position = pos;
        }
    }

    [Button]
    public void MostrarOrdenUI()
    {
        UpdateUI();
    }
}
