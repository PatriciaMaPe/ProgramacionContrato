using System;
using System.Diagnostics;

namespace ProgContrato
{
    class MainClass
    {
		class Chorras{
			public int X{
				get; set;
			}

			public override string ToString()
			{
				return X.ToString();
			}
		}

        [Conditional("DEBUG")]
		static void CreaTrazaEnArchivo(String nombre){
			Trace.Listeners.Add(new TextWriterTraceListener(nombre));
		}

        public static void Main(string[] args)
        {

			CreaTrazaEnArchivo("log");
			var v = new VectorFlexible<Chorras>();

			for (int i= 0; i< 10;i++){
				v.Add(new Chorras { X = i + 1 });
			}

			//v.Add(null);

			//for (int i = 0; i < v.Count; i++)
            //{
			//	Console.WriteLine(i + ": " + v[i]);
            //}

			while(!v.EsVacia()){
				v.Pop();
			}

			Trace.Close();
			return;



        }
    }
}
