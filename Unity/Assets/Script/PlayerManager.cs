using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
   public int PlayerID;
   public int pieces;
   public GameObject piecePfb;
   private List<GameObject> allPieces = new List<GameObject>();
   
   private void Start()
   {
       
   }

   private void Update() 
   {
    
   }

   public void AddPiece()
   {
        GameObject curPiece = Instantiate(piecePfb,Vector3.zero,Quaternion.identity);
        allPieces.Add(curPiece);
        if(allPieces.Count>0)
        {
            
        }
        pieces++;
   }


   
}
