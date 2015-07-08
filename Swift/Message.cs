namespace Swift
{
    public sealed class Message
    {
        internal Message(
            BasicHeader basicHeader,
            ApplicationHeader applicationHeader,
            UserHeader userHeader,
            TextBlock text,
            Trailer trailer)
        {
            if (basicHeader == null)
            {
                throw new ParserException("The Basic Header Block is mandatory.");
            }

            this.BasicHeader = basicHeader;
            this.ApplicationHeader = applicationHeader;
            this.UserHeader = userHeader;
            this.Text = text;
            this.Trailer = trailer;
        }

        public BasicHeader BasicHeader { get; private set; }

        public ApplicationHeader ApplicationHeader { get; private set; }

        public UserHeader UserHeader { get; private set; }

        public TextBlock Text { get; private set; }

        public Trailer Trailer { get; private set; }

        ////public Field FindField(string id)
        ////{
        ////    return this.TextBlock == null ? null : this.TextBlock.FindField(id);
        ////}

        ////public ICollection<Field> FindFields(IEnumerable<string> ids)
        ////{
        ////    return this.TextBlock == null
        ////        ? new List<Field>().AsReadOnly()
        ////        : this.TextBlock.FindFields(ids);
        ////}

        public override string ToString()
        {
            return Invariant.Format(
                "{0}{1}{2}{3}{4}",
                this.BasicHeader,
                this.ApplicationHeader,
                this.UserHeader,
                this.Text,
                this.Trailer);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Message;

            if (item == null)
            {
                return false;
            }

            return string.Equals(this.ToString(), item.ToString());
        }
    }
}
