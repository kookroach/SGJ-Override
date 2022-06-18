using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform[] pos = new Transform[4];

        int current_index = 0;
    bool running = false;
    public float moveSpeed = 2;

    public void RotateCam()
    {
        if(running) return;
        StartCoroutine(Courutine());
    }

    IEnumerator Courutine()
    {
        
        while (Vector3.Distance(Camera.main.transform.position, pos[current_index % pos.Length].position) != 0f  )
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,
                pos[current_index % pos.Length].position, moveSpeed * Time.deltaTime * 7.5f);
            
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation,
                pos[current_index % pos.Length].rotation, moveSpeed * Time.deltaTime * 100f);


            yield return new WaitForEndOfFrame();
        }
        

        current_index++;
        yield return null;
    }

}
