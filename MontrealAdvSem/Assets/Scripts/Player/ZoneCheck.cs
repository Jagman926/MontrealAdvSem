using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCheck : MonoBehaviour 
{
    //Zones
    [Header("Zones")]
    public bool inNoDormantZone;

	void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "NoDormantZone")
        {
            inNoDormantZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        {
            inNoDormantZone = false;
        }
    }
}
