
namespace Core.Models
{
    public class MISTextOrginaize
    {
        /// <summary>
        /// usings section -> TextSections[0] <br/>
        /// Html section -> TextSections[1] <br/>
        /// C# section -> TextSections[2] <br/>
        /// </summary>
        public string[] TextSections { get; set; }

        public MISTextOffsets UsingsSection { get; set; } = new MISTextOffsets();
        public MISTextOffsets HtmlSection { get; set; } = new MISTextOffsets();
        public MISTextOffsets CodeSection { get; set; } = new MISTextOffsets();

        public MISExcludeTextOptions MISExcludeTextOptions { get; set; }

        /// <summary>
        /// Flag to check if there is any @code section in document before insert the new section
        /// </summary>
        public bool IsCodeSectionExist => CodeSection.SectionOffset != 0;

        /// <summary>
        /// Constrctur
        /// </summary>
        /// <param name="usingsSectionOffset">Using section is starting</param>
        /// <param name="htmlSectionOffset">Html section is starting</param>
        /// <param name="codeSectionOffset">Code section is starting</param>
        public MISTextOrginaize(int usingsSectionOffset, int htmlSectionOffset, int codeSectionOffset)
        {
            UsingsSection.SectionOffset = usingsSectionOffset;
            HtmlSection.SectionOffset = htmlSectionOffset;
            CodeSection.SectionOffset = codeSectionOffset;
        }
    }

}
