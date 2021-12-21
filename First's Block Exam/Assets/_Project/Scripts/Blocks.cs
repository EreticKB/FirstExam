using System.Collections;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private int HP = 10;
   
    public bool GetDamage()
    {
        HP--;
        Debug.Log("HP: "+HP);
        if (HP > 0) return true;
        Debug.Log("LowHP");
        StartCoroutine(Death());
        return false;
    }
    IEnumerator Death()
    {
        Debug.Log("I'm at deathdoor");
        yield return new WaitForEndOfFrame();
        Debug.Log("I'm dead");
        Destroy(gameObject);
    }
}
