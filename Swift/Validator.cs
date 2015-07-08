namespace Swift
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public interface IValidator
    {
        ICollection<string> Errors { get; }

        bool Validate(string message);
    }

    public class Validator : IValidator
    {
        private readonly List<string> errors = new List<string>();

        public ICollection<string> Errors
        {
            get
            {
                return this.errors.AsReadOnly();
            }
        }

        public virtual bool Validate(string message)
        {
            this.errors.Clear();

            if (string.IsNullOrWhiteSpace(message))
            {
                this.errors.Add("Message is empty.");
                return false;
            }

            var matches = Expressions.Block.Matches(message);
            var blocks = (from Match match in matches
                          select match.Groups["BlockID"].Value).ToList();

            if (blocks.Count == 0)
            {
                this.errors.Add("Message has no blocks.");
                return false;
            }

            var sortedBlocks = blocks.OrderBy(item => item).ToList();
            if (!blocks.SequenceEqual(sortedBlocks))
            {
                this.errors.Add("Message has incorrect order of blocks.");
            }

            if (blocks.Count != blocks.Distinct().Count())
            {
                this.errors.Add("Message has duplicate blocks.");
            }

            return this.errors.Count == 0;
        }
    }
}
