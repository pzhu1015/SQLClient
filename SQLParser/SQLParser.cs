using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQLParser
{
    public enum ParserMode
    {
        eNormal = 0,
        eDoubleQuote = 1,
        eSingleQuote = 2,
        eComment = 3
    }

    public class SQLParser
    {
        public bool IsDebug = false;
        public ParserMode Mode = ParserMode.eNormal;
        public Token Token = new Token();
        public Regex RegJobBegin = new Regex("(create|alter)[ \f\r\t\v]+(job)",RegexOptions.IgnoreCase);
        public Regex RegQuote = new Regex("(\")|(\')");
        public Regex RegName = new Regex("[_a-zA-Z0-9][_a-zA-Z0-9]*|`(\\.|``|[^`\n])*`");
        public Regex RegBlank = new Regex("[ \f\r\t\v\n]+");
        public Regex RegFinish = new Regex("(;|；)");
        public Regex RegEscape = new Regex("\\\\([a-zA-Z0-9]|[\xf0-\xf7][\x80-\xbf]{3}|[\xe0-\xef][\x80-\xbf]{2}|[\xc0-\xdf][\x80-\xbf])|(\\\\\")|(\\\\\')|(\\\\)");

        public bool Parse(string sql)
        {
            Token.ReSet();
            int i = 0;
            Match m;
            while(i < sql.Length)
            {
                #region create job or alter job
                if (sql[i] == 'c' || sql[i] == 'C' || sql[i] == 'a' || sql[i] == 'A')
                {
                    m = RegJobBegin.Match(sql, i);
                    if (m.Success && m.Index == i)
                    {
                        Token.SetJobBegin(m.Value);
                        i += m.Length;
                        continue;
                    }
                }
                #endregion

                #region outside escape char
                if (sql[i] == '\\')
                {
                    int idx = i;
                    idx++;
                    if (idx != sql.Length &&(sql[idx] == '"' || sql[idx] == '\''))
                    {
                        idx++;
                        string str = sql.Substring(i, (idx - i));
                        Token.SetChar(str);
                        i = idx;
                        continue;
                    }
                }
                #endregion

                #region string
                m = RegQuote.Match(sql, i, 1);
                if (m.Success)
                {
                    #region string first quote
                    Token.AppendString(m.Value, m.Value);
                    i += m.Length;
                    if (m.Value == "\"")
                    {
                        Mode = ParserMode.eDoubleQuote;
                        if (IsDebug)
                        {
                            LogHelper.Info("Double quote mode start");
                        }
                    }
                    else if (m.Value == "\'")
                    {
                        Mode = ParserMode.eSingleQuote;
                        if (IsDebug)
                        {
                            LogHelper.Info("Single quote mode start");
                        }
                    }
                    else
                    {
                        return false;
                    }
                    #endregion

                    #region string contents
                    while (i != sql.Length)
                    {
                        #region string escape
                        int length = 5;
                        if (i + length > sql.Length) length = sql.Length - i;
                        m = RegEscape.Match(sql, i, length);
                        if (m.Success)
                        {
                            i += m.Value.Length;
                            char c = m.Value[m.Length - 1];
                            if (c == '\\')
                            {
                                Token.AppendString(m.Value, "\\\\");
                                i++;
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash: {m.Value}");
                                }
                            }
                            else if (c == '"')
                            {
                                if (Mode == ParserMode.eDoubleQuote)
                                {
                                    Token.AppendString(m.Value, m.Value);
                                }
                                else
                                {
                                    Token.AppendString("\"", m.Value);
                                }
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash double quote: {c}");
                                }
                            }
                            else if (c == '\'')
                            {
                                if (Mode == ParserMode.eSingleQuote)
                                {
                                    Token.AppendString(m.Value, m.Value);
                                }
                                else
                                {
                                    Token.AppendString("\'", m.Value);
                                }
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match bach slash single quote: {c}");
                                }
                            }
                            else if (c == 't')
                            {
                                Token.AppendString("\t", m.Value);
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash t: {c}");
                                }
                            }
                            else if (c == 'b')
                            {
                                Token.AppendString("\b", m.Value);
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash b: {c}");
                                }
                            }
                            else if (c == 'n')
                            {
                                Token.AppendString("\n", m.Value);
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash n: {c}");
                                }
                            }
                            else if (c == 'r')
                            {
                                Token.AppendString("\r", m.Value);
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match back slash r: {c}");
                                }
                            }
                            else
                            {
                                if ((c & 0x80) != 0)
                                {
                                    string chinese_str = m.Value.Substring(1, m.Length - 1);
                                    Token.AppendString(chinese_str, m.Value);
                                    if (IsDebug)
                                    {
                                        LogHelper.Info($"Match chinese char: {m.Value}");
                                    }
                                }
                                else
                                {
                                    string str = m.Value.Substring(1, m.Length - 1);
                                    Token.AppendString(str, str);
                                    if (IsDebug)
                                    {
                                        LogHelper.Info($"Match normal char: {str}");
                                    }
                                }
                            }
                            continue;
                        }
                        #endregion

                        #region string last quote
                        m = RegQuote.Match(sql, i, 1);
                        if (m.Success)
                        {
                            i += m.Length;
                            if (Mode == ParserMode.eDoubleQuote && m.Value == "\"")
                            {
                                Mode = ParserMode.eNormal;
                                Token.AppendString(m.Value, m.Value, true);
                                if (IsDebug)
                                {
                                    LogHelper.Info("Double quote mode finish");
                                }
                                break;
                            }
                            else if (Mode == ParserMode.eSingleQuote && m.Value == "\'")
                            {
                                Mode = ParserMode.eNormal;
                                Token.AppendString(m.Value, m.Value, true);
                                if (IsDebug)
                                {
                                    LogHelper.Info("Single quote mode finish");
                                }
                                break;
                            }
                            else
                            {
                                Token.AppendString(m.Value, m.Value);
                                if (IsDebug)
                                {
                                    LogHelper.Info($"Match normal quote: {m.Value}");
                                }
                                continue;
                            }
                        }
                        #endregion

                        #region any char
                        string any_str = sql[i].ToString();
                        Token.AppendString(any_str, any_str);
                        i++;
                        if (IsDebug)
                        {
                            LogHelper.Info($"Match any: {any_str}");
                        }
                        #endregion
                    }
                    #endregion

                }
                #endregion

                #region blank
                m = RegBlank.Match(sql, i);
                if (m.Success && m.Index == i)
                {
                    Token.SetBlank(" ");
                    i += m.Length;
                    continue;
                }
                #endregion

                #region name
                m = RegName.Match(sql, i);
                if (m.Success && m.Index == i)
                {
                    Token.SetName(m.Value);
                    i += m.Length;
                    continue;
                }
                #endregion

                #region finish
                m = RegFinish.Match(sql, i, 1);
                if (m.Success)
                {
                    Token.SetFinish(";");
                    i++;
                    continue;
                }
                #endregion

                #region comment (#, --, //, /* */)

                #region comment #
                if (sql[i] == '#')
                {
                    if (IsDebug)
                    {
                        LogHelper.Info("Comment (#) start");
                    }
                    Token.AppendComment("#");
                    i++;
                    Mode = ParserMode.eComment;
                    while(i != sql.Length)
                    {
                        if (sql[i] == '\n')
                        {
                            i++;
                            break;
                        }
                        Token.AppendComment(sql[i].ToString());
                        i++;
                    }
                    Token.AppendComment("", true);
                    Mode = ParserMode.eNormal;
                    if (IsDebug)
                    {
                        LogHelper.Info("Comment (#) end");
                    }
                    continue;
                }
                #endregion

                #region comment --
                if (sql[i] == '-')
                {
                    int idx = i;
                    idx++;
                    if (idx != sql.Length && sql[idx] == '-')
                    {
                        if (IsDebug)
                        {
                            LogHelper.Info("Comment (--) start");
                        }
                        Token.AppendComment("--");
                        i = idx;
                        i++;
                        Mode = ParserMode.eComment;
                        while(i != sql.Length)
                        {
                            if (sql[i] == '\n')
                            {
                                i++;
                                break;
                            }
                            Token.AppendComment(sql[i].ToString());
                            i++;
                        }
                        Token.AppendComment("", true);
                        Mode = ParserMode.eNormal;
                        if (IsDebug)
                        {
                            LogHelper.Info("Comment (--) end");
                        }
                        continue;
                    }
                }

                #endregion

                #region comment // or /* */
                if (sql[i] == '/')
                {
                    int idx = i;
                    idx++;
                    if (idx != sql.Length)
                    {
                        #region comment //
                        if (sql[idx] == '/')
                        {
                            if (IsDebug)
                            {
                                LogHelper.Info("Comment (//) start");
                            }
                            Token.AppendComment("//");
                            i = idx;
                            i++;
                            Mode = ParserMode.eComment;
                            while(i != sql.Length)
                            {
                                if (sql[i] == '\n')
                                {
                                    i++;
                                    break;
                                }
                                Token.AppendComment(sql[i].ToString());
                                i++;
                            }
                            Token.AppendComment("", true);
                            Mode = ParserMode.eNormal;
                            if (IsDebug)
                            {
                                LogHelper.Info("Comment (//) end");
                            }
                            continue;
                        }
                        #endregion

                        #region comment /* */
                        if (sql[idx] == '*')
                        {
                            if (IsDebug)
                            {
                                LogHelper.Info("Comment (/*) start");
                            }
                            Token.AppendComment("/*");
                            i = idx;
                            i++;
                            Mode = ParserMode.eComment;
                            while(i != sql.Length)
                            {
                                if (sql[i] == '*')
                                {
                                    idx = i;
                                    idx++;
                                    if (idx != sql.Length && sql[idx] == '/')
                                    {
                                        i = idx;
                                        Mode = ParserMode.eNormal;
                                        Token.AppendComment("*/", true);
                                        if (IsDebug)
                                        {
                                            LogHelper.Info("Comment (*/) end");
                                        }
                                        break;
                                    }
                                }
                                Token.AppendComment(sql[i].ToString());
                                i++;
                            }
                            continue;
                        }
                        #endregion
                    }
                }
                #endregion

                #endregion

                Token.SetChar(sql[i].ToString());
                i++;

            }
            return true;
        }

        public string GetRemain()
        {
            string str = Token.SourceStatement.ToString();
            str += Token.CommentVal.ToString();
            return str;
        }

        public bool IsComplete()
        {
            return Token.IsComplete();
        }

        public void SetDebug(bool debug)
        {
            IsDebug = debug;
            Token.IsDebug = debug;
        }

        public void Clear()
        {
            Token.Clear();
        }
        
        public bool IsSource(int idx)
        {
            string stmt = Token.Statements[idx].GetStatement();
            string source = stmt.Substring(0, 6);
            return source.ToUpper() == "SOURCE";
        }

        public List<StatementObj> GetStatements()
        {
            return Token.Statements;
        }

        public List<StatementObj> GetSourceStatements()
        {
            return Token.SourceStatements;
        }
    }
}
