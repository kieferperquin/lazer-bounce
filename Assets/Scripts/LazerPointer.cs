using UnityEngine;


public class LazerPointer : MonoBehaviour
{
    [SerializeField] private Material LazerMaterial;

    public GameObject lazer;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lazer.GetComponent<Renderer>().material = LazerMaterial;
        lr.material = LazerMaterial;
    }

    void Update()
    {
        lr.SetPosition(0, lazer.transform.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                hit.transform.gameObject.GetComponent<MirrorScript>().MirrorRay(LazerMaterial, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, -transform.forward * 5000);
        }
    }
}