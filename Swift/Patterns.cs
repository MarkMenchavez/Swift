namespace Swift
{
    internal static class Patterns
    {
        public const string AllCharactersBeforeFirstOpenBrace = @"\A[^{]*";

        public const string AllCharactersAfterLastCloseBrace = @"[^}]*\Z";

        public const string BlockIdentifier = @"{(?<BlockID>[1-5]|[S]):";

        public const string FieldIdentifier = @"\r\n:(?<FieldID>\d{2}[A-Z]?):";

        public const string SubBlock = @"{(?<ID>\S{1,3}):(?<Value>.*?)}";

        // Mandatory
        // Block ID - always 1: 
        // Application ID - F, A, or L (only F is supported)
        // Service ID - 2 digits
        // Logical Terminal Address - 12 characters
        // Session Number - 4 digits
        // Sequence Number - 6 digits
        public const string BasicHeaderBlock =
            @"{1:" +
            @"(?<ApplicationID>[F])" +
            @"(?<ServiceID>\d{2})" +
            @"(?<LogicalTerminalAddress>[^\r\n]{12})" +
            @"(?<SessionNumber>\d{4})" +
            @"(?<SequenceNumber>\d{6})}";

        // Optional
        // Block ID - always 2:
        // Indicator - I or O (only O is supported)
        // Message Type - 3 digits
        // Sender Input Time - 4 digits HHmm
        // Message Input Reference - 28 characters
        // Receiver Output Date - 6 digits yyMMdd
        // Receiver Output Time - 4 digits HHmm
        // Message Priority - S, N, or U
        public const string ApplicationHeaderBlock =
                @"{2:" +
                @"(?<Indicator>[O])" +
                @"(?<MessageType>\d{3})" +
                @"(?<SenderInputTime>(?:0[0-9]|1[0-9]|2[0-3])(?:[0-5][0-9]))" +
                @"(?<MessageInputReference>[^\r\n]{28})" +
                @"(?<ReceiverOutputDate>\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-9]|3[01]))" +
                @"(?<ReceiverOutputTime>(?:0[0-9]|1[0-9]|2[0-3])(?:[0-5][0-9]))" +
                @"(?<MessagePriority>[SNU])}";

        // 6!n4!n1!x4!n
        public const string DateTimeOffset =
                @"\A" +
                @"(?<LocalDate>\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-9]|3[01]))" +
                @"(?<LocalTime>(?:0[0-9]|1[0-9]|2[0-3])(?:[0-5][0-9]))" +
                @"(?<Sign>[+-])" +
                @"(?<Offset>(?:0[0-9]|1[0-3])(?:[0-5][0-9]))" +
                @"\z";

        // 5n[/5n]
        ////public const string StatementNumber = @"\A(?<Number>\d{1,5})(?:/(?<Sequence>\d{1,5}))?\z";

        // 3!a[1!a]15d
        ////public const string FloorLimitIndicator = @"\A(?<Currency>[A-Z]{3})(?<Mark>[DC]{1})?(?<Amount>[0-9,]{2,15})\z";

        // 5n3!a15d
        ////public const string NumberAndSumOfEntries = @"\A(?<Number>\d{1,5})(?<Currency>[A-Z]{3})(?<Amount>[0-9,]{2,15})\z";

        // Value Date 6!n yyMMdd
        // Entry Date [4!n] MMdd
        // Debit Credit Mark 2a C/D/EC/ED/RC/RD
        // Funds Code [1!a]
        // Amount 15d
        // TransactionType 1!a3!c = S3!n/N3!c/F3!c
        // Account Owner Reference 16x
        // Account Institution Reference [//16x]
        // Supplementary Details [34x]
        ////public const string StatementLine =
        ////    @"\A" +
        ////    @"(?<ValueDate>\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-9]|3[01]))" +
        ////    @"(?<EntryDate>(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-9]|3[01]))?" +
        ////    @"(?<Mark>(?:C)|(?:D)|(?:EC)|(?:ED)|(?:RC)|(?:RD))" +
        ////    @"(?<FundsCode>\S)?" +
        ////    @"(?<Amount>[0-9,]{2,15})" +
        ////    @"(?<TransactionType>(?:[S]\d{3})|(?:[NF]\S{3}))" +
        ////    @"(?<AccountOwnerReference>[^\r\n]{1,16})" +
        ////    @"(?:\/\/(?<AccountInstitutionReference>[^\r\n]{1,16}))?" +
        ////    @"(?:\s*(?<SupplementaryDetails>[^\r\n]{1,34}))?" +
        ////    @"\z";
    }
}
