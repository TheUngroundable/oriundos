using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
   public int PlayerID;
   public int pieces;
   public GameObject lastPiece;
   private List<GameObject> allPieces = new List<GameObject>();
   public float direction; // da -1 a 1

   private void Start()
   {
       
   }

   private void Update()
   {
        if (Input.GetKey("right"))
        {
            direction = 1;
        }
        else  if(Input.GetKey("left"))
        {
            direction = -1 ;
        }
        else
        {
             direction = 0 ;
        }
   }

    

   public void AddPiece()
   {
        GameObject curPiece = Instantiate(lastPiece,Vector3.zero,Quaternion.identity,transform);
        allPieces.Add(curPiece);
        curPiece.transform.localPosition = lastPiece.transform.localPosition + new Vector3(direction,-1,0);
        pieces++;
        lastPiece = curPiece;
   }


   
}
