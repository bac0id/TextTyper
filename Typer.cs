using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace TextTyper {
    public abstract class Typer {
        protected const float DefaultTypingSpeed = 20f;
        protected const float DefaultMistakeRate = 0.05f;

        protected Random r = new Random();
        protected int sleepTime;

        public float MistakeRate { get; set; }
        public float CharPerSecond {
            get => 1000 / sleepTime;
            set => this.sleepTime = (int)(1000 / value);
        }

        public Typer(float charPerSecond = DefaultTypingSpeed, float mistakeRate = DefaultMistakeRate) {
            this.CharPerSecond = charPerSecond;
            if (mistakeRate > 1f) {
                mistakeRate = 1f;
            } else if (mistakeRate < 0f) {
                mistakeRate = 0f;
            }
            this.MistakeRate = mistakeRate;
        }

        public abstract void WriteChar(char c);

        public abstract void EraseLastWord(string lastWord);

        public void TypeIn(string sentense) {
            string[] strs;
            strs = Regex.Split(sentense, @"(?<=[ ,.!?:;&-])");
            strs = strs.Where(str => str != string.Empty).ToArray();
            this.TypeIn(strs);
        }

        public void TypeIn(string[] words) {
            int p = 0;
            bool isLastWordMistaken = false;
            while (p < words.Length || isLastWordMistaken) {
                if (isLastWordMistaken) {
                    // erase mistaken word
                    this.EraseLastWord(words[--p]);
                    isLastWordMistaken = false;
                } else {
                    // type in next word
                    bool isMistaken = r.NextDouble() < this.MistakeRate;
                    if (isMistaken) {
                        this.WriteMistakenWord(words[p]);
                        isLastWordMistaken = true;
                    } else {
                        this.WriteWord(words[p]);
                    }
                    ++p;
                }
            }
        }

        protected void PressKeyboard(char c) {
            this.WriteChar(c);
            this.Sleep();
        }

        private void WriteWord(string word) {
            foreach (var c in word) {
                this.PressKeyboard(c);
            }
        }

        private void WriteMistakenWord(string word) {
            int mistakenCharCnt = r.Next(1, word.Length);
            int[] mistakenCharIdx = new int[mistakenCharCnt];
            for (int i = 0; i < mistakenCharIdx.Length; i++) {
                mistakenCharIdx[i] = r.Next(word.Length);
            }
            for (int i = 0; i < word.Length; i++) {
                char c = word[i];
                if (mistakenCharIdx.Contains(i)) {
                    c = this.GetOtherChar(word[i]);
                }
                this.PressKeyboard(c);
            }
        }

        private char GetOtherChar(char c) {
            char c1;
            if (c >= 'A' && c <= 'Z') {
                c1 = (char)r.Next('A', 'Z' + 1);
            } else if (c >= '0' && c <= '9') {
                c1 = (char)r.Next('0', '9' + 1);
            } else {
                c1 = (char)r.Next('a', 'z' + 1);
            }
            return c1;
        }

        protected void Sleep() {
            Thread.Sleep(sleepTime);
        }
    }
}
