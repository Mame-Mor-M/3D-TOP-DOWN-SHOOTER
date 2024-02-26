using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Targetting : MonoBehaviour
{
    public List<Transform> targets; // Empty list of targets, clicked entities will be placed in this list
    public static Transform selectedTarget; // Position of the clicked target

    void Start()
    {
        targets = new List<Transform>(); // Empty list created when program first runs
        

        AddAllEnemies();

        selectedTarget = null; // Selects the first target from the enemy target list
    }

    public void AddAllEnemies() // Method that places every enemy into the target list
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy"); // Finds every object in the scene with an enemy tag 

        foreach (GameObject enemy in go)
            AddTarget(enemy.transform);
    }

    public void AddTarget(Transform enemy) // Method adds an enemy to the list of targets
    {
        targets.Add(enemy);
    }

    public void TargetEnemy() // Method to target enemy with player cursor
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // A raycast line is created

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // Declares whereever the mouse cursor is hovering over as the target
        {
            selectedTarget = hit.transform; 
        }
            
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1)) // Targets the enemy if the player clicks their right mouse button
        {
            TargetEnemy();
        }
            
    }
}
