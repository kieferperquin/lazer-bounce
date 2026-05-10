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

                // put mirror detection system in here so no error can happen

                if (hit.collider.tag == "Mirror")
                {
                    hit.transform.gameObject.GetComponent<MirrorScript>().MirrorRay(LazerMaterial, hit, transform.position);
                }
                else if (hit.collider.tag == "LazerGoal")
                {
                    hit.transform.gameObject.GetComponent<LazerGoal>().CheckGoal(LazerMaterial);
                }
            }
        }
        else
        {
            lr.SetPosition(1, -transform.forward * 5000);
        }
    }
}