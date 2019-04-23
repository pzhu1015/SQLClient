using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLUserControl
{
    public class SQLFolding : IFoldingStrategy
    {
        public class SQLFoldStart
        {
            private int startLine = 0;
            private int startColumn = 0;
            private string foldText;

            public int StartLine
            {
                get
                {
                    return startLine;
                }

                set
                {
                    startLine = value;
                }
            }

            public int StartColumn
            {
                get
                {
                    return startColumn;
                }

                set
                {
                    startColumn = value;
                }
            }

            public string FoldText
            {
                get
                {
                    return foldText;
                }

                set
                {
                    foldText = value;
                }
            }

            public SQLFoldStart(int startLine, int startColumn, string text, string foldText)
            {
                this.startLine = startLine;
                this.startColumn = startColumn + text.Length;
                this.foldText = foldText;
            }
        }

        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            var buckets = new Stack<SQLFoldStart>();
            var caseEnd = new Stack<SQLFoldStart>();
            var comments = new Stack<SQLFoldStart>();
            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                string text = document.GetText(document.GetLineSegment(i));
                for (int j = 0; j < text.Length; j++)
                {
                    if (text[j] == '(')
                    {
                        buckets.Push(new SQLFoldStart(i, j, "(", "..."));
                    }
                    else if (text[j] == ')')
                    {
                        if (buckets.Count > 0)
                        {
                            SQLFoldStart fold = buckets.Pop();
                            list.Add(new FoldMarker(document, fold.StartLine, fold.StartColumn, i, j, FoldType.TypeBody, fold.FoldText));
                        }
                    }
                    else if (text.Substring(j).StartsWith("CASE", true, null))
                    {
                        caseEnd.Push(new SQLFoldStart(i, j, "CASE", "..."));
                    }
                    else if (text.Substring(j).StartsWith("END", true, null))
                    {
                        if (caseEnd.Count > 0)
                        {
                            SQLFoldStart fold = caseEnd.Pop();
                            list.Add(new FoldMarker(document, fold.StartLine, fold.StartColumn, i, j, FoldType.TypeBody, fold.FoldText));
                        }
                    }
                    else if (text.Substring(j).StartsWith("/*", true, null))
                    {
                        caseEnd.Push(new SQLFoldStart(i, j, "/*", "..."));
                    }
                    else if (text.Substring(j).StartsWith("*/", true, null))
                    {
                        if (caseEnd.Count > 0)
                        {
                            SQLFoldStart fold = caseEnd.Pop();
                            list.Add(new FoldMarker(document, fold.StartLine, fold.StartColumn, i, j, FoldType.TypeBody, fold.FoldText));
                        }
                    }
                }
            }

            return list;
        }

    }
}
