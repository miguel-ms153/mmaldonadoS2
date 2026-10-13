using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace mmaldonadoS2.Views;

public partial class vTarea : ContentPage
{
	public vTarea()
	{
		InitializeComponent();
	}

    private async void OnCalcularClicked(
       object sender, EventArgs e)
    {
        // 1. Validar selección del estudiante
        if (EstudiantePicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Validación",
                "Seleccione un estudiante.",
                "Aceptar");
            return;
        }

        // 2. Leer y validar las cuatro notas
        if (!TryLeerNota(Seguimiento1Entry.Text, out double s1) ||
            !TryLeerNota(Examen1Entry.Text, out double e1) ||
            !TryLeerNota(Seguimiento2Entry.Text, out double s2) ||
            !TryLeerNota(Examen2Entry.Text, out double e2))
        {
            await DisplayAlert(
                "Error",
                "Ingrese todas las notas como números entre 0 y 10.",
                "Aceptar");
            return;
        }

        // 3. Calcular los parciales ponderados
        double parcial1 = (s1 * 0.3) + (e1 * 0.2);
        double parcial2 = (s2 * 0.3) + (e2 * 0.2);

        // 4. Calcular la nota final
        double notaFinal = parcial1 + parcial2;

        // 5. Determinar el estado
        string estado;

        if (notaFinal >= 7)
        {
            estado = "Aprobado";
        }
        else if (notaFinal >= 5)
        {
            estado = "Complementario";
        }
        else
        {
            estado = "REPROBADO";
        }

        // 6. Obtener estudiante y fecha
        string nombre = EstudiantePicker.SelectedItem.ToString()
            ?? "Sin nombre";

        string fecha = FechaPicker.Date.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        // 7. Actualizar los resultados en pantalla
        Parcial1Label.Text =
            $"Nota Parcial 1: {parcial1:F2} / 5";

        Parcial2Label.Text =
            $"Nota Parcial 2: {parcial2:F2} / 5";

        ResultadoLabel.Text =
            $"Nota Final: {notaFinal:F2} / 10\n" +
            $"Estado: {estado}";

        // 8. Mostrar reporte con saltos de línea
        string reporte =
            $"Nombre: {nombre}\n" +
            $"Fecha: {fecha}\n" +
            $"Nota Parcial 1: {parcial1:F2}\n" +
            $"Nota Parcial 2: {parcial2:F2}\n" +
            $"Nota Final: {notaFinal:F2}\n" +
            $"Estado: {estado}";

        await DisplayAlert(
            "Resultado de Calificaciones",
            reporte,
            "Aceptar");
    }

    private bool TryLeerNota(string? texto, out double nota)
    {
        nota = 0;

        if (string.IsNullOrWhiteSpace(texto))
            return false;

        // Aceptar decimales con punto o coma.
        string valor = texto.Trim().Replace(',', '.');

        if (!double.TryParse(
                valor,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out nota))
        {
            return false;
        }

        return nota >= 0 && nota <= 10;
    }
}
