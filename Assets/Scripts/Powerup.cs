using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerUps
    {
        TripleShot,
        Speed,
        Shield
    };

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerUpID;
    void Start()
    {
       
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerUpID)
                {
                    case (int)PowerUps.TripleShot:
                        player.TripleShotActive();
                        break;
                    case (int)PowerUps.Speed:
                        player.SpeedActive();
                        break;
                    case (int)PowerUps.Shield:
                        player.ShieldActive();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
