
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            float random = Random.Range(-8f, 8f);
            transform.position = new Vector3(random, 7f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            _player.AddScoreToPlayer(10);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            if (_player != null)
                _player.Damage();
            Destroy(this.gameObject);
        }
    }
}
