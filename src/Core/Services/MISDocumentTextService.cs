using Core.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Services
{
    public class MISDocumentTextService 
    {
        private string[] sectionBreaks = new string[2] { "<", "@code" };
        private StringBuilder _stringBuilder = new StringBuilder();
        
        public async Task OrginazeTextInputAsync(string text, MISTextOrginaize sectionOrginaize)
        {
            _stringBuilder.Clear();
            var sections = GenerateSectionsForDocument();

            // Read string line by line
            StringReader reader = new StringReader(text);

            // Keep track of current section
            int currentSection = 0;
            int sectionsLinesAdded = 0;
            
            // hold current line
            string line;

            while (!((line = await reader.ReadLineAsync()) is null))
            {
                if (line.Equals("") || SkipLines(currentSection, line, sectionOrginaize.MISExcludeTextOptions))
                    continue;
                
                // Html section
                if (currentSection != 1 && line.StartsWith(sectionBreaks[0]))
                {
                    sectionOrginaize.UsingsSection.LinesAddedToSection = ++sectionsLinesAdded;
                    SectionUpdated(ref sections, ref currentSection);
                    sectionsLinesAdded = 0;
                }

                // C# section
                if (currentSection != 2 && line.StartsWith(sectionBreaks[1]))
                {
                    sectionOrginaize.HtmlSection.LinesAddedToSection = ++sectionsLinesAdded;
                    SectionUpdated(ref sections, ref currentSection);
                    sectionsLinesAdded = 0;
                }

                sectionsLinesAdded++;
                _stringBuilder.AppendLine(line);
            }

            if (currentSection == 2)
            {
                // Added c# code to code section
                sections[2] += _stringBuilder.ToString();
                sectionOrginaize.CodeSection.LinesAddedToSection = ++sectionsLinesAdded;
            }
            else
                SectionUpdated(ref sections, ref currentSection);
            
            
            // Dispose reader
            reader.Dispose();

            sectionOrginaize.TextSections = sections;
        }


        public async Task CodeSectionOrginazeAsync(MISTextOrginaize sectionOrginaize)
        {
            _stringBuilder.Clear();

            StringReader reader = new StringReader(sectionOrginaize.TextSections[2]);
            int lineNumber = 0;
            string line;

            while (true) 
            {
                line = await reader.ReadLineAsync();
                lineNumber++;

                if ((sectionOrginaize.IsCodeSectionExist && lineNumber == 3))
                {
                    if (sectionOrginaize.MISExcludeTextOptions.IsAddSampleDataSection)
                        _stringBuilder.Append(sectionOrginaize.MISExcludeTextOptions.AddSampleDataSection());
                    
                    continue;
                }

                if (line is null)
                    break;
                

                _stringBuilder.AppendLine(line);
            }

            sectionOrginaize.TextSections[2] = _stringBuilder.ToString();
            sectionOrginaize.TextSections[2] = sectionOrginaize.TextSections[2].Substring(0, sectionOrginaize.TextSections[2].LastIndexOf("}"));

            reader.Dispose();
        }


        /// <summary>
        /// Get current document text in visual studio and divade to sections and addetion data to perform the injection <br/>
        /// Using (CSharp + Blazor section) <br/>
        /// Html (Razor section) <br/>
        /// Code (CSharp section) <br/>
        /// </summary>
        /// <param name="text">Current text in the document</param>
        /// <returns>New <see cref="MISTextOrginaize"/> object</returns>
        public async Task<MISTextOrginaize> TextSectionDivderAsync(string text)
        {
            // Read string line by line
            StringReader reader = new StringReader(text);

            // Keep track of current section and offsets
            int currentSection = 0;
            int[] offsets = new int[2];
            int currentLineNum = 0;

            // hold current line
            string line;

            while (!((line = await reader.ReadLineAsync()) is null))
            {
                currentLineNum++;

                // Html section
                if (currentSection != 1 && offsets[0] == 0 && line.StartsWith(sectionBreaks[0]))
                    offsets[0] = currentLineNum;

                // C# section
                if (currentSection != 2 && line.StartsWith(sectionBreaks[1]))
                    offsets[1] = currentLineNum;
            }

            // Dispose reader
            reader.Dispose();

            return new MISTextOrginaize(0, offsets[0], offsets[1]);
        }

        private bool SkipLines(int section, string line, MISExcludeTextOptions opt)
        {
            if (section == 0)
                for (int i = 0; i < opt.Using_Exclude.Length; i++)
                    if (line.Equals(opt.Using_Exclude[i]))
                        return true;

            if (section == 2 && !opt.IsAddSampleDataSection && line.Contains("Element"))
                opt.IsAddSampleDataSection = true;

            return false;
        }

        private void SectionUpdated(ref string[] sections, ref int currentSection)
        {
            sections[currentSection++] += _stringBuilder.ToString();
            _stringBuilder.Clear();
        }


        private string[] GenerateSectionsForDocument()
            =>  new string[3]
            {
                "",
                $"\n<!--{Constants.AutoGenerated_Comment}-->\n",
                $"\n\t/*{Constants.AutoGenerated_Comment}*/\n"
            };
    }
}
