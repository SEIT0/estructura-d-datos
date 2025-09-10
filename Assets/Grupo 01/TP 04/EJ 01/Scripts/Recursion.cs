using UnityEngine;
using UnityEngine.UI;

public class Recursion : MonoBehaviour
{
    public InputField inputField;
    public Text resultadoText;

    public void FibonacciButton()
    {
        if (int.TryParse(inputField.text, out int n) && n >= 0)
        {
            string secuencia = "";
            for (int i = 0; i < n; i++)
            {
                secuencia += Fibonacci(i) + (i < n - 1 ? ", " : "");
            }
            resultadoText.text = secuencia;
        }
        else
        {
            resultadoText.text = "Ingrese un número válido (>= 0).";
        }
    }

    public void FactorialButton()
    {
        if (int.TryParse(inputField.text, out int n) && n >= 0)
        {
            resultadoText.text = $"Factorial: {Factorial(n)}";
        }
        else
        {
            resultadoText.text = "Ingrese un número válido (>= 0).";
        }
    }

    public void SumaButton()
    {
        if (int.TryParse(inputField.text, out int n) && n >= 0)
        {
            resultadoText.text = $"La suma es: {SumaRecursiva(n -1)}";
        }
        else
        {
            resultadoText.text = "Ingrese un número válido (>= 0).";
        }
    }

    public void PiramideButton()
    {
        if (int.TryParse(inputField.text, out int altura) && altura > 0)
        {
            resultadoText.text = PiramideRecursiva(altura, 1, "");
        }
        else
        {
            resultadoText.text = "Ingrese una altura válida (> 0).";
        }
    }

    public void PalindromoButton()
    {
        string frase = inputField.text;
        string limpia = frase.Replace(" ", "").ToLower();
        bool esPal = EsPalindromo(limpia, 0, limpia.Length - 1);
        resultadoText.text = esPal ? "Es palíndromo" : "No es palíndromo";
    }

    int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    int SumaRecursiva(int n)
    {
        if (n <= 0) return 0;
        return n + SumaRecursiva(n - 1);
    }

    string PiramideRecursiva(int altura, int nivel, string resultado)
    {
        if (nivel > altura) return resultado;

        int cantidadX = (nivel * 2) - 1;
        int espacios = altura - nivel;
        string linea = new string (' ', espacios * 2) + new string('x', cantidadX) + new string(' ', espacios * 2) + "\n";

        return PiramideRecursiva(altura, nivel + 1, resultado + linea);
    }

    bool EsPalindromo(string texto, int inicio, int fin)
    {
        if (inicio >= fin) return true;
        if (texto[inicio] != texto[fin]) return false;
        return EsPalindromo(texto, inicio + 1, fin - 1);
    }
}