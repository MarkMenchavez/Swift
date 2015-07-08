namespace Swift
{
    using System;
    using System.Collections.Generic;

    public sealed class UserHeader : MessageBlock
    {
        internal UserHeader(string block)
            : base(block)
        {
            this.Parse();
        }

        public override string Id
        {
            get
            {
                return "3";
            }
        }

        public ICollection<SubBlock> SubBlocks { get; private set; }

        protected override string Name
        {
            get
            {
                return "User Header Block";
            }
        }

        public override int GetHashCode()
        {
            return this.Block.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as UserHeader;

            if (item == null)
            {
                return false;
            }

            return string.Equals(item.Block, this.Block, StringComparison.Ordinal);
        }

        private void Parse()
        {
            this.Validate();

            this.SubBlocks = this.ParseSubBlocks();
        }
    }
}
