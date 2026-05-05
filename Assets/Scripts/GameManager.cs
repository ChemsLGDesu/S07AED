using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Title("Configuración de Entidades")]
    public List<EntityBase> allEntities = new List<EntityBase>();
    private PriorityQueue<EntityBase> turnQueue;

    [Title("Referencias de UI")]
    public TextMeshProUGUI uiTurnList;        
    public TextMeshProUGUI uiCurrentCriteria; 

    private bool sortBySpeed = true; 

    void Start()
    {
        allEntities.AddRange(FindObjectsByType<EntityBase>(FindObjectsSortMode.None));
        RebuildQueue();
    }

    [Button, GUIColor(0, 1, 0)]
    public void RebuildQueue()
    {
        // Hito 2 y 6
        if (sortBySpeed)
        {
            // Mayor velocidad = Mayor prioridad
            turnQueue = new PriorityQueue<EntityBase>((a, b) => a.speed > b.speed);
            uiCurrentCriteria.text = "Criterio Actual: Mayor Velocidad";
        }
        else
        {
            // Menor ID = Mayor prioridad
            turnQueue = new PriorityQueue<EntityBase>((a, b) => a.id < b.id);
            uiCurrentCriteria.text = "Criterio Actual: Menor ID";
        }

        // Insertar todas las entidades ordenadamente
        foreach (var entity in allEntities)
        {
            turnQueue.Enqueue(entity);
        }
        UpdateUI();
    }

    // Hito 6: Alternar entre tipos de prioridad desde UI
    [Button]
    public void TogglePriority()
    {
        sortBySpeed = !sortBySpeed;
        RebuildQueue();
        Debug.Log("Criterio cambiado. Cola reconstruida.");
    }

    // Hito 7: Ejecutar avance de turnos (Botón Dequeue)
    [Button]
    public void NextTurn()
    {
        // Verificar si hay entidades usando la propiedad Count
        if (turnQueue.Count > 0)
        {          
            EntityBase actingEntity = turnQueue.Dequeue();       
            Debug.Log($" {actingEntity.entityName} ha realizado su acción.");
            UpdateUI();
        }
        else
        {
            // Hito 4: Reconstruir la cola cuando se vacía
            Debug.Log("No quedan turnos. Reiniciando ciclo de combate...");
            RebuildQueue();
        }
    }
    // Hito 5: Mostrar el orden de ataque en pantalla
    void UpdateUI()
    {
        uiTurnList.text = "ORDEN DE ATAQUE:";

        if (turnQueue.Count > 0)
        {
            // Peek para mostrar quién es el primero sin eliminarlo
            EntityBase next = turnQueue.Peek();
            uiTurnList.text += $"PROXIMO: {next.entityName}";
            uiTurnList.text += $"Restantes en cola: {turnQueue.Count}";
        }
        else
        {
            uiTurnList.text += "COLA VACÍA";
        }
    }

    #region Métodos (Peek, Clear, Count)

    public void ExecutePeek()
    {
        if (turnQueue.Count > 0)
            Debug.Log("El siguiente es: " + turnQueue.Peek().entityName);
    }

    public void ExecuteClear()
    {
        turnQueue.Clear(); 
        UpdateUI();
        Debug.Log("Cola limpiada.");
    }

    public void ExecuteCount()
    {
        Debug.Log("Entidades en cola: " + turnQueue.Count); 
    }
    #endregion
}
