namespace Swift
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class FieldIterator : IEnumerable<Field>
    {
        private readonly TextBlock text;

        public FieldIterator(TextBlock text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (!text.IsTextBlock)
            {
                throw new ArgumentException("IsTextBlock is false.");
            }

            this.text = text;
        }

        public IEnumerator<Field> GetEnumerator()
        {
            var data = this.text.Content ?? string.Empty;
            ////var index = 0;

            // Look for a Field Identifier. 
            var match = Expressions.Field.Match(data);
            while (match.Success)
            {
                ////index++;

                var startIndex = match.Index;
                var fieldId = match.Groups["FieldID"].Value;
                var fieldLength = match.Value.Length;

                // Look for the next available Field Identifier.
                match = match.NextMatch();

                var nextIndex = match.Index;

                // The field value is the characters after field but before the next field.
                var length = nextIndex > 0 ? nextIndex - startIndex - fieldLength : data.Length - startIndex - fieldLength;
                var value = data.Substring(startIndex + fieldLength, length);

                yield return new Field(fieldId, value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
