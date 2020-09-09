using System;

namespace TextTyper
{
	public class ConsoleTyper : Typer
	{
		public ConsoleTyper(float charPerSecond = DefaultTypingSpeed, float mistakeRate = DefaultMistakeRate)
			: base(charPerSecond, mistakeRate) {
		}

		public override void WriteChar(char c) {
			Console.Write(c);
		}

		public override void EraseLastWord(string lastTypedWord) {
			for (int i = 0; i < lastTypedWord.Length; ++i) {
				Console.Write('\b');
				Console.Write(' ');
				Console.Write('\b');
				base.Sleep();
			}
		}
	}
}
