using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void MirrorRay (Material lazer, Vector3 rayhitposition) 
    {

    }
}
