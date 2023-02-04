using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
   public int PlayerID;
   public int pieces;
   public List<RootManager> allRoots = new List<RootManager>();
   public float direction; // da -1 a 1
  

    public void SetDirection(float newDirection){
        //direction = newDirection;
    }


    public void AddRoot(RootManager rm , Vector3 pos)
    {
        RootManager newRoot = Instantiate(rm,Vector3.zero,Quaternion.identity,transform);
        
        
        for(int i=1; i<newRoot.transform.childCount; i++)    
                 Destroy(newRoot.transform.GetChild(i).gameObject);
        
        newRoot.transform.position = pos;
       
        newRoot = newRoot.GetComponent<RootManager>();
        newRoot.isNegative = !newRoot.isNegative;
        allRoots.Add(newRoot);
    }

    public void AddPieceToRoot()
    {
        foreach (RootManager rm in allRoots){
            rm.AddPiece(direction);
        }
    }

   public void GetPowerUp(RootManager rm , Vector3 pos)
   {
        AddRoot(rm,pos);
   }

   void Update()
   {
        if (Input.GetKey("right"))
        {
            direction = 1;
        }
       if (Input.GetKey("left"))
        {
            direction = -1;
        }
   }
}
