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
			Console.CursorLeft++;
			for (int i = 0; i < lastTypedWord.Length; ++i) {
				Console.CursorLeft -= 2;
				// these 2 sentenses can be replaced by PressKeyboard(' ');
				// but the typer doesn't really press the SPACE key.
				Console.Write(' ');
				base.Sleep();
			}
			Console.CursorLeft--;
		}
	}
}
