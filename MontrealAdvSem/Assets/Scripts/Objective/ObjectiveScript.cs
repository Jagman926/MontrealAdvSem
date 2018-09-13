using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //If current player passes over objective
        if (col.gameObject == Managers.PlayerManager.Instance.currPlayer)
        {
            //Remove Object from list
            Managers.GameManager.Instance.objectiveList.Remove(gameObject);
            //Destroy this object
            Destroy(gameObject);
        }
    }
}
