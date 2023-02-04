using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
   public int PlayerID;
   public int pieces;
   public List<RootManager> allRoots = new List<RootManager>();
   public float direction; // da -1 a 1
  

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
   }

    public void AddRoot()
    {
        RootManager newRoot = Instantiate(allRoots[0],Vector3.zero,Quaternion.identity,transform);

        for(int i=1; i<newRoot.transform.childCount; i++)    
                 Destroy(newRoot.transform.GetChild(i).gameObject);

        newRoot.transform.localPosition = allRoots[0].lastPiece.transform.localPosition;
        newRoot = newRoot.GetComponent<RootManager>();
        newRoot.isNegative = true;
        allRoots.Add(newRoot);
    }

    public void AddPieceToRoot()
    {
        foreach (RootManager rm in allRoots)
            rm.AddPiece(direction);
    }

   public void GetPowerUp(RootManager rm)
   {
        AddRoot();
   }


   
}
