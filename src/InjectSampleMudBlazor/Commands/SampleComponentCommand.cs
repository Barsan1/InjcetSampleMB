using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.Text;
using System.Windows.Input;
using System.Diagnostics;

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
                await VS.MessageBox.ShowErrorAsync($"{Core.Constants.Extention_Name} Provide an Error", "Please open a razor component document to insert the sample");
                return;
            }

            // Load component from github
            using (MISComponentsService cs = new MISComponentsService())
            {
                rawSample = await cs.LoadSampleComponentAsync(details.Area, details.SampleName);
            }

            MISDocumentTextService misTextService = new MISDocumentTextService();

            // Get offsets and text sections from current razor document view (Opened in visual studio)
            var sectionsData = await misTextService.TextSectionDivderAsync(docView.TextView.TextBuffer.CurrentSnapshot.GetText());

            MISExcludeTextOptions options = new MISExcludeTextOptions(sectionsData.IsCodeSectionExist);
            sectionsData.MISExcludeTextOptions = options;

            await misTextService.OrginazeTextInputAsync(rawSample, sectionsData);

            // Inserts text at the caret (HTML)
            docView.TextBuffer?.Insert(docView.TextView.Caret.Position.BufferPosition, sectionsData.TextSections[1]);

            // Debug
            Debug.WriteLine(docView.TextView.TextBuffer.CurrentSnapshot.GetText());

            // Check if need to add usings in razor component
            if (sectionsData.UsingsSection.LinesAddedToSection > 1)
                docView.TextBuffer?.Insert(0, sectionsData.TextSections[0]);

            // Inserts text (C#)
            if (sectionsData.CodeSection.LinesAddedToSection != 0)
            {
                ITextSnapshotLine secondLine;
                // TODO: Create logic method to orginaze the c sharp code section (Own method)
                if (sectionsData.IsCodeSectionExist)
                {
                    await misTextService.CodeSectionOrginazeAsync(sectionsData);
                    
                    // Orginal place + html lines added + usings lines added - 1 + 2 (Actual comments)
                    int calculatelineToAdd = sectionsData.CodeSection.SectionOffset + sectionsData.HtmlSection.LinesAddedToSection + sectionsData.UsingsSection.LinesAddedToSection + 2;
                    secondLine = docView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(calculatelineToAdd);
                }
                else
                {
                    secondLine = docView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(docView.TextBuffer.CurrentSnapshot.LineCount);
                }

                docView.TextBuffer?.Insert(secondLine.Start, sectionsData.TextSections[2]);
            }
        }
    }
}
