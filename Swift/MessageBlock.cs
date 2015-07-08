namespace Swift
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public abstract class MessageBlock
    {
        private readonly string block;

        protected MessageBlock(string block)
        {
            this.block = block ?? string.Empty;
        }

        public abstract string Id { get; }

        internal virtual string Content
        {
            get
            {
                var prefix = string.Concat(this.StartCharacter, this.Id, this.BlockDelimiter);
                var suffix = this.EndCharacter;

                return this.Block.Length > prefix.Length + suffix.Length
                    ? this.Block.Substring(prefix.Length, this.Block.Length - prefix.Length - suffix.Length)
                    : string.Empty;
            }
        }

        protected abstract string Name { get; }

        protected string Block
        {
            get
            {
                return this.block;
            }
        }

        protected virtual string StartCharacter
        {
            get
            {
                return "{";
            }
        }

        protected virtual string EndCharacter
        {
            get
            {
                return "}";
            }
        }

        protected virtual string BlockDelimiter
        {
            get
            {
                return ":";
            }
        }

        public override string ToString()
        {
            return this.block;
        }

        public abstract override int GetHashCode();

        public abstract override bool Equals(object obj);

        protected void Validate()
        {
            if (!this.Block.StartsWith(this.StartCharacter + this.Id + this.BlockDelimiter, StringComparison.Ordinal) ||
                !this.Block.EndsWith(this.EndCharacter, StringComparison.Ordinal))
            {
                throw new ParserException(
                    Invariant.Format(
                    "{0} {1} must start with {2}{3}{4} and end with {5}.",
                    this.Name,
                    this.Block,
                    this.StartCharacter,
                    this.Id,
                    this.BlockDelimiter,
                    this.EndCharacter));
            }

            if (string.IsNullOrWhiteSpace(this.Content))
            {
                throw new ParserException(Invariant.Format("{0} {1} has no content.", this.Name, this.Block));
            }
        }

        protected ICollection<SubBlock> ParseSubBlocks()
        {
            var matches = Expressions.SubBlock.Matches(this.Content);
            var list = (from Match match in matches
                        let id = match.Groups["ID"].Value
                        let value = match.Groups["Value"].Value
                        select new SubBlock(id, value)).ToList();

            return list.AsReadOnly();
        }
    }
}
