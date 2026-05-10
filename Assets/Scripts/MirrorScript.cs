using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    private LineRenderer lr;

    private float timer = 1;
    private bool hasReset = false;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (!hasReset && timer <= 0f)
        {
            hasReset = true;
            ResetRay();
        }
        else if (timer > 0)
        {
            timer -= .25f;
        }
    }

    public void MirrorRay(Material LazerMaterial, RaycastHit oldHit, Vector3 laserOrigin)
    {
        timer = 1;
        hasReset = false;

        lr.material = LazerMaterial;

        Vector3 hitPoint = oldHit.point;

        lr.SetPosition(0, hitPoint);

        Vector3 incomingDir = (hitPoint - laserOrigin).normalized;

        Vector3 normal = oldHit.normal;

        Vector3 reflectedDir = Vector3.Reflect(incomingDir, normal);

        RaycastHit hit;

        if (Physics.Raycast(hitPoint, reflectedDir, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

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
            lr.SetPosition(1, hitPoint + reflectedDir * 5000);
        }
    }

    public void ResetRay()
    {
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
    }
}