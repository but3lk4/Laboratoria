using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria.Stack
{
    
        public class Stack<T> // LIFO, Last In First Out
        {
            private readonly List<T> _list = new List<T>();

            public int Count => _list.Count;

            public void Push(T obj) // umieszczenie wartości na szczycie stosu
            {
                if (obj == null)
                    throw new ArgumentNullException();

                _list.Add(obj);
            }

            public T Pop() // ściągnięcie obiektu ze stosu i zwrócenie jego wartości
            {
                if (_list.Count == 0)
                    throw new InvalidOperationException();

                var result = _list[_list.Count - 1];
                _list.RemoveAt(_list.Count - 1);

                return result;
            }

            public T Peek() // sprawdzenie ostatniej wartości na stosie
            {
                if (_list.Count == 0)
                    throw new InvalidOperationException();

                return _list[_list.Count - 1];
            }

        }
    
}
