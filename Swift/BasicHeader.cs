namespace Swift
{
    using System;

    public sealed class BasicHeader : MessageBlock
    {
        internal BasicHeader(string block)
            : base(block)
        {
            this.Parse();
        }

        public override string Id
        {
            get
            {
                return "1";
            }
        }

        public string ApplicationId { get; private set; }

        public string ServiceId { get; private set; }

        public string LogicalTerminalAddress { get; private set; }

        public string SessionNumber { get; private set; }

        public string SequenceNumber { get; private set; }

        protected override string Name
        {
            get
            {
                return "Basic Header Block";
            }
        }

        public override int GetHashCode()
        {
            return this.Block.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as BasicHeader;

            if (item == null)
            {
                return false;
            }

            return string.Equals(item.Block, this.Block, StringComparison.Ordinal);
        }

        private void Parse()
        {
            this.Validate();

            var match = Expressions.BasicHeader.Match(this.Block);

            if (!match.Success)
            {
                throw new ParserException(Invariant.Format("Invalid {0} {1}.", this.Name, this.Block));
            }

            this.ApplicationId = match.Groups["ApplicationID"].Value;
            this.ServiceId = match.Groups["ServiceID"].Value;
            this.LogicalTerminalAddress = match.Groups["LogicalTerminalAddress"].Value;
            this.SessionNumber = match.Groups["SessionNumber"].Value;
            this.SequenceNumber = match.Groups["SequenceNumber"].Value;
        }
    }
}
