using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galinha : MonoBehaviour {

	private Vector3 posicao;

	private float posRetaX, posRetaY;
	private float posClickX, posClickY;
	private bool AcimaA, AcimaB;
	private Rigidbody2D player;
	private float velocidadeX, velocidadeY;
	public float velocidade;
	private SpriteRenderer playerSR;
	private AudioSource[] playerAS;
	private AudioSource audioMorri;
	private AudioSource audioAndando;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody2D> ();
		playerSR = GetComponent<SpriteRenderer> ();
		playerAS = GetComponents<AudioSource> ();
		audioMorri = playerAS [0];
		audioAndando = playerAS[1];
		velocidadeX = 0;
		velocidadeY = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			posicao = Input.mousePosition;

			posClickX = posicao.x;
			posClickY = posicao.y;
			//Debug.LogError ("Posição clicada: " + posClickX.ToString() + "," + posClickY.ToString());

			// Cálculo da posição referente à reta "a"
			posRetaX = (Screen.width * posClickY) / Screen.height;
			posRetaY = (Screen.height * posClickX) / Screen.width;
			AcimaA = (posClickX <= posRetaX) && (posClickY >= posRetaY);
			//Debug.LogError ("Reta A:" + posRetaX.ToString() + "," + posRetaY.ToString());
			//Debug.LogError (AcimaA ? "Está ACIMA" : "Está ABAIXO");

			// Cálculo da posição referente à reta "b"
			posRetaX = Screen.width - (Screen.width * posClickY) / Screen.height;
			posRetaY = Screen.height - (Screen.height * posClickX) / Screen.width;
			AcimaB = (posClickX >= posRetaX) && (posClickY >= posRetaY);
			//Debug.LogError ("Reta B:" + posRetaX.ToString() + "," + posRetaY.ToString());
			//Debug.LogError (AcimaB ? "Está ACIMA" : "Está ABAIXO");

			if (AcimaA && AcimaB) {
				velocidadeY = velocidade;
			} else if (AcimaA && !AcimaB) {
				if (player.position.x > -8) {
					velocidadeX = velocidade * -1;
					playerSR.flipX = true;
				} else {
					PlayAudio (audioAndando);
				}
			} else if (!AcimaA && AcimaB) {
				if (player.position.x < 8) {
					velocidadeX = velocidade;
					playerSR.flipX = false;
				} else {
					PlayAudio (audioAndando);
				}
			} else if (!AcimaA && !AcimaB) {
				if (player.position.y > -4.75) {
					velocidadeY = velocidade * -1;
				} else {
					PlayAudio (audioAndando);
				}
			}


		} else {
			velocidadeX = 0;
			velocidadeY = 0;
		}
		player.velocity = new Vector2(velocidadeX, velocidadeY);
	}

	void OnCollisionEnter2D(Collision2D colisao) {
		if (colisao.gameObject.tag.Substring (0, 1) == "V") {
			player.position = new Vector2(0,-4.75f);
			PlayAudio (audioMorri);
		}
	}

	void PlayAudio(AudioSource audio) {
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
