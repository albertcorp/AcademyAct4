using UnityEngine;

public class Weakpoint : MonoBehaviour
{
    public float points = 10f;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Proyectil")
        {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj != null)
            {
                PlayerPoints playerPoints = obj.GetComponent<PlayerPoints>();
                if (playerPoints != null)
                {
                    playerPoints.AddPoints(points);
                }
            }
        }

        if (collision.gameObject.tag == "Proyectil2")
        {
            GameObject obj = GameObject.FindWithTag("Player2");
            if (obj != null)
            {
                PlayerPoints playerPoints = obj.GetComponent<PlayerPoints>();
                if (playerPoints != null)
                {
                    playerPoints.AddPoints(points);
                }
            }
        }
    }
}
