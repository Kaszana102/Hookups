using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] GameObject explosionColliders;

    [SerializeField] float explosionCollidersTime;
    [SerializeField] float explosionParticlesTime;

    [SerializeField] int damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        explosionParticles = GameObject.Instantiate(explosionParticles.gameObject).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);
        explosionColliders.SetActive(false);

    }    

    public void Explode(Vector3 position)
    {
        explosionColliders.transform.position = position;
        explosionParticles.transform.position = position;

        StartCoroutine(Exploding());
    }

    IEnumerator Exploding()
    {
        explosionColliders.SetActive(true);
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();        

        float start = Time.time;
        while (Time.time < start + explosionCollidersTime)
        {
            yield return null;
        }
        explosionColliders.SetActive(false);

        while (Time.time < start + explosionParticlesTime)
        {
            yield return null;
        }
        explosionParticles.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        DamageableObject player = collision.gameObject.GetComponent<DamageableObject>();
        if (player!=null)
        {
            player.receiveDamage(damage);
        }
    }
}
