using Newtonsoft.Json;
using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class VotacionService
    {
        public event Action<List<Pregunta>>? VotoRecibido;
        HttpListener listener = new();
        List<Pregunta> preguntas = new();

        public VotacionService()
        {
            listener.Prefixes.Add("http://127.0.0.1:4323/votacion/");
            EstablecerPreguntas();
        }

        private void EstablecerPreguntas()
        {
            Pregunta pregunta1 = new Pregunta()
            {
                Id = 1,
                _Pregunta = "¿Califique el sabor de nuestros productos?"
            };

            Pregunta pregunta2 = new Pregunta()
            {
                Id = 2,
                _Pregunta = "¿Como considera nuestro rango de precios?"
            };

            Pregunta pregunta3 = new Pregunta()
            {
                Id = 3,
                _Pregunta = "¿Como considera la atencion que recivio?"
            };

            preguntas.Add(pregunta1);
            preguntas.Add(pregunta2);
            preguntas.Add(pregunta3);
        }

        public void Iniciar()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                listener.BeginGetContext(ContextoRecibido, null);
            }
        }
        private string pregunta = "";
        private void ContextoRecibido(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ContextoRecibido, null);

            if (context.Request.Url != null)
            {
                if (context.Request.Url.LocalPath == "/votacion/pregunta")
                {
                    var json = JsonConvert.SerializeObject(preguntas);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    context.Response.ContentType = "application/json";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;
                    context.Response.Close();
                }
                else if (context.Request.HttpMethod == "POST" && (context.Request.Url.LocalPath == "/votacion/responder"))
                {
                    var stream = new StreamReader(context.Request.InputStream);
                    var json = stream.ReadToEnd();
                    List<Pregunta>? preguntas = JsonConvert.DeserializeObject<List<Pregunta>>(json);
                    context.Response.StatusCode = 200;
                    if (preguntas != null)
                    {
                        VotoRecibido?.Invoke(preguntas);
                    }

                    context.Response.Close();
                }
                

            }
        }
    }
}
