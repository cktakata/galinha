using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class veiculo : MonoBehaviour {

	public int mover;
	private Rigidbody2D carro;
	private float velocidade;
	private float posInicialY;
	public GameObject veiculoPreFab;
	public float posInicial;
	public int direcao;

	// Use this for initialization
	void Start () {
		carro = GetComponent<Rigidbody2D> ();
		posInicialY = carro.position.y;
		velocidade = mover * int.Parse (carro.tag.Substring(1).ToString()) * direcao;
	}
	
	// Update is called once per frame
	void Update () {
		carro.velocity = new Vector2 (velocidade, posInicialY);
	}

	void OnBecameInvisible() {
		Instantiate (veiculoPreFab, new Vector2 (posInicial, posInicialY), transform.localRotation);
		Destroy (this.gameObject);
	}
}
