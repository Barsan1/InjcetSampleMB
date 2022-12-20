
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


        public MISTextOrginaize(int usingsSectionOffset, int htmlSectionOffset, int codeSectionOffset)
        {
            UsingsSection.SectionOffset = usingsSectionOffset;
            HtmlSection.SectionOffset = htmlSectionOffset;
            CodeSection.SectionOffset = codeSectionOffset;
        }
    }

}
