﻿namespace Swift
{
    public sealed class Field
    {
        internal Field(string id, string value)
        {
            this.Id = id;
            this.Value = value;
        }

        public string Id { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return string.Concat(":", this.Id, ":", this.Value);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Field;

            if (item == null)
            {
                return false;
            }

            return string.Equals(this.ToString(), item.ToString());
        }
    }
}
