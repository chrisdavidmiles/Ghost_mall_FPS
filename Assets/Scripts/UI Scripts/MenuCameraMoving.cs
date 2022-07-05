using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMoving : MonoBehaviour
{
    public Camera camera;
    public Transform[] newCamPosList;
    public float defaultChangeTime = 1f;
    [SerializeField] private bool cameraMoving = false;
    private float changeTime;
    

    public void MoveCameraToNewPos()
    {
        if(newCamPosList.Length != 0)
        StartCoroutine(MovingTheCam());
    }

    private IEnumerator MovingTheCam()
    {
        for (int i =0; i < newCamPosList.Length; i++)
        {
            Transform oldCamTransform = camera.transform;
            Vector3 oldCamPos = oldCamTransform.position;

            float oldX = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).x;
            float oldY = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).y;
            float oldZ = UnityEditor.TransformUtils.GetInspectorRotation(oldCamTransform).z;

            float newX = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).x;
            float newY = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).y;
            float newZ = UnityEditor.TransformUtils.GetInspectorRotation(newCamPosList[i].transform).z;

            
            Vector3 oldCamRot = new Vector3(oldX, oldY, oldZ);
            Vector3 newCamRot = new Vector3(newX, newY, newZ);

            cameraMoving = true;
            changeTime = 0;


            while (cameraMoving)
            {
                yield return null;
                changeTime += Time.deltaTime;
                camera.transform.position = Vector3.Lerp(oldCamPos, newCamPosList[i].transform.position, (changeTime / (defaultChangeTime / newCamPosList.Length)));
                //camera.transform.rotation = Quaternion.Euler(Vector3.Lerp(oldCamRot, newCamRot, (changeTime / (defaultChangeTime / newCamPosList.Length))));
                camera.transform.eulerAngles = Vector3.Lerp(oldCamRot, newCamRot, (changeTime / (defaultChangeTime / newCamPosList.Length)));


                if (changeTime >= (defaultChangeTime / newCamPosList.Length))
                {
                    cameraMoving = false;
                    changeTime = 0;
                    Debug.Log("We're at: " + i + " out of: " + (newCamPosList.Length - 1) + " and the camera rotation is: " + newCamRot);
                }
            }
        }

    }
}
