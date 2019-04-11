using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLUserControl
{
    public class SQLFolding : IFoldingStrategy
    {
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            //stack 先进先出
            var startLines = new Stack<int>();
            // Create foldmarkers for the whole document, enumerate through every line.
            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                // Get the text of current line.
                string text = document.GetText(document.GetLineSegment(i));
                //支持嵌套 {}
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
            }

            return list;
        }

    }
}
