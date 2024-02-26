using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    //public Transform enemy;
    public float maxRadius;
    public float maxAngle;

    private bool isInFov = false;
    public void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius; // Getting rotation (quaternion) from maxAngle to rotate horizontally
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        


        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public static bool inFOV (Transform checkingObject, Transform target, float maxAngle, float maxRadius) // Allows us to check if object is in FOV
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++) 
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target) // Checks if any of the overlaps are the target, checks if in radius of enemy
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0; // Doing this because height differences can change the angle. Keeps direction of y always 0. Height not a factor of the angle

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle) // Checks if in FOV of the enemy
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) // makes sure the ray cast is in the max radius
                        {
                            if (hit.transform == target) // Lets player see enemy
                                
                                return true;
                        }
                    }
                }
            }
        }
        
        return false;
    }
    

    private void Update()
    {
        if (Targetting.selectedTarget == null)
        {
            isInFov = false;
        }
        else
        {
            isInFov = inFOV(transform, Targetting.selectedTarget, maxAngle, maxRadius);
        }
        


        if (!isInFov)
        {
            ClicktoMove.enemyCanBeShot = false;
            Debug.Log("You cannot see the enemy");
        }
        else
        {
            ClicktoMove.enemyCanBeShot = true;
            Debug.Log("THERE HE IS BLAST HIM");
        }
    }

}
