using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    List<Vector3> pos = new List<Vector3>()  {
          new Vector3(7, 3.5f, 0),
          new Vector3(7, 3.5f, 7),
          new Vector3(0, 3.5f, 7),
          new Vector3(0, 3.5f, 0)
    };

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
        var dir = (pos[current_index % pos.Count] - Camera.main.transform.position).normalized;
        while (Vector3.Distance(Camera.main.transform.position, pos[current_index % pos.Count]) > 0.5f  )
        {
            Camera.main.transform.LookAt(new Vector3(3.5f, 0.5f, 3.5f));

            Camera.main.transform.Translate(dir * moveSpeed * Time.deltaTime);


            yield return new WaitForEndOfFrame();
        }
        Camera.main.transform.LookAt(new Vector3(3.5f, 0.5f, 3.5f));

        Camera.main.transform.position = pos[current_index % pos.Count];

        current_index++;
        yield return null;
    }

}
