using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtons : MonoBehaviour
{
   public void DestroyAll(GameObject button1,GameObject button2,GameObject button3)
   {
      
      Destroy(button2);
      Destroy(button3);
      Destroy(button1);

   }
}
