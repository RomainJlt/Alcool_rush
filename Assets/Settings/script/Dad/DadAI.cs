using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Cible que l'ennemi doit suivre (par exemple, le joueur)
    public Transform target;

    // Vitesse de déplacement de l'ennemi
    public float speed = 120f;

    // Distance à laquelle l'ennemi considère qu'il a atteint un waypoint
    public float nextWpDistance = 1f;

    // Distance minimale pour déclencher une attaque (l'ennemi s'arrête en dehors de cette portée)
    public float attackRange = 2f;

    // Chemin calculé par le Seeker
    public Path path;

    // Indice du waypoint actuel que l'ennemi essaie d'atteindre
    int currWp = 0;

    // Composant Seeker utilisé pour calculer le chemin
    public Seeker seeker;

    // Composant Rigidbody2D utilisé pour le mouvement physique de l'ennemi
    public Rigidbody2D rb;

    public Animator animator;

    public SpriteRenderer spriteRenderer; // Référence au SpriteRenderer

    public float attackCoolDown = 2f; // Temps de recharge entre les attaques
    private float currenCoolDown = 0f; // Temps restant avant

    public int damage = 1; // Dommages infligés par l'ennemi
    
    private Vector2 movement;

    // Méthode appelée au début de l'exécution
    void Start()
    {
        // Met à jour le chemin toutes les 0,5 secondes pour suivre la position du joueur
        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    // Méthode pour mettre à jour le chemin vers la cible
    void UpdatePath()
    {
        // Vérifie si le Seeker est prêt à calculer un nouveau chemin
        if (seeker.IsDone())
            // Demande un nouveau chemin du Seeker entre la position actuelle et la cible
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Méthode appelée lorsque le Seeker a terminé de calculer un chemin
    void OnPathComplete(Path p)
    {
        // Si le calcul a réussi (pas d'erreur), met à jour le chemin et réinitialise l'indice du waypoint
        if (!p.error)
        {
            path = p;
            currWp = 0;
        }
    }

void Update()
{
    // Met à jour l'animation de l'ennemi en fonction de la vitesse
    animator.SetFloat("Speed", rb.linearVelocity.magnitude);

    // Met à jour la rotation en fonction de la direction du déplacement
    if (rb.linearVelocity != Vector2.zero)
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, angle - 1);
    }

    currenCoolDown -= Time.deltaTime; // Réduit le temps de recharge restant

    if(currenCoolDown <= 0f)
    {
        // Calcule la distance entre l'ennemi et le joueur
        currenCoolDown =0;
    }
}

    // Méthode appelée à chaque frame fixe pour gérer les mouvements physiques
    void FixedUpdate()
    {
        // Si aucun chemin n'a été calculé ou si tous les waypoints ont été atteints, ne fait rien
        if (path == null || currWp >= path.vectorPath.Count)
        {
            return;
        }

        // Calcule la distance entre l'ennemi et le joueur
        float playerDistance = Vector2.Distance(target.transform.position, transform.position);

        // Si le joueur est en dehors de la portée d'attaque = on le poursuit
        if (playerDistance > attackRange)
        {
            // Calcule la direction vers le prochain waypoint
            Vector2 direction = ((Vector2)path.vectorPath[currWp] - rb.position).normalized;

            // Lisser la direction pour éviter des changements brusques (interpolation)
            Vector2 smoothDirection = Vector2.Lerp(rb.linearVelocity.normalized, direction, 0.1f);

            // Calcule la vitesse en fonction de la direction et de la vitesse spécifiée
            Vector2 velocity = smoothDirection * speed * Time.fixedDeltaTime;

            // Applique la vitesse calculée au Rigidbody2D
            rb.linearVelocity = velocity;

            // Calcule la distance entre l'ennemi et le waypoint actuel
            float distance = Vector2.Distance(rb.position, path.vectorPath[currWp]);

            // Si l'ennemi est suffisamment proche du waypoint, passe au suivant
            if (distance < nextWpDistance)
            {
                currWp++;
            }
        }else{
            if(currenCoolDown <= 0f)
            {
                Attack();

            }
        }
    }
    void Attack()
    {
        animator.SetBool("isAttacking", true);
        currenCoolDown = attackCoolDown; // Réinitialise le temps de recharge
        animator.SetTrigger("Attack"); 
        // Debug.Log("animation");
    }

    void EndOfAttack()
    {
        animator.SetBool("isAttacking", false);

        if(Vector2.Distance( transform.position, target.position) > attackRange)
        {
            target.GetComponent<PlayerHealth>().takeDamage(damage); // Appelle la méthode takeDamage sur le joueur
            Debug.Log("Attaque du joueur");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Affiche la portée

    }
}
