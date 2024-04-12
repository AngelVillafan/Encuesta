using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class EncuestaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Pregunta? Pregunta { get; set; }
        public string Error { get; set; } = "";
        //comandos
        //votos
        public int TotalSatisfaccion1 { get; set; }
        public int TotalSatisfaccion2 { get; set; }
        public int TotalSatisfaccion3 { get; set; }

        public double PorcentajeSatisfacion1 => TotalEncuestados == 0 ? 0 : TotalSatisfaccion1 * 100 / (TotalEncuestados * 5);
        public double PorcentajeSatisfacion2 => TotalEncuestados == 0 ? 0 : TotalSatisfaccion2 * 100 / (TotalEncuestados * 5);
        public double PorcentajeSatisfacion3 => TotalEncuestados == 0 ? 0 : TotalSatisfaccion3 * 100 / (TotalEncuestados * 5);

        public int TotalEncuestados { get; set; }


        public int Total { get; set; }

        VotacionService server = new();
        //metodos

        public EncuestaViewModel()
        {
            server.Iniciar();
            server.VotoRecibido += Server_VotoRecibido;
        }


        public void Actualizar(string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void Server_VotoRecibido(List<Pregunta> preguntas)
        {

            foreach (var pregunta in preguntas)
            {
                switch (pregunta.Id)
                {
                    case 1: TotalSatisfaccion1 += pregunta.Respuesta; break;
                    case 2: TotalSatisfaccion2 += pregunta.Respuesta; break;
                    case 3: TotalSatisfaccion3 += pregunta.Respuesta; break;

                }
            }

            TotalEncuestados += 1;

            Actualizar();
        }


    }
}
