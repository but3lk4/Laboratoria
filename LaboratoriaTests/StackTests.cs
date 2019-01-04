using System;
using NUnit.Framework;
using Laboratoria.Stack;

namespace LaboratoriaTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Push_ArgumentIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<string>(); //inicjalizacja 

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException); //przywołanie metody z argumentem null ma wywołać wyjątek
        }

        [Test]
        public void Push_ValidArgument_AddObjectToStack()
        {
            var stack = new Stack<string>();

            stack.Push("x"); //dodanie do stosu argumentu

            Assert.That(stack.Count, Is.EqualTo(1)); // sprawdzenie czy zgadza się ilość argumentów na stosie

        }
        [Test]
        public void Push_CheckStackIfNoArguments_ReturnsZero() //test sprawdza czy przy braku argumentów na stosie wynikiem będzie 0
        {
            var stack = new Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        //testy do metody pop
        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);// jeśli nie ma nic na stosie, wyrzucany jest wyjątek 
        }

        [Test]
        public void Pop_StackWithSomeObjects_ReturnsTopObject()  // test sprawdza czy zwracana jest odpowiednia(ze szczytu) wartość ze stosu
        {
            var stack = new Stack<string>(); // inicjalizacja stosu

            stack.Push("x");  // wrzucamy argumenty na stos
            stack.Push("y");
            stack.Push("z");

            var result = stack.Pop();  // używamy Pop

            Assert.That(result, Is.EqualTo("z")); // sprawdzamy czy wartość result zgadza się z ostatnio wrzuconym na stos elementem
        }

        [Test]
        public void Pop_StackWithSomeObjects_RemovesTopObject() // test sprawdza czy usuwany jest odpowiedni element stosu
        {
            var stack = new Stack<string>();

            stack.Push("x");
            stack.Push("y");
            

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(1));  //sprawdzamy tylko czy zgadza się liczba elementów na stosie

        }
        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException() // jeśli nie ma nic na stosie, wyrzucany jest wyjątek 
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }
        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTopOfTheStack() // test sprawdza czy zwracana jest prawidłowa wartość ze stosu
        {
            var stack = new Stack<string>();

            stack.Push("x");
            stack.Push("y");
            stack.Push("z");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("z"));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTopOfTheStack() // sprawdzamy czy metoda peek nie usuwa ostatniej wartości
        {
            var stack = new Stack<string>();

            stack.Push("x");
            stack.Push("y");
            stack.Push("z");

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(3));

        }
    }
}

