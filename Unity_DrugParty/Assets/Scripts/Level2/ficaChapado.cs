using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ficaChapado : MonoBehaviour
{
    void Update()
    {
        if (Player.chapado || Player.bebado || Player.drogado)
        {
            gameObject.SetActive(false);
        }
    }
}
