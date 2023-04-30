using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Globalization;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        SpeechRecognitionEngine recognizer;
        
        public NotesWindow()
        {
            InitializeComponent();

            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                 where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                 select r).FirstOrDefault();

            recognizer = new SpeechRecognitionEngine(currentCulture);

            //GrammarBuilder builder = new GrammarBuilder();
            //builder.AppendDictation();
            //Grammar grammar = new Grammar(builder);
            //recognizer.LoadGrammar(grammar);

            recognizer.LoadGrammar(new DictationGrammar());

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.SpeechRecognized += Recognizer_SpeechReconized;
        }

        private void Recognizer_SpeechReconized(object sender, SpeechRecognizedEventArgs e)
        {
            string reconizedText = e.Result.Text;
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(reconizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        bool isRecognizing = false;

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            // Speech Recognizer with .NET Core
            // Utiliza o pacote NuGet Microsoft.CognitiveServices.Speech

            //-----------------------
            //string region = "southcentralus";
            //string key = "ca2d92581df5497591263a1470030839";

            //var speechConfig = SpeechConfig.FromSubscription(key, region);

            //using(var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            //{
            //    using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            //    {
            //        var result = await recognizer.RecognizeOnceAsync();
            //        contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
            //    }
            //}


            // Speech Recognizer with .NET Framework
            // Utiliza a referencia System.Speech

            if (!isRecognizing)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                isRecognizing = true;
            }
            else
            {
                recognizer.RecognizeAsyncStop();
                isRecognizing = false;
            }

        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {ammountCharacters} characters.";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            //var textToBold = new TextRange(contentRichTextBox.Selection.Start, contentRichTextBox.Selection.End);

            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);

        }
    }
}
