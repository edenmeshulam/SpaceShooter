using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _triplelaserPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private bool _isTripleShot = false;
    [SerializeField]
    private bool _isShield = false;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private UIManager _uIManager;

    private SpawnManager _spawnManager;
    private float _nextFireRate = -1f;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
            Debug.Log("Spawn Manager is NULL");
        if (_uIManager == null)
            Debug.Log("UI Manager is NULL");
    }

    void Update()
    {
        InitStartKeys();
        FireLaser();
        TripleShotDownRoutine();
        SpeedRoutine();
    }

    private void FireLaser()
    {
        //fire on press space:
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireRate)
        {
            _nextFireRate = Time.time + _fireRate;

            if (_isTripleShot) // fire 3 laser
                Instantiate(_triplelaserPrefab, transform.position, Quaternion.identity);
            else // fire one laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        }
    }

    public void AddScoreToPlayer(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }

    private void InitStartKeys()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 vector = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(vector * _speed * Time.deltaTime);

        TransformFunctions();
    }

    private void TransformFunctions()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

    public void TripleShotActive()
    {
        this._isTripleShot = true;
        StartCoroutine(TripleShotDownRoutine());
    }
    IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this._isTripleShot = false;
    }

    public void SpeedActive()
    {
        this._speed = 8.7f;
        StartCoroutine(SpeedRoutine());
    }

    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this._speed = 3.5f;
    }

    public void ShieldActive()
    {
        _shieldVisualizer.SetActive(true);
        _isShield = true;
    }

    public void Damage()
    {
        if (_isShield)
        {
            _shieldVisualizer.SetActive(false);
            _isShield = false;
            return;
        }

        _lives--;

        _uIManager.UpdateLives(_lives);

        if (_lives == 0)
        {
            _spawnManager.OnPlayerDead();
            Destroy(this.gameObject);
            _uIManager.ShowGameOver();
        }
    }
}
