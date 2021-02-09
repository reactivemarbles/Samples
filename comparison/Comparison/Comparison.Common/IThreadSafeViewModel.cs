namespace Comparison.Common
{
    public interface IThreadSafeViewModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
    }
}