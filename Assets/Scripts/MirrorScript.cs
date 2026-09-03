using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    [SerializeField] private GameObject laserObject;

    public LaserSegment CreateNextSegment(Material material)
    {
        GameObject createdObject = Instantiate(laserObject);

        createdObject.transform.SetParent(transform, false);

        createdObject.GetComponent<LineRenderer>().material = material;

        LaserSegment segment = createdObject.GetComponent<LaserSegment>();

        segment.laserMaterial = material;

        return segment;
    }

    public void UpdateLaser(RaycastHit oldHit, Vector3 laserOrigin, LaserSegment segment)
    {
        LineRenderer lr = segment.gameObject.GetComponent<LineRenderer>();

        Vector3 hitPoint = oldHit.point;

        lr.SetPosition(0, hitPoint);

        Vector3 reflectedDir = CalculateAngle(oldHit, laserOrigin);

        RaycastHit hit;

        if (Physics.Raycast(hitPoint, reflectedDir, out hit))
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

                    segment.UpdateNextSegment(nextMirror, hit, hitPoint);
                }
                else if (hit.collider.CompareTag("LazerGoal"))
                {
                    hit.transform.gameObject.GetComponent<LazerGoal>().CheckGoal(segment.laserMaterial);

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
            lr.SetPosition(1, hitPoint + reflectedDir * 5000);

            segment.DeleteNextSegment();
        }
    }

    Vector3 CalculateAngle(RaycastHit oldHit, Vector3 laserOrigin)
    {
        Vector3 incomingDir = (oldHit.point - laserOrigin).normalized;

        return Vector3.Reflect(incomingDir, oldHit.normal);
    }
}