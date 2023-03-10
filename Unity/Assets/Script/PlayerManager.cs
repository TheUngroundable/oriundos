using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
   public int PlayerID;
   public int pieces;
   public List<RootManager> allRoots = new List<RootManager>();
   public float direction; // da -1 a 1
   public bool isDebug;
   public bool isAlive = true;

    public AudioManager audioManager;

   private void Start()
   {
        isAlive = true;
       audioManager = GameObject.FindObjectOfType<AudioManager>();
   }


    public void SetDirection(float newDirection){
        if(!isDebug)
        direction = newDirection;
    }


    public void AddRoot(RootManager rm , Vector3 pos)
    {
        RootManager newRoot = Instantiate(rm,Vector3.zero,Quaternion.identity,transform);
        
        
        for(int i=1; i<newRoot.transform.childCount; i++)    
                 Destroy(newRoot.transform.GetChild(i).gameObject);
        
        newRoot.transform.position = pos;
       
        newRoot = newRoot.GetComponent<RootManager>();
        int random = Random.Range(0,10);
        newRoot.isNegative = random % 2 == 0;
        allRoots.Add(newRoot);
        audioManager.PlayGrow();
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

   public void DeleteRoot(RootManager rt)
   {
      allRoots.Remove(rt);
      
      if(allRoots.Count<=0)
      {
         isAlive = false;
        GameObject.FindObjectOfType<GameManager>().CheckLoose();
      }
       
   }

}
