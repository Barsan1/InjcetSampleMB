using Core;
using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.Text;
using System.Windows.Input;

namespace InjectSampleMudBlazor.Commands
{
    public class SampleComponentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            RawComponentDetails details = parameter as RawComponentDetails;
            string rawSample;

            // Current document open
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();

            // Check if there is document open
            if (docView?.TextView == null || !(bool)(docView?.FilePath.EndsWith(".razor")))
            {
                await VS.MessageBox.ShowErrorAsync($"{Constants.Extention_Name} Provide an Error", "Please open a razor component document to insert the sample");
                return; 
            }

            using (MISComponentsService cs = new MISComponentsService())
            {
                rawSample = await cs.LoadSampleComponentAsync(details.Area, details.SampleName);
            }

            MISDocumentTextService misTextService = new MISDocumentTextService();

            // Get offsets from current razor document view
            var sectionsData = await misTextService.TextSectionDivderAsync(docView.TextView.TextBuffer.CurrentSnapshot.GetText());


            await misTextService.OrginazeTextInputAsync(rawSample, sectionsData);
            
            // Inserts text at the caret (HTML)
            docView.TextBuffer?.Insert(docView.TextView.Caret.Position.BufferPosition, sectionsData.TextSections[1]);

            // Inserts text (C#)
            ITextSnapshotLine secondLine = docView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(sectionsData.CodeSection.SectionOffset + sectionsData.HtmlSection.LinesAddedToSection);
            docView.TextBuffer?.Insert(secondLine.Start, sectionsData.TextSections[2]);
        }
    }
}
