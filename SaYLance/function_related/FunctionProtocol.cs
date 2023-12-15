using System.Diagnostics.Contracts;

namespace SaYLance.function_related
{
    public class FunctionProtocol
    {
        private List<ProtocolNote> Notes;
        public FunctionProtocol(IEnumerable<ProtocolNote> notes) { Notes = notes.ToList(); }
        public FunctionProtocol(int argumentsCount) { this.Notes = new() { new ProtocolNote(0, argumentsCount) }; }
        public void AddNote(int argumentsCount)
        {
            Notes.Add(new ProtocolNote(
                Notes.Sum(note => note.Count),
                argumentsCount));
        }
        public int NotesCount => Notes.Count;
        public ProtocolNote GetNoteByIndex(int noteIndex) => Notes[noteIndex];
    }
    public class ProtocolNote
    {
        public ProtocolNote(int startIndex, int count)
        {
            StartIndex = startIndex;
            Count = count;
        }
        public int StartIndex { get; }
        public int Count { get; }

    }
}
