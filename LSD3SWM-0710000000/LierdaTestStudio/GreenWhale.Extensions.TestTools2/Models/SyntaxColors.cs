using System.Drawing;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    ///  This class provides colors to highlight the tokens.
    /// </summary>
    public class SyntaxColors
    {
        static Color DefaultCommentColor { get { return Color.Green; } }
        static Color DefaultKeywordColor { get { return Color.Blue; } }
        static Color DefaultStringColor { get { return Color.Brown; } }
        static Color DefaultXmlCommentColor { get { return Color.Gray; } }
        static Color DefaultTextColor { get { return Color.Black; } }

        public Color CommentColor { get { return DefaultCommentColor; } }
        public Color KeywordColor { get { return  DefaultKeywordColor; } }
        public Color TextColor { get { return  DefaultTextColor; } }
        public Color XmlCommentColor { get { return  DefaultXmlCommentColor; } }
        public Color StringColor { get { return DefaultStringColor; } }
    }
}
