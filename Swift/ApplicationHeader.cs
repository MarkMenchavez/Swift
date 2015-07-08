namespace Swift
{
    using System;

    public sealed class ApplicationHeader : MessageBlock
    {
        internal ApplicationHeader(string block)
            : base(block)
        {
            this.Parse();
        }

        public override string Id
        {
            get
            {
                return "2";
            }
        }

        public string Indicator { get; private set; }

        public string MessageType { get; private set; }

        public string SenderInputTime { get; private set; }

        public string MessageInputReference { get; private set; }

        public string ReceiverOutputDate { get; private set; }

        public string ReceiverOutputTime { get; private set; }

        public string MessagePriority { get; private set; }

        protected override string Name
        {
            get
            {
                return "Application Header Block";
            }
        }

        public override int GetHashCode()
        {
            return this.Block.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as ApplicationHeader;

            if (item == null)
            {
                return false;
            }

            return string.Equals(item.Block, this.Block, StringComparison.Ordinal);
        }

        private void Parse()
        {
            this.Validate();

            var match = Expressions.ApplicationHeader.Match(this.Block);

            if (!match.Success)
            {
                throw new ParserException(Invariant.Format("Invalid {0} {1}.", this.Name, this.Block));
            }

            this.Indicator = match.Groups["Indicator"].Value;
            this.MessageType = match.Groups["MessageType"].Value;
            this.SenderInputTime = match.Groups["SenderInputTime"].Value;
            this.MessageInputReference = match.Groups["MessageInputReference"].Value;
            this.ReceiverOutputDate = match.Groups["ReceiverOutputDate"].Value;
            this.ReceiverOutputTime = match.Groups["ReceiverOutputTime"].Value;
            this.MessagePriority = match.Groups["MessagePriority"].Value;

            if (Invariant.ToDateTime(this.ReceiverOutputDate, "yyMMdd", DateTime.MinValue) == DateTime.MinValue)
            {
                throw new ParserException(Invariant.Format("Invalid {0} {1}.", this.Name, this.Block));
            }
        }
    }
}
