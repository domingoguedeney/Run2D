using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PapelAni : MonoBehaviour
{
    public bool ensuelo;
    public Transform check;
    public Animator ani;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    bool control=false;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        ensuelo = Physics2D.OverlapBox(check.position, dimensionesCaja, 0, queEsSuelo);


        if (ensuelo)
        {
            //Debug.LogWarning("Estoy tocando e Suelo! .");
            ani.SetBool("isGround", true);
            
        }
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(check.position, dimensionesCaja);
    }

}
