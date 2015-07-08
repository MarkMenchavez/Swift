namespace Swift
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    internal class MessageIterator : IEnumerable<MessageIterator.Message>
    {
        private readonly TextReader reader;

        public MessageIterator(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            this.reader = reader;
        }

        public IEnumerator<Message> GetEnumerator()
        {
            var content = this.reader.ReadToEnd();

            // Look for a Basic Header Block. 
            // A message always starts with this block.
            var match = Expressions.BasicHeader.Match(content);
            while (match.Success)
            {
                var startIndex = match.Index;

                // Look for the next available Basic Header Block.
                match = match.NextMatch();

                var nextIndex = match.Index;
                var length = nextIndex > 0 ? nextIndex - startIndex : content.Length - startIndex;

                // The message is the characters from the current basic header block to the next.
                var message = content.Substring(startIndex, length);

                // Remove all characters after the last }.
                message = Expressions.CloseBrace.Replace(message, string.Empty);

                yield return new Message(startIndex, message);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal class Message
        {
            public Message(int offset, string value)
            {
                this.Offset = offset;
                this.Value = value;
            }

            public int Offset { get; private set; }

            public string Value { get; private set; }
        }
    }
}
