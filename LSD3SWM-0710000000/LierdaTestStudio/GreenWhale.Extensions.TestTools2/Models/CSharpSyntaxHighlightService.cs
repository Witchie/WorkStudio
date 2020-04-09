using DevExpress.CodeParser;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace GreenWhale.Extensions.TestTools2.Models
{
    /// <summary>
    ///  This class implements the Execute method of the ISyntaxHighlightService interface to parse and colorize the text.
    /// </summary>
    public class CSharpSyntaxHighlightService : ISyntaxHighlightService
    {
        readonly RichEditControl syntaxEditor;
        SyntaxColors syntaxColors;
        SyntaxHighlightProperties commentProperties;
        SyntaxHighlightProperties keywordProperties;
        SyntaxHighlightProperties stringProperties;
        SyntaxHighlightProperties xmlCommentProperties;
        SyntaxHighlightProperties textProperties;
        SyntaxHighlightProperties numberProperties;

        public CSharpSyntaxHighlightService(RichEditControl syntaxEditor)
        {
            this.syntaxEditor = syntaxEditor;
            syntaxColors = new SyntaxColors();
        }

        void HighlightSyntax(TokenCollection tokens)
        {
            commentProperties = new SyntaxHighlightProperties();
            commentProperties.ForeColor = syntaxColors.CommentColor;

            keywordProperties = new SyntaxHighlightProperties();
            keywordProperties.ForeColor = syntaxColors.KeywordColor;

            stringProperties = new SyntaxHighlightProperties();
            stringProperties.ForeColor = syntaxColors.StringColor;

            xmlCommentProperties = new SyntaxHighlightProperties();
            xmlCommentProperties.ForeColor = syntaxColors.XmlCommentColor;

            textProperties = new SyntaxHighlightProperties();
            textProperties.ForeColor = syntaxColors.TextColor;
            numberProperties = new SyntaxHighlightProperties();
            numberProperties.ForeColor = Color.DarkGreen;
            if (tokens == null || tokens.Count == 0)
                return;

            Document document = syntaxEditor.Document;
            //CharacterProperties cp = document.BeginUpdateCharacters(0, 1);
            List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
            foreach (Token token in tokens)
            {
                HighlightCategorizedToken((CategorizedToken)token, syntaxTokens);
            }
            document.ApplySyntaxHighlight(syntaxTokens);
            //document.EndUpdateCharacters(cp);
        }
        void HighlightCategorizedToken(CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens)
        {
            TokenCategory category = token.Category;
            if (category == TokenCategory.Comment)
            {
                syntaxTokens.Add(SetTokenColor(token, commentProperties));
            }
            else if (category == TokenCategory.Keyword)
            {
                syntaxTokens.Add(SetTokenColor(token, keywordProperties));
            }
            else if (category == TokenCategory.String)
            {
                syntaxTokens.Add(SetTokenColor(token, stringProperties));
            }
            else if (category == TokenCategory.XmlComment)
            {
                syntaxTokens.Add(SetTokenColor(token, xmlCommentProperties));
            }
            else if (category==TokenCategory.Number)
            {
                syntaxTokens.Add(SetTokenColor(token, numberProperties));
            }
            else
            {
                syntaxTokens.Add(SetTokenColor(token, textProperties));
            }
        }
        SyntaxHighlightToken SetTokenColor(Token token, SyntaxHighlightProperties foreColor)
        {
            if (syntaxEditor.Document.Paragraphs.Count < token.Range.Start.Line)
                return null;
            int paragraphStart = DocumentHelper.GetParagraphStart(syntaxEditor.Document.Paragraphs[token.Range.Start.Line - 1]);
            int tokenStart = paragraphStart + token.Range.Start.Offset - 1;
            if (token.Range.End.Line != token.Range.Start.Line)
                paragraphStart = DocumentHelper.GetParagraphStart(syntaxEditor.Document.Paragraphs[token.Range.End.Line - 1]);

            int tokenEnd = paragraphStart + token.Range.End.Offset - 1;
            Debug.Assert(tokenEnd > tokenStart);
            return new SyntaxHighlightToken(tokenStart, tokenEnd - tokenStart, foreColor);
        }

        #region #ISyntaxHighlightServiceMembers
        public void Execute()
        {
            string newText = syntaxEditor.Text;
            // Determine language by file extension.
            string ext = System.IO.Path.GetExtension(syntaxEditor.Options.DocumentSaveOptions.CurrentFileName);
            ParserLanguageID lang_ID = ParserLanguage.FromFileExtension(ext);
            // Do not parse HTML or XML.
            if (lang_ID == ParserLanguageID.Html ||
                lang_ID == ParserLanguageID.Xml ||
                lang_ID == ParserLanguageID.None) return;
            // Use DevExpress.CodeParser to parse text into tokens.
            ITokenCategoryHelper tokenHelper = TokenCategoryHelperFactory.CreateHelper(lang_ID);
            TokenCollection highlightTokens;
            highlightTokens = tokenHelper.GetTokens(newText);
            HighlightSyntax(highlightTokens);
        }

        public void ForceExecute()
        {
            Execute();
        }
        #endregion #ISyntaxHighlightServiceMembers
    }
}
