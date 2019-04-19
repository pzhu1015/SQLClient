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
            private int line = 0;
            private int column = 0;
            private string foldText = string.Empty;

            public SQLFoldStart(int line, int column)
            {
                this.line = line;
                this.column = column;
            }

            public int Line
            {
                get
                {
                    return line;
                }
            }

            public int Column
            {
                get
                {
                    return column;
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
        }

        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            //stack 先进先出
            var buckets = new Stack<SQLFoldStart>();
            var caseEnd = new Stack<SQLFoldStart>();
            // Create foldmarkers for the whole document, enumerate through every line.
            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                // Get the text of current line.
                string text = document.GetText(document.GetLineSegment(i));
                for (int j = 0; j < text.Length; j++)
                {
                    if (text[j] == '(')
                    {
                        buckets.Push(new SQLFoldStart(i, j + 1));
                    }
                    else if (text.Substring(j).StartsWith("CASE", true, null))
                    {
                        caseEnd.Push(new SQLFoldStart(i, j + 5));
                    }
                    else if (text[j] == ')')
                    {
                        if (buckets.Count > 0)
                        {
                            SQLFoldStart fold = buckets.Pop();
                            list.Add(new FoldMarker(document, fold.Line, fold.Column, i, j, FoldType.TypeBody, "..."));
                        }
                    }
                    else if (text.Substring(j).StartsWith("END", true, null))
                    {
                        if (caseEnd.Count > 0)
                        {
                            SQLFoldStart fold = caseEnd.Pop();
                            list.Add(new FoldMarker(document, fold.Line, fold.Column, i, j, FoldType.TypeBody, "..."));
                        }
                    }
                }
                /*
                if (text.Trim().StartsWith("(")) // Look for method starts
                {
                    startLines.Push(i);
                }
                if (text.Trim().StartsWith(")")) // Look for method endings
                {
                    if (startLines.Count > 0)
                    {
                        int start = startLines.Pop();
                        list.Add(new FoldMarker(document, start, document.GetLineSegment(start).Length, i, 57, FoldType.TypeBody, "...)"));
                    }
                }
                */
            }

            return list;
        }

    }
}
