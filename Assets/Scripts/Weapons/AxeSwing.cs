using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    #region Properties
    private float duration = 0.60f;
    private float radius = 1.5f; // distancia del axe al owner durante el swing
    private int damage;

    float timer;
    Transform owner;
    #endregion

    #region Fields
    [SerializeField] bool drawRuntimeRadius = false; // Opcional: dibujar el radio en Play para depuración
    bool hasFinished = false;
    HashSet<Collider2D> damaged = new HashSet<Collider2D>();
    float sweep = 170f; // arco total del swing en grados
    float hitRadius = 0.25f; // radio para detectar impactos alrededor del sprite del hacha
    float startAngle;
    #endregion

    #region Unity Callbacks
    // Init acepta aimPosition para centrar el arco
    public void Init(Transform owner, int damage, float radius, Vector2 aimPosition)
    {
        this.owner = owner;
        this.damage = damage;
        this.radius = radius;

        // Calcula dirección objetivo; si aimPosition == owner pos, usa derecha por defecto
        Vector2 raw = aimPosition - (Vector2)owner.position;
        Vector2 dir = raw.sqrMagnitude > 0.0001f ? raw.normalized : Vector2.right;
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Empezamos el swing medio arco antes de la dirección de la mira (centrar el sweep)
        startAngle = baseAngle - (sweep * 0.5f);

        // Posición inicial según startAngle
        float rad = startAngle * Mathf.Deg2Rad;
        transform.position = owner.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;

        damaged.Clear();
        timer = 0f;
        hasFinished = false;
    }

    void Update()
    {
        if (owner == null) return;

        timer += Time.deltaTime;
        float normalizedTime = Mathf.Clamp01(timer / duration);

        // Ángulo actual del swing
        float currentAngle = startAngle + sweep * normalizedTime;
        float aRad = currentAngle * Mathf.Deg2Rad;

        // Posicionar el hacha describiendo el arco
        transform.position = owner.position + new Vector3(Mathf.Cos(aRad), Mathf.Sin(aRad), 0f) * radius;

        // Orientación visual del sprite: que mire hacia fuera/centro (ajusta si necesitas otra apariencia)
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle - 90f);

        // Detectar impactos alrededor de la posición del hacha y aplicar daño una vez por _target
        ApplyHitsAtPosition(transform.position);

        if (normalizedTime >= 1f && !hasFinished)
        {
            hasFinished = true;
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    void ApplyHitsAtPosition(Vector3 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, hitRadius);
        foreach (var hit in hits)
        {
            if (hit == null) continue;
            if (hit.transform == owner) continue;
            if (hit.CompareTag("Player")) continue;
            if (damaged.Contains(hit)) continue;

            IDamageable dmg = hit.GetComponent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
                damaged.Add(hit);
            }
        }
    }
    #endregion

}
