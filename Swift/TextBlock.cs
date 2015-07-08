namespace Swift
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class TextBlock : MessageBlock
    {
        internal TextBlock(string block)
            : base(block)
        {
            this.IsTextBlock = this.Block.EndsWith("-}", StringComparison.Ordinal);
            this.Parse();
        }

        public override string Id
        {
            get
            {
                return "4";
            }
        }

        public bool IsTextBlock { get; private set; }

        public ICollection<SubBlock> SubBlocks { get; private set; }

        public ICollection<Field> Fields { get; private set; }

        protected override string Name
        {
            get
            {
                return "Text Block";
            }
        }

        protected override string EndCharacter
        {
            get
            {
                return !this.IsTextBlock ? base.EndCharacter : Environment.NewLine + "-" + base.EndCharacter;
            }
        }

        ////public Field FindField(string id)
        ////{
        ////    return this.Fields == null ? null : this.Fields.FirstOrDefault(item => item.Id == id);
        ////}

        ////public ICollection<Field> FindFields(IEnumerable<string> ids)
        ////{
        ////    var container = ids != null ? ids.ToList() : new List<string>();

        ////    var list = new List<Field>();

        ////    if (this.Fields != null)
        ////    {
        ////        list.AddRange(this.Fields.Where(field => container.Contains(field.Id)));
        ////    }

        ////    return list.AsReadOnly();
        ////}

        public override int GetHashCode()
        {
            return this.Block.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as TextBlock;

            if (item == null)
            {
                return false;
            }

            return string.Equals(item.Block, this.Block, StringComparison.Ordinal);
        }

        private ICollection<Field> ParseFields()
        {
            var iterator = new FieldIterator(this);
            var list = iterator.ToList();

            return list.AsReadOnly();
        }

        private void Parse()
        {
            this.Validate();

            if (!this.IsTextBlock)
            {
                this.SubBlocks = this.ParseSubBlocks();
            }
            else
            {
                this.Fields = this.ParseFields();
            }
        }
    }
}
