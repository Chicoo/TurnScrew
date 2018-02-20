using System;
using Xunit;

namespace TurnScrew.Wiki.SearchEngine.UnitTests {
	
	
	public class ToolsTests : TestsBase {

		[Fact]
		public void RemoveDiacriticsAndPunctuation() {
			string testPhrase = "Wow, thìs thing sèems really cool!";
			string testWord = "Wòrd";

			Assert.Equal("wow this thing seems really cool", Tools.RemoveDiacriticsAndPunctuation(testPhrase, false));
			Assert.Equal("word", Tools.RemoveDiacriticsAndPunctuation(testWord, true));
		}

		[Fact]
		public void IsSplitChar() {
			foreach(char c in ",.;:-\"'!?^=()<>\\|/[]{}«»*°§%&#@~©®±") {
				Assert.True(Tools.IsSplitChar(c), "Char is a split char");
			}
			foreach(char c in "abcdefghijklmnopqrstuvwxyz0123456789òçàùèéì€$£") {
				Assert.False(Tools.IsSplitChar(c), "Char is not a split char");
			}
		}

		[Fact]
		public void SkipSplitChars() {
			Assert.Equal(0, Tools.SkipSplitChars(0, "hello"));
			Assert.Equal(1, Tools.SkipSplitChars(0, " hello"));
			Assert.Equal(7, Tools.SkipSplitChars(6, "Hello! How are you?"));
		}

		[Fact]
		public void Tokenize() {
			string input = "Hello, there!";
			WordInfo[] expectedOutput = new WordInfo[] { new WordInfo("Hello", 0, 0, WordLocation.Content), new WordInfo("there", 7, 1, WordLocation.Content) };

			WordInfo[] output = Tools.Tokenize(input, WordLocation.Content);

			Assert.Equal(expectedOutput.Length, output.Length);

			for(int i = 0; i < output.Length; i++) {
				Assert.Equal(expectedOutput[i].Text, output[i].Text);
				Assert.Equal(expectedOutput[i].FirstCharIndex, output[i].FirstCharIndex);
				Assert.Equal(expectedOutput[i].WordIndex, output[i].WordIndex);
			}
		}

		[Fact]
		public void Tokenize_OneWord() {
			string input = "todo";
			WordInfo[] expectedOutput = new WordInfo[] { new WordInfo("todo", 0, 0, WordLocation.Content) };

			WordInfo[] output = Tools.Tokenize(input, WordLocation.Content);

			Assert.Equal(expectedOutput.Length, output.Length);

			for(int i = 0; i < output.Length; i++) {
				Assert.Equal(expectedOutput[i].Text, output[i].Text);
				Assert.Equal(expectedOutput[i].FirstCharIndex, output[i].FirstCharIndex);
				Assert.Equal(expectedOutput[i].WordIndex, output[i].WordIndex);
			}
		}

		[Fact]
		public void Tokenize_OneWordWithSplitChar() {
			string input = "todo.";
			WordInfo[] expectedOutput = new WordInfo[] { new WordInfo("todo", 0, 0, WordLocation.Content) };

			WordInfo[] output = Tools.Tokenize(input, WordLocation.Content);

			Assert.Equal(expectedOutput.Length, output.Length);

			for(int i = 0; i < output.Length; i++) {
				Assert.Equal(expectedOutput[i].Text, output[i].Text);
				Assert.Equal(expectedOutput[i].FirstCharIndex, output[i].FirstCharIndex);
				Assert.Equal(expectedOutput[i].WordIndex, output[i].WordIndex);
			}
		}

        [Fact]
        public void Tokenize_Text_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Tools.Tokenize(null, WordLocation.Content));
            Assert.Equal("Value cannot be null.\r\nParameter name: text", ex.Message);
        }

        [Fact]
		public void RemoveStopWords() {
			WordInfo[] input = new WordInfo[] { new WordInfo("I", 0, 0, WordLocation.Content), new WordInfo("like", 7, 1, WordLocation.Content),
				new WordInfo("the", 15, 2, WordLocation.Content), new WordInfo("cookies", 22, 3, WordLocation.Content) };
			WordInfo[] expectedOutput = new WordInfo[] { new WordInfo("I", 0, 0, WordLocation.Content), new WordInfo("like", 7, 1, WordLocation.Content),
				new WordInfo("cookies", 22, 3, WordLocation.Content) };

			WordInfo[] output = Tools.RemoveStopWords(input, new string[] { "the", "in", "of" });

			Assert.Equal(expectedOutput.Length, output.Length);

			for(int i = 0; i < output.Length; i++) {
				Assert.Equal(expectedOutput[i].Text, output[i].Text);
				Assert.Equal(expectedOutput[i].FirstCharIndex, output[i].FirstCharIndex);
			}
		}
        
        [Fact]
        public void RemoveStopWords_InputWords_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>("words", () => Tools.RemoveStopWords(null, new string[0]));
        }

        [Fact]
        public void RemoveStopWords_StopWords_Null()
        {
            var ex = Assert.Throws<ArgumentNullException>("stopWords", () => Tools.RemoveStopWords(new WordInfo[0], null));
        }

    }

}
