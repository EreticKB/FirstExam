using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public PlaneControl ParentPlatform;
    private void OnTriggerEnter(Collider other)
    {
        ParentPlatform.TriggerSprung();
    }
}
