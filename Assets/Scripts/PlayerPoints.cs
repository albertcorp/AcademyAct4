using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public float points;
    public TextMeshProUGUI scoreText;

    [SerializeField] Canvas _dieCanvas;
    [SerializeField] Canvas _winnerCanvas;

    void Start()
    {
        // Inicializar el texto con el puntaje inicial
        UpdateScoreText();
    }

    public void AddPoints(float pointsToAdd) 
    {
        points += pointsToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Actualizar el texto TMP con el puntaje actual
        scoreText.text = "Points: " + points.ToString();

        if (points == 100) 
        {
            _dieCanvas.gameObject.SetActive(true);
            _winnerCanvas.gameObject.SetActive(true);
        }
    }
}
