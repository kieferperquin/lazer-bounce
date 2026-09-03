using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LaserSegment segment;
    private LineRenderer lr;

    public GameObject lazer;

    void Start()
    {
        segment = GetComponent<LaserSegment>();

        lr = GetComponent<LineRenderer>();

        lazer.GetComponent<Renderer>().material = segment.laserMaterial;
        lr.material = segment.laserMaterial;

        lr.SetPosition(0, lazer.transform.position);

    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

                if (hit.collider.CompareTag("Mirror"))
                {
                    MirrorScript nextMirror = hit.collider.GetComponent<MirrorScript>();

                    if (segment.nextSegment == null)
                    {
                        segment.CreateNextSegment(nextMirror);
                    }
                    
                    segment.UpdateNextSegment(nextMirror, hit, transform.position);
                }
                else if (hit.collider.CompareTag("LazerGoal"))
                {
                    hit.transform.gameObject.GetComponent<LaserGoal>().CheckGoal(segment.laserMaterial);

                    segment.DeleteNextSegment();
                }
                else
                {
                    segment.DeleteNextSegment();
                }
            }
        }
        else
        {
            lr.SetPosition(1, -transform.forward * 5000);

            segment.DeleteNextSegment();
        }
    }
}