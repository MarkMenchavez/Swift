namespace Swift
{
    using System.Text.RegularExpressions;

    internal static class Expressions
    {
        public static readonly Regex Block = new Regex(Patterns.BlockIdentifier, RegexOptions.Singleline);

        public static readonly Regex SubBlock = new Regex(Patterns.SubBlock, RegexOptions.Singleline);

        public static readonly Regex Field = new Regex(Patterns.FieldIdentifier, RegexOptions.Singleline);

        public static readonly Regex BasicHeader = new Regex(Patterns.BasicHeaderBlock, RegexOptions.Singleline);

        public static readonly Regex ApplicationHeader = new Regex(Patterns.ApplicationHeaderBlock, RegexOptions.Singleline);

        ////public static readonly Regex OpenBrace = new Regex(Patterns.AllCharactersBeforeFirstOpenBrace, RegexOptions.Singleline);

        public static readonly Regex CloseBrace = new Regex(Patterns.AllCharactersAfterLastCloseBrace, RegexOptions.Singleline);

        ////public static readonly Regex DateTimeOffset = new Regex(Patterns.DateTimeOffset, RegexOptions.None);

        ////public static readonly Regex StatementNumber = new Regex(Patterns.StatementNumber, RegexOptions.None);

        ////public static readonly Regex FloorLimitIndicator = new Regex(Patterns.FloorLimitIndicator, RegexOptions.None);

        ////public static readonly Regex NumberAndSumOfEntries = new Regex(Patterns.NumberAndSumOfEntries, RegexOptions.None);

        ////public static readonly Regex StatementLine = new Regex(Patterns.StatementLine, RegexOptions.Singleline);
    }
}
