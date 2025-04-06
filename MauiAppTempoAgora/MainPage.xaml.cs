using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Button_buscar(object sender, EventArgs e)
        {
            try
            {

                lbl_res.Text = "";
                
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null) 
                    {


                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp. Max: {t.temp_max}ºC \n" +
                                         $"Temp. Min: {t.temp_min}ºC \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Velocidade do vento: {t.speed}m/s  \n" +
                                         $"Visibilidade: {t.visibility}m"; 


                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        lbl_res.Text = "Cidade informada não localizada";
                    }
                }
                else
                {
                    lbl_res.Text = "Preenche a cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de Conexão", "Sem acesso a internet. Verifique a conexão", "Ok");
            }
            
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }          
        }
    }
}
