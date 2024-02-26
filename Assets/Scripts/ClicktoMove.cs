using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ClicktoMove : MonoBehaviour
{
    //Animator Stuff
    private Animator anim;

    //NavMesh Stuff
    private NavMeshAgent navMeshAgent;
    private Quaternion finalRot;
    

    //Enemy Stuff
    public static bool enemyCanBeShot;
    public static GameObject enemySelected;
    public int enemiesKilled;

    private bool running;

    private bool isRotating;

    //Misc Stuff
    
   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //finalRot = Quaternion.LookRotation(path.corners[distBetween]);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit(); // Defines hit as the variable for the raycast
        

        if (Input.GetMouseButtonDown(0)) // Checks if the player left clicks
        {
            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Moves the raycast laser accordingly with the mouse
            
            if (Physics.Raycast(ray,out hit, Mathf.Infinity)) // Finds out where the player clicks
            {
                
                Vector3 relativePos = hit.point - transform.position; // Player position
                finalRot = Quaternion.LookRotation(relativePos, Vector3.forward); // Rotation position towards the direction of the clicked location

                if (hit.transform.gameObject.layer == 8) // Checks if enemy was clicked
                {
                    
                    transform.rotation = finalRot;
                    if (enemyCanBeShot == true) // Checks if enemy is in FOV
                    {
                        Destroy(hit.transform.gameObject); // Destroys enemy
                        enemiesKilled += 1;
                        Debug.Log("Kills: " + enemiesKilled);
                    }
                    
                }
                else
                {
                    navMeshAgent.SetDestination(hit.point); // Moves the player
                }
            }
        }


       

    }

}
