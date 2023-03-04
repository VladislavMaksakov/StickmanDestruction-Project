using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public float interpVelocity;

    Camera mainCamera;

    public float followUpSpeed;

    [Range(0,2f)]
    public float xOffset;
    [Range(-2f, 2f)]
    public float yOffset;

   
    public Vector3 offset;
    Vector3 targetPos;

    // Use this for initialization
    void Start () {
        
        targetPos = transform.position;
        mainCamera = GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {

            CameraFollowUp();
        }


    void ResetCameraSize()
    {  
        if (player2 && player3 && player4)
        {
            float[] distances = new float[5];
            distances[0] = Vector3.Distance(player.transform.position, player2.transform.position);
            distances[1] = Vector3.Distance(player.transform.position, player3.transform.position);
            distances[2] = Vector3.Distance(player.transform.position, player4.transform.position);
            distances[3] = Vector3.Distance(player2.transform.position, player3.transform.position);
            distances[4] = Vector3.Distance(player2.transform.position, player4.transform.position);

            float maxDistance = distances[0];

            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] > maxDistance)
                {
                    maxDistance = distances[i];
                }
            }
          
                mainCamera.orthographicSize = 8 + maxDistance / 2.25f;   
        }
        else if (player2 && player3)
        {
            float[] distances = new float[2];
            distances[0] = Vector3.Distance(player.transform.position, player2.transform.position);
            distances[1] = Vector3.Distance(player.transform.position, player3.transform.position);
         

            float maxDistance = distances[0];

            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] > maxDistance)
                {
                    maxDistance = distances[i];
                }
            }

            mainCamera.orthographicSize = 8 + maxDistance / 2.25f;
        }
        else if (player2 && player4)
        {
            float[] distances = new float[2];
            distances[0] = Vector3.Distance(player.transform.position, player2.transform.position);
            distances[1] = Vector3.Distance(player.transform.position, player4.transform.position);


            float maxDistance = distances[0];

            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] > maxDistance)
                {
                    maxDistance = distances[i];
                }
            }

            mainCamera.orthographicSize = 8 + maxDistance / 2.25f;
        }

        else
        {
            if (player2)
            {
                mainCamera.orthographicSize = 8 + Vector3.Distance(player.transform.position, player2.transform.position) / 2.25f;
            }
            else
            if (player3)
            {
                mainCamera.orthographicSize = 8 + Vector3.Distance(player.transform.position, player3.transform.position) / 2.25f;
            }
            else
            if (player4)
            {
                mainCamera.orthographicSize = 8 + Vector3.Distance(player.transform.position, player4.transform.position) / 2.25f;
            }
        }

    }

    void CameraFollowUp()
    {

        if (player2 == null && player3==null && player4==null)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position - posNoZ);
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * followUpSpeed;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 && player3 == null && player4 == null)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position+player2.transform.position)/2 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 && player3 && player4 == null)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player2.transform.position + player3.transform.position) / 3 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 && player3 && player4)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player2.transform.position + player3.transform.position + player4.transform.position) / 4 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }


        else if (player2==null && player3 && player4)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player3.transform.position + player4.transform.position) / 3 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 == null && player3==null && player4)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player4.transform.position) / 2 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 == null && player3 && player4==null)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player3.transform.position) / 2 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

        else if (player2 && player3==null && player4)
        {
            ResetCameraSize();
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position + player2.transform.position + player4.transform.position) / 3 - posNoZ;
            targetDirection.x += xOffset;
            targetDirection.y += yOffset;
            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }

    }
  
 
}
