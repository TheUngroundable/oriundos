using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public int pieces;
    public GameObject lastPiece ;
    public bool isNegative;
    private bool isAlive = true;

    private void Update(){
        if(!lastPiece && this.gameObject.activeSelf){
            lastPiece= transform.GetChild(0).gameObject;
        }
    }

   public void AddPiece(float direction)
   {
        if(isAlive && lastPiece)
        {
            GameObject curPiece = Instantiate(lastPiece,Vector3.zero,Quaternion.identity,transform);
            if(isNegative)
                direction = -direction*Random.Range(0f,1.5f);
            else
                direction = direction*Random.Range(0f,1.5f);
                
            curPiece.transform.localPosition =  lastPiece.transform.localPosition + new Vector3(direction,Random.Range(0f,-2f),0);
            pieces++;
            
            if(lastPiece.transform.GetSiblingIndex()!=0)
             Destroy(lastPiece.GetComponent<BoxCollider2D>());
          
            lastPiece = curPiece;
        }
   }

   public void GetPowerUp(Vector3 pos)
   {
        transform.parent.GetComponent<PlayerManager>().GetPowerUp(this,pos);
   }

    public void GetObstacle()
   {
        isAlive = false;
        transform.parent.GetComponent<PlayerManager>().DeleteRoot(this);
   }
}
