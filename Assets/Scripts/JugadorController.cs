using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorController : MonoBehaviour
{
    //Declaro la variable de tipo RigidBody que luego asociaremos con nuestro jugador
    public Rigidbody rb;

    //Inicializo el contador de coleccionables recogidos
    private int contador;

    //Inicializo variables para los textos
    public Text textoContador, textoGanar;

    //Declaro la variable publica para poder modificarla desde la Inspector window
    public float velocidad;

    //El tiempo que durara en segundos despues de la partida ganada
    public float delay = 5f;

    void Start()
    {
        //Capturo esa variable al iniciar el juego
        rb = GetComponent<Rigidbody>();

        //Iniciar contador
        contador = 0;

        //Actualizo el texto del contador por primera vez
        setTextoContador();

        //Inicio el texto de ganar a vacio
        textoGanar.text = "";
    }

    // Para que se sincronice con los frames de fisica del motor
    void FixedUpdate()
    {
        //Estas variables nos capturan el movimiento en horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        //Un vector 3 es un trio de posiciones en el espacio XYZ, en este caso el que corresponde al movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        //Asigno ese movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento * velocidad);
        
    }

    //Se ejecuta al entrar a un objeto con la opcion isTrigger seleccionada
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coleccionable"))
        {
            //Sonido de la moneda
            soundManager.instance.coinssource.PlayOneShot(soundManager.instance.coinSound);

            //Desactiva el objeto
            other.gameObject.SetActive(false);

            //Incremento el contador en uno (tambien de puede hacer como contador++)
            contador++;

            //Actualizo el texto del contador
            setTextoContador();

        }
    }

    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
        if(contador >= 12){
            textoGanar.text = "¡Ganaste!";
            Invoke("menuPrincipal", delay);
        }

    }

    //Funcion de cargar menu principal
    private void menuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
