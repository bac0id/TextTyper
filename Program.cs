using System;

namespace TextTyper
{
	public class Program
	{
		static void Main(string[] args) {
			TyperTest test = new TyperTest();
			test.TestTyper();
			Console.WriteLine();

			test.TestChangeTypingSpeed();
			Console.WriteLine();
			test.TestChangeMistakeRate();
			Console.WriteLine();

			Console.ReadKey();
		}
	}

	public class TyperTest
	{
		public Typer typer = new ConsoleTyper(20f,0.3f);
		public void TestTyper() {
			typer.TypeIn("Hi, I'm a typist.\n");
			typer.TypeIn("I can use keyboard well but sometimes I mistake the word.\n");
		}
		public void TestChangeTypingSpeed() {
			typer.CharPerSecond = 40f;
			typer.TypeIn("Licensing a repository\n");
			typer.TypeIn("Public repositories on GitHub are often used to share open source software. ");
			typer.TypeIn("For your repository to truly be open source, you'll need to license it so that others are free to use, change, and distribute the software.\n");
		}
		public void TestChangeMistakeRate() {
			typer.MistakeRate = 0.5f;
			typer.TypeIn("C# is an object-oriented, component-oriented programming language.\n");
			typer.TypeIn("C# provides language constructs to directly support these concepts, making C# a natural language in which to create and use software components.");
		}
	}
}
