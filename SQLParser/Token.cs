using Helper;
using System.Collections.Generic;
using System.Text;

namespace SQLParser
{

    public class StatementObj
    {
        public StringBuilder Statement = new StringBuilder();
        public string FirstWord;
        public void Append(string str)
        {
            if (Statement.Length == 0)
            {
                FirstWord = str;
            }
            Statement.Append(str);
        }

        public void Clear()
        {
            Statement.Clear();
        }

        public bool Empty()
        {
            return Statement.Length == 0;
        }

        public string GetStatement()
        {
            return Statement.ToString();
        }

        public string GetFirstWord()
        {
            return FirstWord;
        }
    }

    public class Token
    {
        public bool IsDebug = false;
        public bool JobBegin;
        public string NameVal;
        public string CharVal;
        public string BlankVal;
        public string FinishVal;
        public string JobBeginVal;
        public StringBuilder StringVal = new StringBuilder();
        public StringBuilder SourceStringVal = new StringBuilder();
        public StringBuilder CommentVal = new StringBuilder();
        public StatementObj Statement = new StatementObj();
        public StatementObj SourceStatement = new StatementObj();
        public List<StatementObj> Statements = new List<StatementObj>();
        public List<StatementObj> SourceStatements = new List<StatementObj>();

        public void ReSet()
        {
            JobBegin = false;
            NameVal = "";
            CharVal = "";
            BlankVal = "";
            FinishVal = "";
            JobBeginVal = "";
            StringVal.Clear();
            SourceStringVal.Clear();
            CommentVal.Clear();
            Statement = new StatementObj();
            SourceStatement = new StatementObj();
        }

        public void Clear()
        {
            ReSet();
            Statements.Clear();
            SourceStatements.Clear();
        }

        public bool IsComplete()
        {
            if (IsDebug)
            {
                if (!Statement.Empty() || CommentVal.Length != 0)
                {
                    LogHelper.Info($"Not Complete: {Statement.ToString()} {CommentVal.ToString()}");
                }
            }
            return Statement.Empty() && CommentVal.Length == 0;
        }

        public void AppendToken(string str, string source_str)
        {
            Statement.Append(str);
            SourceStatement.Append(source_str);
        }

        public void AppendString(string str, string source_str, bool finish = false)
        {
            StringVal.Append(str);
            SourceStringVal.Append(source_str);
            if (IsDebug)
            {
                if (finish)
                {
                    LogHelper.Info($"Token string: {StringVal.ToString()}");
                    LogHelper.Info($"Token source string: {SourceStringVal.ToString()}");
                }
            }
            AppendToken(str, source_str);
            if (finish)
            {
                StringVal.Clear();
                SourceStringVal.Clear();
            }
        }

        public void SetBlank(string str)
        {
            if (IsDebug)
            {
                LogHelper.Info($"Token Blank: {str}");
            }
            BlankVal = str;
            if (!Statement.Empty())
            {
                AppendToken(str, str);
            }
        }

        public void SetJobBegin(string str)
        {
            if (IsDebug)
            {
                LogHelper.Info($"Token job begin: {str}");
            }
            JobBeginVal = str;
            AppendToken(str, str);
            JobBegin = true;
        }

        public void SetName(string str)
        {
            if (IsDebug)
            {
                LogHelper.Error($"Token name: {str}");
            }
            NameVal = str;
            AppendToken(str, str);
        }

        public void AppendComment(string str, bool finish = false)
        {
            CommentVal.Append(str);
            if (IsDebug)
            {
                if (finish)
                {
                    LogHelper.Info($"Token comment: {CommentVal.ToString()}");
                }
            }
            if (finish)
            {
                CommentVal.Clear();
            }
        }

        public void SetChar(string str)
        {
            if (IsDebug)
            {
                LogHelper.Info($"Token char: {str}");
            }
            CharVal = str;
            AppendToken(str, str);
        }

        public void SetFinish(string str)
        {
            if (IsDebug)
            {
                LogHelper.Info($"Token finish: {str}");
            }
            FinishVal = str;
            AppendToken(str, str);
            if (!JobBegin)
            {
                if (IsDebug)
                {
                    LogHelper.Info($"statement: {Statement.GetStatement()}");
                    LogHelper.Info($"source statement: {SourceStatement.GetStatement()}");
                }
                Statements.Add(Statement);
                SourceStatements.Add(SourceStatement);
                Statement = new StatementObj();
                SourceStatement = new StatementObj();
            }
            else
            {
                string name = NameVal;
                if (name.ToUpper() == "END")
                {
                    if (IsDebug)
                    {
                        LogHelper.Info($"statement: {Statement.GetStatement()}");
                        LogHelper.Info($"source statement: {SourceStatement.GetStatement()}");
                    }
                    Statements.Add(Statement);
                    SourceStatements.Add(SourceStatement);
                    Statement = new StatementObj();
                    SourceStatement = new StatementObj();
                    JobBegin = false;
                }
            }
        }
    }
}
