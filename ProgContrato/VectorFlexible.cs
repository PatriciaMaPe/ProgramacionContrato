using System;
using System.Diagnostics;
using System.Text;

namespace ProgContrato
{   
	//Crece de tamaño segun le insertamos eleentos
    public class VectorFlexible<T>
    {
		T[] elementos;
        public VectorFlexible(int capacidad = 5)
        {
			this.Capacidad = capacidad;
			this.Count = 0;
			this.elementos = new T[capacidad];
        }

		public void Add(T x){
			Trace.WriteLine("Add(x): Comienza");
			Trace.Indent();//Hacia la derecha para indicar que esta dentor de un metodo
            //Add no funciona correctamente si l parametro que se le pasa es null
			Debug.Assert(x != null, "Add(x): x no puede ser null");
			Trace.WriteLine("Expandir vector");
			AsegurarEspacioPara(this.Count + 1);
			this.elementos[this.Count] = x;
			++this.Count;
			VuelcaPorConsola();
			this.ChkInvariantes();
			Trace.Unindent();
            Trace.WriteLine("Add(x): Termina");
		}

        //Elimina y delvuelve el ultimo elemento
        //Metodo pregunta y accion a la vez, lo dividimos en preguna y accion
		//public T Pop(){
		//	--this.Count;
		//	return this.elementos[this.Count];
		//}

		public void Pop()
        {
			this.ChkNoEsVacia("Pop");
            --this.Count;
			this.ChkInvariantes();

        }

		public T Top
        {
			get{
				this.ChkNoEsVacia("Top");
				this.ChkInvariantes();
				return this.elementos[this.Count];
			}
            
        }

		[Conditional("DEBUG")]
		void ChkNoEsVacia(string nombreFuncion)
        {
			Debug.Assert(this.Count > 0 , nombreFuncion + ": vector no puede ser vacio");
            
        }


		[Conditional("DEBUG")]
		void ChkInvariantes(){
			Debug.Assert(this.Count <= this.Capacidad);
		}

		[Conditional("DEBUG")]
		void ChkLimites(string nombreFuncion, int pos){
			Debug.Assert(pos >= 0, nombreFuncion + ": pos no puede ser 0"); //asegura que pos es mayor que 0, el mensaje salta cuando nos e cumple
			Debug.Assert(pos < this.Count, nombreFuncion +  "pos muy grande");
		}

		public T this[int pos]{
			get{
				this.ChkLimites("[]", pos);
				return this.elementos[pos];
			}
			set{
				this.ChkLimites("[]", pos);

				this.elementos[pos] = value;
			}
		}

		public bool EsVacia(){
			this.ChkInvariantes();
			return this.Count == 0;
		}

        //tamano real del vector
		public int Count {
			get; private set;
		}

		public int Capacidad{
			get; private set;
		}

		void AsegurarEspacioPara(int x){
			if(this.Capacidad<x){
				while(this.Capacidad<x){
					this.Capacidad *= 2;
				}

				var v = new T[this.Capacidad];
				Array.Copy(this.elementos, v, this.Count);
				this.elementos = v;
			}
            //postcondiciones
			Debug.Assert(this.Capacidad == this.elementos.Length);
			Debug.Assert(this.elementos.Length >= x);
			return;
		}

		public override string ToString()
		{
			var toret = new StringBuilder();

			toret.Append("[ ");


			for (int i = 0; i < this.Count; i++){
				toret.Append(this.elementos[i]);
				toret.Append(" ,");
			}

			toret.Append(" ]");

			return toret.ToString();
		}

		[Conditional("DEBUG")]
		void VuelcaPorConsola(){ //Si estamos en un perfil que no sea el de depuracion no se compila el metodo
			Console.WriteLine(this);
		}
    }
}
