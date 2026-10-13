namespace mmaldonadoS2.Views;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {

	try 
	{	        
		string pais = pkPaises.Items[pkPaises.SelectedIndex].ToString();
    DisplayAlertAsync("Alerta", pais, "ok");

			//FECHA

			string fecha = DpFecha.Date.ToString();
			string nombre = txtDato.Text;
            DisplayAlertAsync("Alerta", "Es pais seleccionado es"+ pais, "\n la fecha es "+ fecha, "ok");

        }
	catch (global::System.Exception ex)
	{

            DisplayAlertAsync("Alerta", "Selecciona un elemento", "ok");
			
        }

		 }

    private async void btnAbrir_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new vTarea());
    }
}